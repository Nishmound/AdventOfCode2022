
using System.Net.Sockets;

internal class Day17 : AdventDay<long>
{
    public long RunP1(StreamReader reader)
    {
        string jets = reader.ReadToEnd().Trim();
        bool[,] chamber = new bool[7, 256];
        long steps = 0;
        long highest = -1;

        for (long i = 0; i < 2022; i++)
        {
            Sim(i, chamber, jets, ref steps, ref highest);
            if (highest > chamber.GetLength(1) - 16) chamber = Expand(chamber);
        }

        return highest + 1;
    }

    public long RunP2(StreamReader reader)
    {
        const long TARGET = 1000000000000;
        string jets = reader.ReadToEnd().Trim();
        bool[,] chamber = new bool[7, 256];
        long steps = 0;
        long highest = -1;
        long added_by_repeats = 0;
        List<(bool[], long)> tops = new();
        Dictionary<(long, long), (long, long, long)> seen = new();

        long i = 0;
        while (i < TARGET)
        {
            Sim(i, chamber, jets, ref steps, ref highest);
            if (highest > chamber.GetLength(1) - 16) chamber = Expand(chamber);

            if (added_by_repeats == 0)
            {
                var key = (i % 5, steps % (jets.Length));
                if (seen.ContainsKey(key))
                {
                    if (seen[key].Item1 == 2)
                    {
                        var (_, old_i, old_highest) = seen[key];
                        var delta_highest = highest - old_highest;
                        var delta_i = i - old_i;
                        var repeats = (TARGET - i) / delta_i;
                        added_by_repeats += repeats * delta_highest;
                        i += repeats * delta_i;
                    }
                    else
                    {
                        seen[key] = (seen[key].Item1 + 1, i, highest);
                    }
                }
                else
                {
                    seen[key] = (1, i, highest);
                }
            }

            i++;
        }


        return highest + added_by_repeats + 1;
    }

