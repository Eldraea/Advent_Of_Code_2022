using Day_5_Supply_Stacks;
using System.Text.RegularExpressions;

string path = "../../../input.txt";
PartOne partOne = new(path);
PartTwo partTwo = new(path);
Console.WriteLine(partOne.Result);
Console.WriteLine(partTwo.Result);


void PrintDictionary(Dictionary<int, List<char>> dictionary)
{
    foreach(List<char> stack in dictionary.Values)
    {
        foreach (char c in stack)
            Console.Write($"{c} ");
        Console.WriteLine();
    }    
}










