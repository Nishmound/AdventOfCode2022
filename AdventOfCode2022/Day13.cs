
using System.Text.Json;

internal class Day13 : AdventDay<int>
{
    public int RunP1(StreamReader reader)
    {
        var pairIndex = 0;
        var sumIndex = 0;

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            var first = line.Trim();
            var second = (reader.ReadLine() ?? string.Empty).Trim();
            reader.ReadLine();

            pairIndex++;
            sumIndex += Compare(first, second) < 0 ? pairIndex : 0;
        }

        return sumIndex;
    }

    public int RunP2(StreamReader reader)
    {
        string[] a = { "[[]]", "[[2]]", "[[6]]" };
        List<string> ss = new(a);
        string? line;
        while ((line = reader.ReadLine()) != null)
            if (line.Length > 0)
                ss.Add(line);
        ss.Sort(Compare);
        return ss.IndexOf(a[1]) * ss.IndexOf(a[2]);
    }
    private int Compare(string s1, string s2) =>
        Compare(JsonSerializer.Deserialize<JsonElement>(s1),
            JsonSerializer.Deserialize<JsonElement>(s2));

    private int Compare(JsonElement j1, JsonElement j2) =>
        (j1.ValueKind, j2.ValueKind) switch
        {
            (JsonValueKind.Number, JsonValueKind.Number) =>
                j1.GetInt32() - j2.GetInt32(),
            (JsonValueKind.Number, _) =>
                DoCompare(JsonSerializer.Deserialize<JsonElement>($"[{j1.GetInt32()}]"), j2),
            (_, JsonValueKind.Number) =>
                DoCompare(j1, JsonSerializer.Deserialize<JsonElement>($"[{j2.GetInt32()}]")),
            _ => DoCompare(j1, j2),
        };

    private int DoCompare(JsonElement j1, JsonElement j2)
    {
        int res;
        JsonElement.ArrayEnumerator e1 = j1.EnumerateArray();
        JsonElement.ArrayEnumerator e2 = j2.EnumerateArray();

        while (e1.MoveNext() && e2.MoveNext())
            if ((res = Compare(e1.Current, e2.Current)) != 0)
                return res;
        return j1.GetArrayLength() - j2.GetArrayLength();
    }
}