
using System.Collections.Frozen;

internal class Day18 : AdventDay<int>
{
    public int RunP1(StreamReader reader)
    {
        var shape = Parse(reader);

        return shape.Select(pos => 6 - Neighbors(pos, shape)).Sum();
    }

    public int RunP2(StreamReader reader)
    {
        var shape = Parse(reader);
        var outside = GetOutside(shape);

        return shape.Select(pos => Neighbors(pos, outside)).Sum();
    }

    private int Neighbors(in Position core, in FrozenSet<Position> shape)
    {
        int neighbors = 0;
        foreach (var pos in shape) if (dist(core, pos) == 1) neighbors++;
        return neighbors;
    }

    private FrozenSet<Position> GetOutside(FrozenSet<Position> shape)
    {
        List<Position> outside = [new(-1,-1,-1)];
        int xmax = shape.MaxBy(pos => pos.X).X + 1;
        int ymax = shape.MaxBy(pos => pos.Y).Y + 1;
        int zmax = shape.MaxBy(pos => pos.Z).Z + 1;

        Trace(new(-1,-1,-1));

        void Trace(Position start)
        {
            Position[] neighbors = [
                new(start.X + 1, start.Y, start.Z),
                new(start.X - 1, start.Y, start.Z),
                new(start.X, start.Y + 1, start.Z),
                new(start.X, start.Y - 1, start.Z),
                new(start.X, start.Y, start.Z + 1),
                new(start.X, start.Y, start.Z - 1),
            ];
            neighbors = neighbors.Where(x =>
            x.X >= -1
            && x.Y >= -1
            && x.Z >= -1
            && x.X <= xmax
            && x.Y <= ymax
            && x.Z <= zmax
            && !shape.Contains(x)
            && !outside.Contains(x)
            ).ToArray();

            if (!neighbors.Any()) return;

            outside = outside.Concat(neighbors).ToList();

            foreach (var neighbor in neighbors) Trace(neighbor);
        }

        return outside.ToFrozenSet();
    }

    int dist(in Position a, in Position b) =>
        Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) + Math.Abs(a.Z - b.Z);

    FrozenSet<Position> Parse(StreamReader reader)
    {
        List<Position> positions = new();

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            var parts = line.Split(',').Select(p => int.Parse(p)).ToArray();
            positions.Add(new(parts[0], parts[1], parts[2]));
        }

        return positions.ToFrozenSet();
    }

    readonly record struct Position(int X, int Y, int Z);
}