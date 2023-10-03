using System.Numerics;

internal class Day11 : AdventDay<int>
{
    public int RunP1(StreamReader _)
    {
        var monkeys = setupMonkeys();

        for (int i = 0; i < 20; i++)
        {
            foreach (var monkey in monkeys)
            {
                foreach (var oldItem in monkey.Items)
                {
                    monkey.Inspections++;
                    long newItem = monkey.Op(oldItem);
                    newItem /= 3;
                    if (newItem % monkey.TestVal == 0) monkey.CaseTrue.Items.Add(newItem);
                    else monkey.CaseFalse.Items.Add(newItem);
                }
                monkey.Items = new();
            }
        }

        var orderedMonkeys = monkeys.OrderByDescending(x => x.Inspections).ToList();
        return orderedMonkeys[0].Inspections * orderedMonkeys[1].Inspections;
    }

    public int RunP2(StreamReader _)
    {
        var monkeys = setupMonkeys();
        int divisorProduct = monkeys.Select(x => x.TestVal).Aggregate(1, (a, b) => a * b);

        for (int i = 0; i < 10000; i++)
        {
            Console.Write("Runde " + i);
            foreach (var monkey in monkeys)
            {
                foreach (var oldItem in monkey.Items)
                {
                    monkey.Inspections++;
                    long newItem = monkey.Op(oldItem) % divisorProduct;
                    if (newItem % monkey.TestVal == 0) monkey.CaseTrue.Items.Add(newItem);
                    else monkey.CaseFalse.Items.Add(newItem);
                }
                monkey.Items = new();
            }
            Console.SetCursorPosition(0, Console.CursorTop);
        }

        var orderedMonkeys = monkeys.OrderByDescending(x => x.Inspections).ToList();
        Console.WriteLine(Math.BigMul(orderedMonkeys[0].Inspections, orderedMonkeys[1].Inspections));
        return 0;
    }

    List<Monkey> setupMonkeys()
    {
        List<Monkey> monkeys = new()
        {
            new(x => x * 3, 13),
            new(x => x + 1, 3),
            new(x => x * 13, 7),
            new(x => x * x, 2),
            new(x => x + 7, 19),
            new(x => x + 8, 5),
            new(x => x + 4, 11),
            new(x => x + 5, 17)
        };

        monkeys[0].Items = new() { 89, 73, 66, 57, 64, 80 };
        monkeys[1].Items = new() { 83, 78, 81, 55, 81, 59, 69 };
        monkeys[2].Items = new() { 76, 91, 58, 85 };
        monkeys[3].Items = new() { 71, 72, 74, 76, 68 };
        monkeys[4].Items = new() { 98, 85, 84 };
        monkeys[5].Items = new() { 78 };
        monkeys[6].Items = new() { 86, 70, 60, 88, 88, 78, 74, 83 };
        monkeys[7].Items = new() { 81, 58 };

        monkeys[0].CaseTrue = monkeys[6];
        monkeys[0].CaseFalse = monkeys[2];
        monkeys[1].CaseTrue = monkeys[7];
        monkeys[1].CaseFalse = monkeys[4];
        monkeys[2].CaseTrue = monkeys[1];
        monkeys[2].CaseFalse = monkeys[4];
        monkeys[3].CaseTrue = monkeys[6];
        monkeys[3].CaseFalse = monkeys[0];
        monkeys[4].CaseTrue = monkeys[5];
        monkeys[4].CaseFalse = monkeys[7];
        monkeys[5].CaseTrue = monkeys[3];
        monkeys[5].CaseFalse = monkeys[0];
        monkeys[6].CaseTrue = monkeys[1];
        monkeys[6].CaseFalse = monkeys[2];
        monkeys[7].CaseTrue = monkeys[3];
        monkeys[7].CaseFalse = monkeys[5];

        return monkeys;
    }

    class Monkey
    {
        public List<long> Items { get; set; }
        public Operation Op { get; init; }
        public int TestVal { get; init; }
        public Monkey CaseTrue { get; set; }
        public Monkey CaseFalse { get; set; }

        public int Inspections { get; set; }

        public Monkey(Operation op, int testVal)
        {
            Items = new();
            Op = op;
            TestVal = testVal;
            CaseTrue = this;
            CaseFalse = this;
            Inspections = 0;
        }

        public delegate long Operation(long value);
    }
}