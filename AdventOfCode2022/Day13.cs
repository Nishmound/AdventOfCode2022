

internal class Day13 : AdventDay<int>
{
    public int RunP1(StreamReader reader)
    {
        var pairIndex = 1;
        var sumIndex = 0;
        
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            var left = line.Trim();
            var right = (reader.ReadLine() ?? string.Empty).Trim();

            if (isInRightOrder(left, right)) sumIndex += pairIndex;

            pairIndex++;
            reader.ReadLine();
        }

        return sumIndex;
    }

    private bool isInRightOrder(string l, string r)
    {
        var depth = 0;

        for(int i = 0; i < l.Length && i < r.Length; i++)
        {
            if (l[i] == '[' && r[i] == '[') { depth++; continue; }


        }

        return true;
    }

    public int RunP2(StreamReader reader)
    {
        return 0;
    }
}