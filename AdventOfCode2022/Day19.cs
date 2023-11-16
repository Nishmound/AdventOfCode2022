
using System.Collections.Frozen;

internal class Day19 : AdventDay<int>
{
    public int RunP1(StreamReader reader)
    {
        var bps = Parse(reader);
        Stack<(byte, uint, uint)> stack = new();
        int quality = 0;

        foreach (var bp in bps)
        {
            Console.WriteLine($"Checking {bp}");
            stack.Push((24, 0, 1));
            byte currMax = 0;
            
            
            while(stack.Count != 0)
            {
                var state = stack.Pop();
                uint res = state.Item2 + state.Item3;

                if ((res >> 24) > currMax) currMax = (byte)(res >> 24);
                if (state.Item1 <= 1) continue;

                var canBuild = (
                    (byte)state.Item2 >= bp.OreCost && (byte)state.Item3 < bp.MaxOre,
                    (byte)state.Item2 >= bp.ClayCost && (byte)(state.Item3 >> 8) < bp.ObsidianCost.Item2,
                    (byte)state.Item2 >= bp.ObsidianCost.Item1 && (byte)(state.Item2 >> 8) >= bp.ObsidianCost.Item2 && (byte)(state.Item3 >> 16) < bp.GeodeCost.Item2,
                    (byte)state.Item2 >= bp.GeodeCost.Item1 && (byte)(state.Item2 >> 16) >= bp.GeodeCost.Item2
                    );

                byte maxAddGeodes = (byte)(canBuild.Item4
                    ? state.Item1 * (state.Item3 >> 24) + state.Item1 * (state.Item1 - 1) / 2
                    : state.Item1 * (state.Item3 >> 24) + (state.Item1 - 1) * (state.Item1 - 2) / 2);
                if ((res >> 24) + maxAddGeodes <= currMax) continue;

                if (canBuild.Item4)
                {
                    stack.Push(((byte)(state.Item1 - 1), (uint)(res - bp.GeodeCost.Item1 - (bp.GeodeCost.Item2 << 16)), state.Item3 + (1 << 24)));
                    continue;
                }

                stack.Push(((byte)(state.Item1 - 1), res, state.Item3));
                if (canBuild.Item1) stack.Push(((byte)(state.Item1 - 1), res - bp.OreCost, state.Item3 + 1));
                if (canBuild.Item2) stack.Push(((byte)(state.Item1 - 1), res - bp.ClayCost, state.Item3 + (1 << 8)));
                if (canBuild.Item3) stack.Push(((byte)(state.Item1 - 1), (uint)(res - bp.ObsidianCost.Item1 - (bp.ObsidianCost.Item2 << 8)), state.Item3 + (1 << 16)));
                
            }
            Console.WriteLine($"Max {currMax} Geodes");
            quality += bp.Id * currMax;
        }

        return quality;
    }

    public int RunP2(StreamReader reader)
    {
        var bps = Parse(reader).Take(3);
        Stack<(byte, uint, uint)> stack = new();
        int quality = 1;

        foreach (var bp in bps)
        {
            Console.WriteLine($"Checking {bp}");
            stack.Push((32, 0, 1));
            byte currMax = 0;


            while (stack.Count != 0)
            {
                var state = stack.Pop();
                uint res = state.Item2 + state.Item3;

                if ((res >> 24) > currMax) currMax = (byte)(res >> 24);
                if (state.Item1 <= 1) continue;

                var canBuild = (
                    (byte)state.Item2 >= bp.OreCost && (byte)state.Item3 < bp.MaxOre,
                    (byte)state.Item2 >= bp.ClayCost && (byte)(state.Item3 >> 8) < bp.ObsidianCost.Item2,
                    (byte)state.Item2 >= bp.ObsidianCost.Item1 && (byte)(state.Item2 >> 8) >= bp.ObsidianCost.Item2 && (byte)(state.Item3 >> 16) < bp.GeodeCost.Item2,
                    (byte)state.Item2 >= bp.GeodeCost.Item1 && (byte)(state.Item2 >> 16) >= bp.GeodeCost.Item2
                    );

                byte maxAddGeodes = (byte)(canBuild.Item4
                    ? state.Item1 * (state.Item3 >> 24) + state.Item1 * (state.Item1 - 1) / 2
                    : state.Item1 * (state.Item3 >> 24) + (state.Item1 - 1) * (state.Item1 - 2) / 2);
                if ((res >> 24) + maxAddGeodes <= currMax) continue;

                if (canBuild.Item4)
                {
                    stack.Push(((byte)(state.Item1 - 1), (uint)(res - bp.GeodeCost.Item1 - (bp.GeodeCost.Item2 << 16)), state.Item3 + (1 << 24)));
                    continue;
                }

                stack.Push(((byte)(state.Item1 - 1), res, state.Item3));
                if (canBuild.Item1) stack.Push(((byte)(state.Item1 - 1), res - bp.OreCost, state.Item3 + 1));
                if (canBuild.Item2) stack.Push(((byte)(state.Item1 - 1), res - bp.ClayCost, state.Item3 + (1 << 8)));
                if (canBuild.Item3) stack.Push(((byte)(state.Item1 - 1), (uint)(res - bp.ObsidianCost.Item1 - (bp.ObsidianCost.Item2 << 8)), state.Item3 + (1 << 16)));

            }
            Console.WriteLine($"Max {currMax} Geodes");
            quality *= currMax;
        }

        return quality;
    }

    private Blueprint[] Parse(StreamReader reader)
    {
        List<Blueprint> bps = new();

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            ushort[] parts = line.Split([
                "Blueprint ",
                ": Each ore robot costs ",
                " ore. Each clay robot costs ",
                " ore. Each obsidian robot costs ",
                " ore and ",
                " clay. Each geode robot costs ",
                " obsidian."
            ], StringSplitOptions.RemoveEmptyEntries)
                .Select(x => ushort.Parse(x)).ToArray();

            bps.Add(new(
                parts[0],
                parts[1],
                parts[2],
                (parts[3], parts[4]),
                (parts[5], parts[6]),
                ((ushort[])[parts[1], parts[2], parts[3], parts[5]]).Max()
                ));
        }
        reader.Close();

        return bps.ToArray();
    }
    readonly record struct Blueprint(
        ushort Id,
        ushort OreCost,
        ushort ClayCost,
        (ushort, ushort) ObsidianCost,
        (ushort, ushort) GeodeCost,
        ushort MaxOre
        );
}