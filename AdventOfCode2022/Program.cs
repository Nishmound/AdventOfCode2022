
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
    day1_result = Day1.run_p1(reader);
Console.WriteLine($"The Elf that carries the most calories carries:\n{day1_result}");
write_empty();
Console.WriteLine("--- Part Two ---");
write_empty();
using (var reader = new StreamReader(@"inputs\adventofcode.com_2022_day_1_input.txt"))
    day1_result = Day1.run_p2(reader);
Console.WriteLine($"The top three elves that are carrying the most calories\nare carrying in total:\n{day1_result}");
#endregion

#region Day2
write_empty();
write_sep();
Console.WriteLine("--- Day 2: Rock Paper Scissors ---");
write_empty();
int day2_result;
using (var reader = new StreamReader(@"inputs\adventofcode.com_2022_day_2_input.txt"))
    day2_result = Day2.run_p1(reader);
Console.WriteLine($"If everything goes exactly according to my strategy guide,\nmy total score would be:\n{day2_result}");
write_empty();
Console.WriteLine("--- Part Two ---");
write_empty();
using (var reader = new StreamReader(@"inputs\adventofcode.com_2022_day_2_input.txt"))
    day2_result = Day2.run_p2(reader);
Console.WriteLine($"If everything goes exactly according to my strategy guide,\nmy total score would be:\n{day2_result}");
#endregion

#region Day3
write_empty();
write_sep();
Console.WriteLine("--- Day 3: Rucksack Reorganization ---");
write_empty();
int day3_result;
using (var reader = new StreamReader(@"inputs\adventofcode.com_2022_day_3_input.txt"))
    day3_result = Day3.run_p1(reader);
Console.WriteLine($"The sum of the priorities of all common items is:\n{day3_result}");
write_empty();
Console.WriteLine("--- Part Two ---");
write_empty();
using (var reader = new StreamReader(@"inputs\adventofcode.com_2022_day_3_input.txt"))
    day3_result = Day3.run_p2(reader);
Console.WriteLine($"The sum of the priorities of all badges is:\n{day3_result}");
#endregion

#region Day4
write_empty();
write_sep();
Console.WriteLine("--- Day 4: Camp Cleanup ---");
write_empty();
int day4_result;
using (var reader = new StreamReader(@"inputs\adventofcode.com_2022_day_4_input.txt"))
    day4_result = Day4.run_p1(reader);
Console.WriteLine($"The number of assignment pairs,\nwhere one range fully contains the other, is:\n{day4_result}");
write_empty();
Console.WriteLine("--- Part Two ---");
write_empty();
using (var reader = new StreamReader(@"inputs\adventofcode.com_2022_day_4_input.txt"))
    day4_result = Day4.run_p2(reader);
Console.WriteLine($"The number of assignment pairs,\nwhere one range fully contains the other, is:\n{day4_result}");
#endregion

#region Day5
write_empty();
write_sep();
Console.WriteLine("--- Day 5: Supply Stacks ---");
write_empty();
string day5_result;
using (var reader = new StreamReader(@"inputs\adventofcode.com_2022_day_5_input.txt"))
    day5_result = Day5.run_p1(reader);
Console.WriteLine($"After rearranging, the crates at the top are:\n{day5_result}");
write_empty();
Console.WriteLine("--- Part Two ---");
write_empty();
using (var reader = new StreamReader(@"inputs\adventofcode.com_2022_day_5_input.txt"))
    day5_result = Day5.run_p2(reader);
Console.WriteLine($"After rearranging, the crates at the top are:\n{day5_result}");
#endregion

#region Day6
write_empty();
write_sep();
Console.WriteLine("--- Day 6: Tuning trouble ---");
write_empty();
int day6_result;
using (var reader = new StreamReader(@"inputs\adventofcode.com_2022_day_6_input.txt"))
    day6_result = Day6.run_p1(reader);
Console.WriteLine($"Before the first start-of-packet marker is detected,\n the amount of characters needed to be processed is:\n{day6_result}");
write_empty();
Console.WriteLine("--- Part Two ---");
write_empty();
using (var reader = new StreamReader(@"inputs\adventofcode.com_2022_day_6_input.txt"))
    day6_result = Day6.run_p2(reader);
Console.WriteLine($"Before the first start-of-message marker is detected,\n the amount of characters needed to be processed is:\n{day6_result}");
#endregion

#region Day6
write_empty();
write_sep();
Console.WriteLine("--- Day 7: No Space Left On Device ---");
write_empty();
int day7_result;
using (var reader = new StreamReader(@"inputs\adventofcode.com_2022_day_7_input.txt"))
    day7_result = Day7.run_p1(reader);
Console.WriteLine($"The sum of the total sizes of all directories\nwith a total size of at most 100000 is:\n{day7_result}");
write_empty();
Console.WriteLine("--- Part Two ---");
write_empty();
using (var reader = new StreamReader(@"inputs\adventofcode.com_2022_day_7_input.txt"))
    day7_result = Day7.run_p2(reader);
Console.WriteLine($"The smallest directory that fits the criteria\nhas a total size of:\n{day7_result}");
#endregion

Console.ReadLine();

static void write_sep() => Console.WriteLine(string.Join("", Enumerable.Repeat("#", 50)));
static void write_empty() => Console.WriteLine();