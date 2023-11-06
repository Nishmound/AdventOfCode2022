


using System;

internal class Day14 : AdventDay<int>
{
    public int RunP1(StreamReader reader)
    {
        List<Position[]> drawPositions = new();
        int xmin = 500;
        int xmax = 500;
        int ymax = 0;

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            Position[] positions = line.Split(" -> ")
                .Select(x => x.Split(","))
                .Select(x => new Position(int.Parse(x[0]), int.Parse(x[1])))
                .ToArray();
            drawPositions.Add(positions);
        }

        foreach (var draw in drawPositions)
            foreach (var pos in draw)
            {
                if (pos.X < xmin) xmin = pos.X;
                if (pos.X > xmax) xmax = pos.X;
                if (pos.Y > ymax) ymax = pos.Y;
            }

        SimGrid grid = new(xmin, xmax, ymax);
        foreach (var draw in drawPositions)
            grid.DrawLines(draw);

        return grid.Simulate();
    }

    public int RunP2(StreamReader reader)
    {
        List<Position[]> drawPositions = new();
        int xmin = 500;
        int xmax = 500;
        int ymax = 0;

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            Position[] positions = line.Split(" -> ")
                .Select(x => x.Split(","))
                .Select(x => new Position(int.Parse(x[0]), int.Parse(x[1])))
                .ToArray();
            drawPositions.Add(positions);
        }

        foreach (var draw in drawPositions)
            foreach (var pos in draw)
            {
                if (pos.X < xmin) xmin = pos.X;
                if (pos.X > xmax) xmax = pos.X;
                if (pos.Y > ymax) ymax = pos.Y;
            }

        ymax += 2;
        xmax += ymax;
        xmin -= ymax;
        drawPositions.Add([new Position(xmin, ymax), new Position(xmax, ymax)]);


        SimGrid grid = new(xmin, xmax, ymax);
        foreach (var draw in drawPositions)
            grid.DrawLines(draw);

        return grid.Simulate();
    }

    class SimGrid
    {
        public int XMin { get; }
        public int XMax { get; }
        public int YMax { get; }
        public bool[,] Grid { get; private set; }

        public SimGrid(int xMin, int xMax, int yMax)
        {
            XMin = xMin;
            XMax = xMax;
            YMax = yMax;
            Grid = new bool[xMax - xMin + 1, yMax + 1];
        }

        public void DrawLines(Position[] positions)
        {
            for (int i = 0; i < positions.Length - 1; i++)
            {
                if (positions[i].X == positions[i + 1].X)
                {
                    int max, min;
                    if (positions[i].Y < positions[i + 1].Y)
                    {
                        min = positions[i].Y;
                        max = positions[i + 1].Y;
                    }
                    else
                    {
                        max = positions[i].Y;
                        min = positions[i + 1].Y;
                    }

                    for (int j = min; j <= max; j++)
                    {
                        Grid[positions[i].X - XMin, j] = true;
                    }
                }
                else
                {
                    int max, min;
                    if (positions[i].X < positions[i + 1].X)
                    {
                        min = positions[i].X - XMin;
                        max = positions[i + 1].X - XMin;
                    }
                    else
                    {
                        max = positions[i].X - XMin;
                        min = positions[i + 1].X - XMin;
                    }

                    for (int j = min; j <= max; j++)
                    {
                        Grid[j, positions[i].Y] = true;
                    }
                }
            }
        }
        public int Simulate()
        {
            bool[,] grid = (bool[,])Grid.Clone();
            bool[,] sandGrid = new bool[grid.GetLength(0), grid.GetLength(1)];
            int units = 0;

            while (true)
            {
                units++;
                Position pos = new(500 - XMin, 0);

                while (pos.Y < YMax)
                {
                    if (!grid[pos.X, pos.Y + 1])
                        pos = new(pos.X, pos.Y + 1);
                    else if (pos.X == 0) break;
                    else if (!grid[pos.X - 1, pos.Y + 1])
                        pos = new(pos.X - 1, pos.Y + 1);
                    else if (pos.X == grid.GetLength(0) - 1) break;
                    else if (!grid[pos.X + 1, pos.Y + 1])
                        pos = new(pos.X + 1, pos.Y + 1);
                    else break;
                }

                if (pos.Y == YMax
                    || pos.X == 0
                    || pos.X == grid.GetLength(0) - 1)
                {
                    units--;
                    break;
                }
                grid[pos.X, pos.Y] = true;
                sandGrid[pos.X, pos.Y] = true;
                if (pos == new Position(500 - XMin, 0)) break;
            }


            return units;
        }
    }

    static void ShowGrid(bool[,] grid)
    {
        for (int y = 0; y < grid.GetLength(1); y++)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                Console.Write(grid[x, y] ? '#' : '.');
            }
            Console.WriteLine();
        }
    }
    public static void ShowGrid(bool[,] grid, bool[,] sandGrid)
    {
        for (int y = 0; y < grid.GetLength(1); y++)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                Console.Write(grid[x, y] ? (sandGrid[x, y] ? 'O' : '#') : '.');
            }
            Console.WriteLine();
        }
    }

    readonly record struct Position(int X, int Y);
}