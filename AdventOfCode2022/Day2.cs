namespace AdventOfCode2022;
internal class Day2
{
    public static int run_p1(StreamReader reader)
    {
        int score = 0;

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            char opponent = line[0];
            char player = line[2];

            switch (player)
            {
                case 'X':
                    score += 1;
                    switch (opponent)
                    {
                        case 'A':
                            score += 3;
                            break;
                        case 'C':
                            score += 6;
                            break;
                    }
                    break;
                case 'Y':
                    score += 2;
                    switch (opponent)
                    {
                        case 'B':
                            score += 3;
                            break;
                        case 'A':
                            score += 6;
                            break;
                    }
                    break;
                case 'Z':
                    score += 3;
                    switch (opponent)
                    {
                        case 'C':
                            score += 3;
                            break;
                        case 'B':
                            score += 6;
                            break;
                    }
                    break;
            }
        }

        return score;
    }

    public static int run_p2(StreamReader reader)
    {
        int score = 0;

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            char opponent = line[0];
            char player = line[2];

            switch (opponent)
            {
                case 'A':
                    switch (player)
                    {
                        case 'X':
                            score += 0 + 3;
                            break;
                        case 'Y':
                            score += 3 + 1;
                            break;
                        case 'Z':
                            score += 6 + 2;
                            break;
                    }
                    break;
                case 'B':
                    switch (player)
                    {
                        case 'X':
                            score += 0 + 1;
                            break;
                        case 'Y':
                            score += 3 + 2;
                            break;
                        case 'Z':
                            score += 6 + 3;
                            break;
                    }
                    break;
                case 'C':
                    switch (player)
                    {
                        case 'X':
                            score += 0 + 2;
                            break;
                        case 'Y':
                            score += 3 + 3;
                            break;
                        case 'Z':
                            score += 6 + 1;
                            break;
                    }
                    break;
            }
        }

        return score;
    }
}
