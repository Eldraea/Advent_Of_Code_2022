
using System.Collections.Immutable;

string input = File.ReadAllText("../../../input.txt");
Point point = new Point();

Console.WriteLine(point.PartOne(input));
Console.WriteLine(point.PartTwo(input));