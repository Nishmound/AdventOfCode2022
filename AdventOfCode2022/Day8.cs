internal class Day8 : AdventDay<int>
{
    public int RunP1(StreamReader reader)
    {
        byte[,] heightMap = MakeHeightMap(reader);
        int visibles = 0;

        for (int i = 0; i < heightMap.GetLength(0); i++)
        {
            for (int j = 0; j < heightMap.GetLength(1); j++)
            {
                if (i == 0 || i == heightMap.GetLength(0) - 1 || j == 0 || j == heightMap.GetLength(1) - 1)
                {
                    visibles++;
                    continue;
                }

                bool isBlocked = false;

                for (int k = i - 1; k >= 0 && !isBlocked; k--)
                    if (heightMap[k, j] >= heightMap[i, j]) isBlocked = true;

                if (!isBlocked)
                {
                    visibles++;
                    continue;
                }
                else isBlocked = false;

                for (int k = i + 1; k < heightMap.GetLength(0) && !isBlocked; k++)
                    if (heightMap[k, j] >= heightMap[i, j]) isBlocked = true;

                if (!isBlocked)
                {
                    visibles++;
                    continue;
                }
                else isBlocked = false;

                for (int k = j - 1; k >= 0 && !isBlocked; k--)
                    if (heightMap[i, k] >= heightMap[i, j]) isBlocked = true;

                if (!isBlocked)
                {
                    visibles++;
                    continue;
                }
                else isBlocked = false;

                for (int k = j + 1; k < heightMap.GetLength(1) && !isBlocked; k++)
                    if (heightMap[i, k] >= heightMap[i, j]) isBlocked = true;

                if (isBlocked) continue;
                visibles++;
            }
        }

        return visibles;
    }

    public int RunP2(StreamReader reader)
    {
        byte[,] heightMap = MakeHeightMap(reader);
        int highestScenic = 0;

        for (int i = 0; i < heightMap.GetLength(0); i++)
        {
            for (int j = 0; j < heightMap.GetLength(1); j++)
            {
                int scenic = 1;
                int visibles = 0;

                for (int k = i - 1; k >= 0; k--)
                {
                    visibles++;
                    if (heightMap[k, j] >= heightMap[i, j]) break;
                }

                scenic *= visibles;
                visibles = 0;

                for (int k = i + 1; k < heightMap.GetLength(0); k++)
                {
                    visibles++;
                    if (heightMap[k, j] >= heightMap[i, j]) break;
                }

                scenic *= visibles;
                visibles = 0;

                for (int k = j - 1; k >= 0; k--)
                {
                    visibles++;
                    if (heightMap[i, k] >= heightMap[i, j]) break;
                }

                scenic *= visibles;
                visibles = 0;

                for (int k = j + 1; k < heightMap.GetLength(1); k++)
                {
                    visibles++;
                    if (heightMap[i, k] >= heightMap[i, j]) break;
                }

                scenic *= visibles;
                
                if (scenic > highestScenic) highestScenic = scenic;

            }
        }

        return highestScenic;
    }

    byte[,] MakeHeightMap(StreamReader reader)
    {
        List<byte[]> lineMap = new();

        string? line;
        while ((line = reader.ReadLine()) != null) 
            lineMap.Add(line.Select(c => (byte)(c - '0')).ToArray());

        byte[,] map = new byte[lineMap[0].Length, lineMap.Count];
        
        for (int i = 0; i < lineMap[0].Length; i++)
        {
            for ( int j = 0; j < lineMap.Count; j++)
            {
                map[j, i] = lineMap[j][i];
            }
        }

        return map;
    }
}