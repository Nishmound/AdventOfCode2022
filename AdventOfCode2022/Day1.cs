namespace AdventOfCode2022;
internal class Day1
{
    public static int run(StreamReader reader)
    {
        var newElf = true;
        List<int> elfs = new();

        string? line;
        while((line = reader.ReadLine()) != null)
        {
            if (string.IsNullOrWhiteSpace(line)) { newElf = true; continue; }
            if (newElf) {
                newElf = false;
                elfs.Add(0);
            }

            elfs[elfs.Count - 1] += int.Parse(line);
        }

        return elfs.Max();
    }
}
