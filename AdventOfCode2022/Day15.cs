
internal class Day15 : AdventDay<long>
{
    public long RunP1(StreamReader reader)
    {
        const int CHECK_ROW = 2000000;

        List<Position> sensors = new();
        List<Position> beacons = new();

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            int[] args = line.Split(["Sensor at x=",
                ": closest beacon is at x=",
                ", y="], StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x)).ToArray();
            sensors.Add(new(args[0], args[1]));
            beacons.Add(new(args[2], args[3]));
        }

        int maxdist = sensors.Zip(beacons).Select(x => dist(x.First, x.Second)).Max();
        int xmin = sensors.Concat(beacons).MinBy(x => x.X).X - maxdist;
        int xmax = sensors.Concat(beacons).MaxBy(x => x.X).X + maxdist;

        int nobeacons = 0;
        for (int i = xmin; i <= xmax; i++)
        {
            Position pos = new(i, CHECK_ROW);
            if (sensors.Contains(pos)) { nobeacons++; continue; }
            if (beacons.Contains(pos)) continue;

            if (sensors.Zip(beacons)
                .Select(x => (dist(x.First, x.Second), dist(x.First, pos)))
                .Where(x => x.Item1 >= x.Item2)
                .Any()) nobeacons++;
        }

        return nobeacons;
    }

    public long RunP2(StreamReader reader)
    {
        const int SEARCH_AREA = 4000000;

        List<Position> sensors = new();
        List<Position> beacons = new();

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            int[] args = line.Split(["Sensor at x=",
                ": closest beacon is at x=",
                ", y="], StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x)).ToArray();
            sensors.Add(new(args[0], args[1]));
            beacons.Add(new(args[2], args[3]));
        }

        List<(Position, int)> sensorRanges = sensors.Zip(beacons)
            .Select(x => (x.First, dist(x.First, x.Second))).ToList();

        Dictionary<(bool, int), int> lines = new();

        foreach (var sensor in sensorRanges)
        {
            (bool, int)[] sensorLines =
                [(true, sensor.Item1.Y - sensor.Item2 - 1 - sensor.Item1.X),
                    (false, sensor.Item1.Y - sensor.Item2 - 1 + sensor.Item1.X),
                    (true, sensor.Item1.Y + sensor.Item2 + 1 - sensor.Item1.X),
                    (false, sensor.Item1.Y + sensor.Item2 + 1 + sensor.Item1.X)];

            foreach (var sensorLine in sensorLines)
            {
                if (lines.ContainsKey(sensorLine))
                {
                    lines[sensorLine]++;
                }
                else
                {
                    lines.Add(sensorLine, 1);
                }
            }
        }

        List<int> riseLines = new();
        List<int> descLines = new();

        foreach (var sLine in lines.Where(x => x.Value > 1))
        {
            if (sLine.Key.Item1)
                descLines.Add(sLine.Key.Item2);
            else riseLines.Add(sLine.Key.Item2);
        }

        List<Position> points = new();
        foreach (var riseq in riseLines)
            foreach (var descq in descLines)
            {
                int x = (riseq - descq) / 2;
                int y = x + descq;
                points.Add(new(x, y));
            }

        Position distress = points
            .Where(x => 0 <= x.Y && x.Y <= SEARCH_AREA && 0 <= x.X && x.X <= SEARCH_AREA)
            .Where(point =>
            {
                foreach (var sensor in sensorRanges)
                {
                    if (sensor.Item2 >= dist(sensor.Item1, point)) return false;
                }
                return true;
            }).First();

        return (long)distress.X * 4000000 + (long)distress.Y;
        /*

        Position distress = new();
        Console.Write("0/4000000");
        for (int x = 0; x <= 4000000; x++)
        {
            for (int y = 0; y <= 4000000; y++)
            {
                Position pos = new(x, y);
                if (sensors.Contains(pos) ||
                    beacons.Contains(pos) ||
                    sensors.Zip(beacons)
                    .Select(x => (dist(x.First, x.Second), dist(x.First, pos)))
                    .Where(x => x.Item1 >= x.Item2)
                    .Any()) continue;

                distress = pos;
                break;
            }
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write($"{x}/4000000");
            if (distress != new Position()) break;
        }
        Console.WriteLine();
        
        return distress.X * 4000000 + distress.Y;
        

        List<List<Position>> diamonds = new();

        for (int i = 0; i < sensors.Count; i++)
        {
            int d = dist(sensors[i], beacons[i]) + 1;
            List<Position> diamond = new();

            for (int j = -d; j <= d; j++)
            {
                if (j == d || j == -d)
                {
                    diamond.Add(new(sensors[i].X + j, sensors[i].Y));
                    continue;
                }
                diamond.Add(new(sensors[i].X + j, sensors[i].Y + d - Math.Abs(j)));
                diamond.Add(new(sensors[i].X + j, sensors[i].Y - d + Math.Abs(j)));
            }
            diamonds.Add(diamond);
        }

        Console.WriteLine("Diamonds Created");
        
        for (int i = 0; i < diamonds.Count; i++)
        {
            List<List<Position>> intersections = new();

            for (int j = i + 1; j < diamonds.Count; j++)
                intersections.Add(diamonds[i].Intersect(diamonds[j]).ToList());
            diamonds[i] = intersections.Aggregate(new List<Position>(), (a, b) => a.Concat(b).ToList())
                .Distinct().ToList();
        }
        Console.WriteLine("Intersections Created");
        
        List<Position> targets = diamonds.Aggregate(new List<Position>(), (a, b) => a.Concat(b).ToList())
                .Distinct().Where(pos => diamonds.Where(x => x.Contains(pos)).Count() >= 2).ToList();
        
        Console.WriteLine(targets.Count);

        Position distress = new();
        foreach (var target in targets)
        {
            if (sensors.Contains(target) ||
                beacons.Contains(target) ||
                sensors.Zip(beacons)
                .Select(x => (dist(x.First, x.Second), dist(x.First, target)))
                .Where(x => x.Item1 >= x.Item2)
                .Any()) continue;

            distress = target;
            break;
        }

        Console.WriteLine(distress);
        return distress.X * 4000000 + distress.Y;
        */
    }

    readonly record struct Position(int X, int Y);
    int dist(Position a, Position b) => Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
}