    void Sim(in long i, in bool[,] chamber, in string jets, ref long steps, ref long highest)
    {
        Position start = new(2, highest + 4);
        Block current = getBlock((BlockType)(i % 5), start);

        while (true)
        {
            if (jets[(int)steps % jets.Length] == '<')
            {
                if (isEmpty(chamber, current.CheckLeft())) current.MoveLeft();
            }
            else
            {
                if (isEmpty(chamber, current.CheckRight())) current.MoveRight();
            }
            steps++;

            if (isEmpty(chamber, current.CheckDown())) current.MoveDown();
            else break;
        }

        Position[] block = current.Place();
        foreach (var pos in block) chamber[pos.X, pos.Y] = true;

        if (highest < block.MaxBy(x => x.Y).Y)
        {
            highest = block.MaxBy(x => x.Y).Y;

        }
    }
    bool[] getTops(in bool[,] chamber, in long highest)
    {
        bool[] tops = new bool[7 * 10];
        for (int i = 0; i < 10; i++)
        {
            int l = i * 7;
            if (highest - i < 0)
            {
                tops[l] = false;
                tops[l + 1] = false;
                tops[l + 2] = false;
                tops[l + 3] = false;
                tops[l + 4] = false;
                tops[l + 5] = false;
                tops[l + 6] = false;
            }
            else
            {
                tops[l] = chamber[0, highest - i];
                tops[l + 1] = chamber[1, highest - i];
                tops[l + 2] = chamber[2, highest - i];
                tops[l + 3] = chamber[3, highest - i];
                tops[l + 4] = chamber[4, highest - i];
                tops[l + 5] = chamber[5, highest - i];
                tops[l + 6] = chamber[6, highest - i];
            }
        }
        return tops;
    }
    bool[,] Expand(bool[,] arr)
    {
        bool[,] narr = new bool[arr.GetLength(0), arr.GetLength(1) * 2];
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                narr[i, j] = arr[i, j];
            }
        }
        return narr;
    }

    bool isEmpty(in bool[,] chamber, in Position[] positions)
    {
        foreach (var pos in positions)
            if (pos.X < 0
                || pos.X >= chamber.GetLength(0)
                || pos.Y < 0
                || pos.Y >= chamber.GetLength(1)
                || chamber[pos.X, pos.Y])
                return false;
        return true;
    }

    Block getBlock(BlockType type, Position pos) => type switch
    {
        BlockType.Bar => new Bar(pos),
        BlockType.Cross => new Cross(pos),
        BlockType.Angle => new Angle(pos),
        BlockType.Column => new Column(pos),
        BlockType.Square => new Square(pos),
        _ => throw new ArgumentOutOfRangeException(nameof(type), $"Not expected Block Type: {type}"),
    };
    readonly record struct Position(long X, long Y);

    abstract class Block
    {
        public Position Pos { get; private set; }
        public void MoveLeft() => Pos = new(Pos.X - 1, Pos.Y);
        public void MoveRight() => Pos = new(Pos.X + 1, Pos.Y);
        public void MoveDown() => Pos = new(Pos.X, Pos.Y - 1);
        public abstract Position[] CheckLeft();
        public abstract Position[] CheckRight();
        public abstract Position[] CheckDown();
        public abstract Position[] Place();
        public Block(Position pos)
        {
            Pos = pos;
        }
    }
    enum BlockType
    {
        Bar,
        Cross,
        Angle,
        Column,
        Square,
    }

    class Bar : Block
    {
        public Bar(Position pos) : base(pos) { }
        public override Position[] CheckDown() => [
            new(Pos.X, Pos.Y - 1),
            new(Pos.X + 1, Pos.Y - 1),
            new(Pos.X + 2, Pos.Y - 1),
            new(Pos.X + 3, Pos.Y - 1),
        ];
        public override Position[] CheckLeft() => [
            new(Pos.X - 1, Pos.Y),
        ];
        public override Position[] CheckRight() => [
            new(Pos.X + 4, Pos.Y),
        ];
        public override Position[] Place() => [
            new(Pos.X, Pos.Y),
            new(Pos.X + 1, Pos.Y),
            new(Pos.X + 2, Pos.Y),
            new(Pos.X + 3, Pos.Y),
        ];
    }
    class Column : Block
    {
        public Column(Position pos) : base(pos) { }
        public override Position[] CheckDown() => [
            new(Pos.X, Pos.Y - 1),
        ];
        public override Position[] CheckLeft() => [
            new(Pos.X - 1, Pos.Y),
            new(Pos.X - 1, Pos.Y + 1),
            new(Pos.X - 1, Pos.Y + 2),
            new(Pos.X - 1, Pos.Y + 3),
        ];
        public override Position[] CheckRight() => [
            new(Pos.X + 1, Pos.Y),
            new(Pos.X + 1, Pos.Y + 1),
            new(Pos.X + 1, Pos.Y + 2),
            new(Pos.X + 1, Pos.Y + 3),
        ];
        public override Position[] Place() => [
            new(Pos.X, Pos.Y),
            new(Pos.X, Pos.Y + 1),
            new(Pos.X, Pos.Y + 2),
            new(Pos.X, Pos.Y + 3),
        ];
    }
    class Cross : Block
    {
        public Cross(Position pos) : base(pos) { }
        public override Position[] CheckDown() => [
            new(Pos.X, Pos.Y),
            new(Pos.X + 1, Pos.Y - 1),
            new(Pos.X + 2, Pos.Y),
        ];
        public override Position[] CheckLeft() => [
            new(Pos.X, Pos.Y),
            new(Pos.X - 1, Pos.Y + 1),
            new(Pos.X, Pos.Y + 2),
        ];
        public override Position[] CheckRight() => [
            new(Pos.X + 2, Pos.Y),
            new(Pos.X + 3, Pos.Y + 1),
            new(Pos.X + 2, Pos.Y + 2),
        ];
        public override Position[] Place() => [
            new(Pos.X + 1, Pos.Y),
            new(Pos.X, Pos.Y + 1),
            new(Pos.X + 1, Pos.Y + 1),
            new(Pos.X + 2, Pos.Y + 1),
            new(Pos.X + 1, Pos.Y + 2),
        ];
    }
    class Angle : Block
    {
        public Angle(Position pos) : base(pos) { }
        public override Position[] CheckDown() => [
            new(Pos.X, Pos.Y - 1),
            new(Pos.X + 1, Pos.Y - 1),
            new(Pos.X + 2, Pos.Y - 1),
        ];
        public override Position[] CheckLeft() => [
            new(Pos.X - 1, Pos.Y),
            new(Pos.X + 1, Pos.Y + 1),
            new(Pos.X + 1, Pos.Y + 2),
        ];
        public override Position[] CheckRight() => [
            new(Pos.X + 3, Pos.Y),
            new(Pos.X + 3, Pos.Y + 1),
            new(Pos.X + 3, Pos.Y + 2),
        ];
        public override Position[] Place() => [
            new(Pos.X, Pos.Y),
            new(Pos.X + 1, Pos.Y),
            new(Pos.X + 2, Pos.Y),
            new(Pos.X + 2, Pos.Y + 1),
            new(Pos.X + 2, Pos.Y + 2),
        ];
    }
    class Square : Block
    {
        public Square(Position pos) : base(pos) { }
        public override Position[] CheckDown() => [
            new(Pos.X, Pos.Y - 1),
            new(Pos.X + 1, Pos.Y - 1),
        ];
        public override Position[] CheckLeft() => [
            new(Pos.X - 1, Pos.Y),
            new(Pos.X - 1, Pos.Y + 1),
        ];
        public override Position[] CheckRight() => [
            new(Pos.X + 2, Pos.Y),
            new(Pos.X + 2, Pos.Y + 1),
        ];
        public override Position[] Place() => [
            new(Pos.X, Pos.Y),
            new(Pos.X + 1, Pos.Y),
            new(Pos.X, Pos.Y + 1),
            new(Pos.X + 1, Pos.Y + 1),
        ];
    }
}