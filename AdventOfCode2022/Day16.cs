

internal class Day16 : AdventDay<int>
{
    public int RunP1(StreamReader reader)
    {
        var valves = Parse(reader);
        int minutes = 30;
        string start = "AA";
        var (pressure, path) = maxPressure(minutes, start, valves);
        Console.WriteLine(path);
        return pressure;
    }

    public int RunP2(StreamReader reader)
    {
        return 0;
    }

    private (int, string) maxPressure(int minutes, string current, Dictionary<string, (int, bool, string[])> valves)
    {
        int pressure = 0;
        bool opened = false;
        if (!valves[current].Item2 && valves[current].Item1 > 0)
        {
            minutes--;
            valves[current] = (valves[current].Item1, true, valves[current].Item3);
            pressure += valves[current].Item1 * minutes;
            opened = true;
        }

        if (minutes < 3 || !valves.Where(x => x.Value.Item1 > 0 && !x.Value.Item2).Any())
        {
            if (opened) valves[current] = (valves[current].Item1, false, valves[current].Item3);
            return (pressure, opened ? current : string.Empty);
        }
        var max = valves[current].Item3.Select(x => maxPressure(minutes - 1, x, valves)).MaxBy(x => x.Item1);
        pressure += max.Item1;
        if (opened) valves[current] = (valves[current].Item1, false, valves[current].Item3);
        return (pressure, opened ? current + " < " + max.Item2 : max.Item2);
    }

    private Dictionary<string, (int, bool, string[])> Parse(StreamReader reader)
    {
        Dictionary<string, (int, bool, string[])> valves = new();

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] parts = line.Split(["Valve ", " has flow rate=", 
                "; tunnels lead to valves ", "; tunnel leads to valve ", 
                ", "], StringSplitOptions.RemoveEmptyEntries);

            valves.Add(parts[0], (int.Parse(parts[1]), false, parts[2..]));
        }

        return valves;
    }
}