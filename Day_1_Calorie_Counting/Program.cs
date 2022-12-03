string path = "input.txt";

Console.WriteLine($"The elf who is carring the most calories of food carries {GetTheTotalCaloriesCarriedByElves(path, 1)} calories");
Console.WriteLine($"The Total of calories carried by the three elves who are carring the most calories of food carry in total {GetTheTotalCaloriesCarriedByElves(path, 2)} calories");


int GetTheTotalCaloriesCarriedByElves(string path, int part)
{
    int sum = 0;
    List<int> calories = new List<int>();
    using (StreamReader sr = new StreamReader(path))
    {
        string caloriesReport;
        while ((caloriesReport =sr.ReadLine())!= null)
        {
            if (string.IsNullOrWhiteSpace(caloriesReport))
            {
                calories.Add(sum);
                sum = 0;
            }
            else
                sum += int.Parse(caloriesReport);
        }
    }
    calories.Sort();

    return part == 1
        ?calories.Last()
        :calories.TakeLast(3).Sum();
}