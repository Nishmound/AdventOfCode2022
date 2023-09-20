internal class Day1 : AdventDay<int>
{
    public int RunP1(StreamReader reader)
    {
        return elfs(reader).Max();
    }

    public int RunP2(StreamReader reader)
    {
        return elfs(reader).OrderDescending().Take(3).Sum();
    }

    static List<int> elfs(StreamReader reader)
    {
        var newElf = true;
        List<int> elfs = new();

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            if (string.IsNullOrWhiteSpace(line)) { newElf = true; continue; }
            if (newElf)
            {
                newElf = false;
                elfs.Add(0);
            }

            elfs[elfs.Count - 1] += int.Parse(line);
        }

        return elfs;
    }
}
