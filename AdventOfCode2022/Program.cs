Console.WriteLine("Advent of Code\n$year=2022;");
write_empty();

/*
RunDay(new Day1(),
    "adventofcode.com_2022_day_1_input",
    "Day 1: Calorie Counting",
    "The Elf that carries the most calories carries",
    "The top three elves that are carrying the most calories\nare carrying in total");

RunDay(new Day2(),
    "adventofcode.com_2022_day_2_input",
    "Day 2: Rock Paper Scissors",
    "If everything goes exactly according to my strategy guide,\nmy total score would be",
    "If everything goes exactly according to my strategy guide,\nmy total score would be");

RunDay(new Day3(),
    "adventofcode.com_2022_day_3_input",
    "Day 3: Rucksack Reorganization",
    "The sum of the priorities of all common items is",
    "The sum of the priorities of all badges is");

RunDay(new Day4(),
    "adventofcode.com_2022_day_4_input",
    "Day 4: Camp Cleanup",
    "The number of assignment pairs,\nwhere one range fully contains the other, is",
    "The number of assignment pairs,\nwhere one range fully contains the other, is");

RunDay(new Day5(),
    "adventofcode.com_2022_day_5_input",
    "Day 5: Supply Stacks",
    "After rearranging, the crates at the top are",
    "After rearranging, the crates at the top are");

RunDay(new Day6(),
    "adventofcode.com_2022_day_6_input",
    "Day 6: Tuning trouble",
    "Before the first start-of-packet marker is detected,\n the amount of characters needed to be processed is",
    "Before the first start-of-packet marker is detected,\n the amount of characters needed to be processed is");

RunDay(new Day7(),
    "adventofcode.com_2022_day_7_input",
    "Day 7: No Space Left On Device",
    "The sum of the total sizes of all directories\nwith a total size of at most 100000 is",
    "The smallest directory that fits the criteria\nhas a total size of");

RunDay(new Day8(),
    "adventofcode.com_2022_day_8_input",
    "Day 8: Treetop Tree House",
    "The amount of trees visible from outside is",
    "The highest scenic score possible is");

RunDay(new Day9(),
    "adventofcode.com_2022_day_9_input",
    "Day 9: Rope Bridge",
    "The amount of positions the tail of the rop visits at least once is",
    "The amount of positions the tail of the rop visits at least once is");

RunDay(new Day10(),
    "adventofcode.com_2022_day_10_input",
    "Day 10: Cathode-Ray Tube",
    "The sum of the signal strengths is",
    "");

RunDay(new Day11(),
    "adventofcode.com_2022_day_11_input",
    "Day 11: Monkey in the Middle",
    "",
    "");

RunDay(new Day12(),
    "adventofcode.com_2022_day_12_input",
    "Day 12: Hill Climbing Algorithm",
    "",
    "");
*/

Console.ReadLine();

static void write_sep() => Console.WriteLine(string.Join("", Enumerable.Repeat("#", 50)));
static void write_empty() => Console.WriteLine();

static void RunDay<T>(AdventDay<T> day, string inputPath, string title, string p1Text, string p2Text)
{
    write_empty();
    write_sep();
    Console.WriteLine($"--- {title} ---");
    write_empty();
    T result;
    using (var reader = new StreamReader($@"inputs\{inputPath}.txt"))
        result = day.RunP1(reader);
    Console.WriteLine($"{p1Text}:\n{result}");
    write_empty();
    Console.WriteLine("--- Part Two ---");
    write_empty();
    using (var reader = new StreamReader($@"inputs\{inputPath}.txt"))
        result = day.RunP2(reader);
    Console.WriteLine($"{p2Text}:\n{result}");
}