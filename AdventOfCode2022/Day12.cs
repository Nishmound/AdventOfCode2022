using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

internal class Day12 : AdventDay<int>
{
    public int RunP1(StreamReader reader)
    {
        char[,] map = readMap(reader);

        Position start = new(findInArr(map, 'S').First());
        Position end = new(findInArr(map, 'E').First());

        return findPath(start, end, map);

    }

    public int RunP2(StreamReader reader)
    {
        char[,] map = readMap(reader);

        List<Position> starts = new() { new(findInArr(map, 'S').First()) };
        starts.AddRange(findInArr(map, 'a').Select(x => new Position(x)));
        Position end = new(findInArr(map, 'E').First());

        List<int> paths = starts.Select(x => findPath(x, end, map)).ToList();

        return paths.Min();
    }

    char[,] readMap(StreamReader reader)
    {
        List<List<char>> lines = new();


        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            List<char> chars = new();
            foreach (var c in line) chars.Add(c);
            lines.Add(chars);
        }

        char[,] map = new char[lines.Count, lines[0].Count];

        for (int i = 0; i < lines.Count; i++)
        {
            for (int j = 0; j < lines[i].Count; j++)
            {
                map[i, j] = lines[i][j];
            }
        }

        return map;
    }

    int findPath(Position start, Position end, in char[,] map)
    {
        Console.WriteLine("Start at (" + start.X + ", " + start.Y + "):");

        List<Position> visited = new();
        List<Position> current = new() { start };
        int steps = 0;
        int total = map.GetLength(0) * map.GetLength(1);

        Console.Write("Visited 0/" + total);

        while (!current.Contains(end))
        {
            List<Position> next = new();

            foreach (var pos in current)
            {
                Position up = new() { X = pos.X - 1, Y = pos.Y };
                if (up.X >= 0 &&
                    (map[up.X, up.Y] == 'E' ? 'z' : map[up.X, up.Y]) - (map[pos.X, pos.Y] == 'S' ? 'a' : map[pos.X, pos.Y]) <= 1 &&
                    !visited.Contains(up) && !current.Contains(up))
                    next.Add(up);
                Position down = new() { X = pos.X + 1, Y = pos.Y };
                if (down.X < map.GetLength(0) &&
                    (map[down.X, down.Y] == 'E' ? 'z' : map[down.X, down.Y]) - (map[pos.X, pos.Y] == 'S' ? 'a' : map[pos.X, pos.Y]) <= 1 &&
                    !visited.Contains(down) && !current.Contains(down))
                    next.Add(down);
                Position left = new() { X = pos.X, Y = pos.Y - 1 };
                if (left.Y >= 0 &&
                    (map[left.X, left.Y] == 'E' ? 'z' : map[left.X, left.Y]) - (map[pos.X, pos.Y] == 'S' ? 'a' : map[pos.X, pos.Y]) <= 1 &&
                    !visited.Contains(left) && !current.Contains(left))
                    next.Add(left);
                Position right = new() { X = pos.X, Y = pos.Y + 1 };
                if (right.Y < map.GetLength(1) &&
                    (map[right.X, right.Y] == 'E' ? 'z' : map[right.X, right.Y]) - (map[pos.X, pos.Y] == 'S' ? 'a' : map[pos.X, pos.Y]) <= 1 &&
                    !visited.Contains(right) && !current.Contains(right))
                    next.Add(right);
            }

            if (next.Count == 0)
            {
                steps = int.MaxValue;
                break;
            }

            visited.AddRange(current);
            current = next.Distinct().ToList();
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write("Visited " + visited.Count + "/" + total);
            steps++;
        }
        Console.WriteLine();
        Console.WriteLine("Path Length: " + steps);
        return steps;
    }

    record struct Position(int X, int Y)
    {
        public Position((int, int) coords) : this(coords.Item1, coords.Item2) { }
    }

    List<(int, int)> findInArr<T>(T[,] arr, T element) where T : IEquatable<T>
    {
        List<(int, int)> occ = new();
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                if (arr[i, j].Equals(element)) occ.Add((i, j));
            }
        }

        return occ;
    }
}