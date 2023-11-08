

using static System.Reflection.Metadata.BlobBuilder;

internal class Day16 : AdventDay<int>
{
    public int RunP1(StreamReader reader)
    {
        var (weights, flows, start) = Parse(reader);
        int[] toCheck = Enumerable.Range(1, flows.Count() -1).Zip(flows.Skip(1))
            .Where(x => x.Second > 0)
            .Select(x => x.First).ToArray();

        var pressure = maxPressure(30, start, weights, flows, toCheck);
        return pressure;
    }

    public int RunP2(StreamReader reader)
    {
        var (weights, flows, start) = Parse(reader);
        int[] toCheck = Enumerable.Range(1, flows.Count() - 1).Zip(flows.Skip(1))
            .Where(x => x.Second > 0)
            .Select(x => x.First).ToArray();

        List<(int[], int[])> pathPairs = new();

        foreach (var item in SubSetsOf(toCheck))
        {
            var pair = (item.ToArray(), toCheck.Except(item).ToArray());

            foreach (var item1 in pathPairs)
            {
                if (!item1.Item1.Except(pair.Item2).Any()) goto exit;
            }

            pathPairs.Add(pair);
            exit:;
        }
        Console.WriteLine($"n:{toCheck.Length}, sub: { pathPairs.Count}");
        /*
        for (int i = 0; i <= toCheck.Length; i++)
        {
            for (int j = i; j <= toCheck.Length; j++)
            {
                        int[] a = toCheck[i..(j+1)];
                        int[] b = toCheck.Except(a).ToArray();
                        pathPairs.Add((a, b));
            }
        }
        */

        var pressure = pathPairs
            .Select(x => maxPressure(26, start, weights, flows, x.Item1)
                + maxPressure(26, start, weights, flows, x.Item2))
            .Max();
        return pressure;
    }
    private void showMatrix(int[,] matrix)
    {
        int threshold = int.MaxValue / matrix.GetLength(0);
        Console.Write("X\t0");
        for (int i = 1; i < matrix.GetLength(1); i++) Console.Write($"\t{i}");
        Console.WriteLine();
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            Console.Write($"{i}");
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] >= threshold)
                {
                    Console.Write("\tINF");
                } else Console.Write($"\t{matrix[i, j]}");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    // https://stackoverflow.com/a/999182
    public static IEnumerable<IEnumerable<T>> SubSetsOf<T>(IEnumerable<T> source)
    {
        if (!source.Any())
            return Enumerable.Repeat(Enumerable.Empty<T>(), 1);

        var element = source.Take(1);
        var haveNots = SubSetsOf(source.Skip(1));
        var haves = haveNots.Select(set => element.Concat(set));

        return haves.Concat(haveNots);
    }

    private (int[,], int[], int) Parse(StreamReader reader)
    {
        List<string[]> valveParts = new();
        
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] parts = line.Split(["Valve ",
                " has flow rate=",
                "; tunnels lead to valves ",
                "; tunnel leads to valve ",
                ", "], StringSplitOptions.RemoveEmptyEntries);

            valveParts.Add(parts);
        }

        int[,] weights = new int[valveParts.Count, valveParts.Count];
        //int[,] paths = new int[valveParts.Count, valveParts.Count];
        int[] flows = new int[valveParts.Count];
        string[] names = valveParts.Select(x => x[0]).ToArray();
        int threshold = int.MaxValue / names.Length;

        for (int i = 0; i < valveParts.Count; i++)
        {
            flows[i] = int.Parse(valveParts[i][1]);
            for (int j = 0; j < valveParts.Count; j++)
            {
                if (i == j) 
                { 
                    weights[i, j] = 0;
                    //paths[i, j] = -1;
                    continue; 
                }

                if (valveParts[i][2..].Contains(names[j]))
                {
                    weights[i, j] = 1;
                    //paths[i, j] = i;
                }
                else
                {
                    weights[i, j] = threshold;
                    //paths[i, j] = -1;
                }
            }
        }

        //Floyd-Warshall
        for (int k = 0; k < valveParts.Count; k++)
            for (int i = 0; i < valveParts.Count; i++)
                for (int j = 0; j < valveParts.Count; j++)
                    weights[i, j] = Math.Min(weights[i, j], weights[i, k] + weights[k, j]);

        return (weights, flows, names.TakeWhile(x => x != "AA").Count());
    }

    private int maxPressure(int t, int start, int[,] weights, int[] flows, int[] remain) => 
        (t * flows[start]) + remain
        .Where(x => weights[start, x] < int.MaxValue && t - weights[start, x] - 1 > 0)
        .Select(x => maxPressure(t - weights[start, x] - 1, x, weights, flows,
            remain.Except([x]).ToArray()))
        .Concat([0]).Max();
}