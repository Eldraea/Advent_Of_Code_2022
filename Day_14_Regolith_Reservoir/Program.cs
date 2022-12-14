using Day_14_Regolith_Reservoir;
using System.Numerics;

string input = File.ReadAllText("../../../input.txt");
var partOne =  new Cave(input, false).FillWithSand(new Complex(500, 0));
var partTwo = new Cave(input, true).FillWithSand(new Complex(500, 0));

Console.WriteLine(partOne);
Console.WriteLine(partTwo);


