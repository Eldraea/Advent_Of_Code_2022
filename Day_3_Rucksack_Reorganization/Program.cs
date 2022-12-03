using Day_3_Rucksack_Reorganization;

string path = "input.txt";

Console.WriteLine($"The sum of the priotities of these item stacks is equal to {GetTheSumOfRuckstacksCommonItemsForPartOne(path)}");
Console.WriteLine($"The sum of the priotities of these item groupstacks is equal to {GetTheSumOfRuckstacksCommonItemsForPartTwo(path)}");

int GetTheSumOfRuckstacksCommonItemsForPartOne(string path)
{
    int[] letters = Enumerable.Range(97, 26).Concat(Enumerable.Range(65, 26)).ToArray();
    List<Rucksack> ruckstacks = new();

    using(StreamReader streamReader = new StreamReader(path))
    {
        string line;
        while((line = streamReader.ReadLine())!=null)
        {
            ruckstacks.Add(new Rucksack(line.Substring(0, line.Length/2), line.Substring(line.Length/2)));
            
        }
    }
    return ruckstacks.Select(ruckstack => Array.IndexOf(letters, ruckstack.CommonItem) + 1).Sum();
}

int GetTheSumOfRuckstacksCommonItemsForPartTwo(string path)
{
    int[] letters = Enumerable.Range(97, 26).Concat(Enumerable.Range(65, 26)).ToArray();
    List<Group> ruckstackGroups = new();
    using (StreamReader streamReader = new StreamReader(path))
    {
        string line;
        while ((line = streamReader.ReadLine()) != null)
        {
           string line2 = streamReader.ReadLine();
           string line3 = streamReader.ReadLine();
            ruckstackGroups.Add(new Group(line, line2, line3));
        }
        
    }
    return ruckstackGroups.Select(ruckstackGroup => Array.IndexOf(letters, ruckstackGroup.CommonItem) +1).Sum();
}


