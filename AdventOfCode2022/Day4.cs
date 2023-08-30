internal class Day4
{
    internal static int run_p1(StreamReader reader)
    {
        int count = 0;

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            var (start1, end1, start2, end2) = sections(line);

            var first = Enumerable.Range(start1, end1 - start1 + 1);
            var second = Enumerable.Range(start2, end2 - start2 + 1);
            var sect = first.Intersect(second);

            if (sect.Count() == first.Count() || sect.Count() == second.Count()) count++;
        }

        return count;
    }

    internal static int run_p2(StreamReader reader)
    {
        int count = 0;

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            var (start1, end1, start2, end2) = sections(line);

            var first = Enumerable.Range(start1, end1 - start1 + 1);
            var second = Enumerable.Range(start2, end2 - start2 + 1);
            var sect = first.Intersect(second);

            if (sect.Any()) count++;
        }

        return count;
    }

    static (int, int, int, int) sections(string line)
    {
        int[] sections = line.Split(new char[] { ',', '-' }).Select(s => int.Parse(s)).ToArray();

        return (sections[0], sections[1], sections[2], sections[3]);
    }
}