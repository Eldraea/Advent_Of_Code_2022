﻿string text = File.ReadAllText("input.txt");
List<int> calories = GetTheCaloriesCarriedByElves(text).OrderBy(x => x).ToList();
Console.WriteLine($"The elf who is carring the most calories of food carries {PartOne(calories)} calories");
Console.WriteLine($"The Total of calories carried by the three elves who are carring the most calories of food carry in total {PartTwo(calories)} calories");


int PartOne(List<int> caloriesCarried)
    => calories.Last();

int PartTwo(List<int> caloriesCarried)
    => caloriesCarried.TakeLast(3).Sum();


List<int> GetTheCaloriesCarriedByElves(string text)
{
    string[] textSplitted = text.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);

    List<int> calories = new List<int>();
    foreach (string line in textSplitted)
    {
        int[] lineSplitted = line.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)
             .Select(x => Int32.Parse(x)).ToArray();
        calories.Add(lineSplitted.Sum());
    }
    return calories; ;
}









