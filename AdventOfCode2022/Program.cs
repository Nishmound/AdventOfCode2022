
using AdventOfCode2022;

Console.WriteLine("Advent of Code\n$year=2022;");
write_empty();

#region Day1
write_empty();
write_sep();
Console.WriteLine("--- Day 1: Calorie Counting ---");
write_empty();
int day1_result;
using (var reader = new StreamReader(@"inputs\adventofcode.com_2022_day_1_input.txt"))
    day1_result = Day1.run(reader);
Console.WriteLine($"The Elf that carries the most calories carries:\n{day1_result}");
#endregion

#region Day2
write_empty();
write_sep();
Console.WriteLine("--- Day 2: Rock Paper Scissors ---");
write_empty();
int day2_result;
using (var reader = new StreamReader(@"inputs\adventofcode.com_2022_day_2_input.txt"))
    day2_result = Day2.run(reader);
Console.WriteLine($"If everything goes exactly according to my strategy guide,\nmy total score would be:\n{day2_result}");
#endregion

#region Day3
write_empty();
write_sep();
Console.WriteLine("--- Day 3: Rucksack Reorganization ---");
write_empty();
int day3_result;
using (var reader = new StreamReader(@"inputs\adventofcode.com_2022_day_3_input.txt"))
    day3_result = Day3.run(reader);
Console.WriteLine($"The sum of the priorities of all common items is:\n{day3_result}");
#endregion

Console.ReadLine();

static void write_sep() => Console.WriteLine(string.Join("", Enumerable.Repeat("#", 50)));
static void write_empty() => Console.WriteLine();