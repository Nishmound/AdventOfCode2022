internal class Day6
{
    internal static int run_p1(StreamReader reader)
    {
        return find_distinct_index(reader, 4);
    }

    internal static int run_p2(StreamReader reader)
    {
        return find_distinct_index(reader, 14);
    }

    static int find_distinct_index(StreamReader reader, int distinctCount)
    {
        char[] buf = new char[distinctCount];
        reader.ReadBlock(buf);
        int charCount = distinctCount;

        while (buf.Distinct().Count() != distinctCount)
        {
            shift(buf);
            if (reader.ReadBlock(buf, distinctCount - 1, 1) == 0) break;
            charCount++;
        }

        return charCount;
    }

    static void shift<T>(T[] arr)
    {
        for (int i = 1; i < arr.Length; i++) arr[i - 1] = arr[i];
    }
}