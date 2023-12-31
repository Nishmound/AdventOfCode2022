﻿internal class Day3 : AdventDay<int>
{
    public int RunP1(StreamReader reader)
    {
        int sum = 0;

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            string left = line[..(line.Length / 2)];
            string right = line[(line.Length / 2)..];

            char common = left.Intersect(right).First();

            sum += char.IsLower(common) ? common % 32 : common % 32 + 26;
        }

        return sum;
    }

    public int RunP2(StreamReader reader)
    {
        int sum = 0;

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            string line2 = reader.ReadLine() ?? string.Empty;
            string line3 = reader.ReadLine() ?? string.Empty;

            char common = line.Intersect(line2).Intersect(line3).First();

            sum += char.IsLower(common) ? common % 32 : common % 32 + 26;
        }

        return sum;
    }
}
