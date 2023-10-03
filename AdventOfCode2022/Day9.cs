internal class Day9 : AdventDay<int>
{
    public int RunP1(StreamReader reader)
    {
        Knot tail = new();
        Knot head = new(tail);

        List<Position> visited = new() { tail.Pos };

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] command = line.Split(' ');

            for (int i = 0; i < int.Parse(command[1]); i++)
            {
                Position pos = head.Pos;
                switch (command[0])
                {
                    case "R": pos.X++; break;
                    case "L": pos.X--; break;
                    case "U": pos.Y++; break;
                    case "D": pos.Y--; break;
                }
                head.UpdatePos(pos);
                visited.Add(tail.Pos);
            }
        }

        return visited.Distinct().Count();
    }

    public int RunP2(StreamReader reader)
    {
        Knot tail = new();
        Knot head = new(new(new(new(new(new(new(new(new(tail)))))))));

        List<Position> visited = new() { tail.Pos };

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] command = line.Split(' ');

            for (int i = 0; i < int.Parse(command[1]); i++)
            {
                Position pos = head.Pos;
                switch (command[0])
                {
                    case "R": pos.X++; break;
                    case "L": pos.X--; break;
                    case "U": pos.Y++; break;
                    case "D": pos.Y--; break;
                }
                head.UpdatePos(pos);
                visited.Add(tail.Pos);
            }
        }
        return visited.Distinct().Count();
    }

    record struct Position(int X, int Y);

    class Knot
    {
        public Position Pos { get; set; }
        public Knot? Next { get; set; }

        public Knot()
        {
            Pos = new(0, 0);
            Next = null;
        }
        public Knot(Knot next) : this() { Next = next; }

        public void UpdatePos(Position pos)
        {
            Pos = pos;
            if (Next == null || (Math.Abs(Pos.X - Next.Pos.X) <= 1 && Math.Abs(Pos.Y - Next.Pos.Y) <= 1)) return;

            var nextPos = Next.Pos;
            if (Pos.X > nextPos.X) nextPos.X++;
            if (Pos.X < nextPos.X) nextPos.X--;
            if (Pos.Y > nextPos.Y) nextPos.Y++;
            if (Pos.Y < nextPos.Y) nextPos.Y--;
            Next.UpdatePos(nextPos);
        }
    }
}