string path = "input.txt";
List<int> calories = GetTheCaloriesCarriedByElves(path);

Console.WriteLine($"The elf who is carring the most calories of food carries {PartOne(calories)} calories");
Console.WriteLine($"The Total of calories carried by the three elves who are carring the most calories of food carry in total {PartTwo(calories)} calories");

int PartOne(List<int> caloriesCarried)
    => calories.Last();

int PartTwo(List<int> caloriesCarried)
    => caloriesCarried.TakeLast(3).Sum();

List<int> GetTheCaloriesCarriedByElves(string path)
{
    int sum = 0;
    List<int> calories = new List<int>();
    using (StreamReader sr = new StreamReader(path))
    {
        string line;
        while ((line =sr.ReadLine())!= null)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                calories.Add(sum);
                sum = 0;
            }
            else
                sum += int.Parse(line);
        }
    }
    calories.Sort();

    return calories; ;
}