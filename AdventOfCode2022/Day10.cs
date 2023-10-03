internal class Day10 : AdventDay<int>
{
    public int RunP1(StreamReader reader)
    {
        int X = 1;
        int cycles = 0;
        int signal = 20;
        int strength = 0;
        
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] command = line.Split(' ');
            if (command[0] == "noop")
            {
                Cycle();
                continue;
            }
            Cycle();
            Cycle();
            X += int.Parse(command[1]);
        }

        return strength;

        void Cycle()
        {
            cycles++;
            if (cycles == signal)
            {
                strength += cycles * X;
                signal += 40;
            }
        }
    }


    public int RunP2(StreamReader reader)
    {
        string screen = String.Empty;
        int X = 1;
        int cycles = 0;

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] command = line.Split(' ');
            if (command[0] == "noop")
            {
                Cycle();
                continue;
            }
            Cycle();
            Cycle();
            X += int.Parse(command[1]);
        }

        Console.WriteLine(screen);
        return 0;

        void Cycle()
        {
            if (Math.Abs(X - (cycles % 40)) <= 1) screen += '#';
            else screen += '.';
            
            cycles++;
            if (cycles % 40 == 0) screen += Environment.NewLine;
        }
    }
}