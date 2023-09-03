internal class Day5
{
    internal static string run_p1(StreamReader reader)
    {
        Stack<char>[] _crates = crates(reader);

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] command = line.Split(' ');
            for (int _ = 0; _ < int.Parse(command[1]); _++)
            {
                char crate = _crates[int.Parse(command[3]) - 1].Pop();
                _crates[int.Parse(command[5]) - 1].Push(crate);
            }
        }

        string tops = "";
        foreach (var stack in _crates)
        {
            tops += stack.Peek();
        }

        return tops;
    }

    internal static string run_p2(StreamReader reader)
    {
        Stack<char>[] _crates = crates(reader);

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] command = line.Split(' ');
            Stack<char> tmpStack = new();
            for (int _ = 0; _ < int.Parse(command[1]); _++)
            {
                char crate = _crates[int.Parse(command[3]) - 1].Pop();
                tmpStack.Push(crate);
            }
            foreach (var crate in tmpStack)
            {
                _crates[int.Parse(command[5]) - 1].Push(crate);
            }
        }

        string tops = "";
        foreach (var stack in _crates)
        {
            //if (stack.Count == 0) continue; 
            tops += stack.Peek();
        }

        return tops;
    }

    static Stack<char>[] crates(StreamReader reader)
    {
        List<string> lines = new();
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            if (string.IsNullOrWhiteSpace(line)) break;
            lines.Add(line);
        }

        Stack<char>[] _crates = new Stack<char>[9];
        for (int i = 0; i < _crates.Length; i++) _crates[i] = new();

        for (int i = lines.Count() - 2; i >= 0; i--)
        {
            for (int j = 0; j * 4 < lines[i].Length; j++)
            {
                string crate = lines[i][(j * 4)..((j * 4) + 3)];
                if (string.IsNullOrWhiteSpace(crate)) continue;
                _crates[j].Push(crate[1]);
            }
        }

        return _crates;
    }
}