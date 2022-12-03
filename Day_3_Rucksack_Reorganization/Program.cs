using Day_3_Rucksack_Reorganization;

string path = "input.txt";
int[] letters = Enumerable.Range(97, 26).Concat(Enumerable.Range(65, 26)).ToArray(); ;

Console.WriteLine($"The sum of the priotities of the item types is equal to {GetTheSumOfRucksacksCommonItemsForPartOne(path)}");
Console.WriteLine($"The sum of the priotities of the item type that corresponds to the badges of each three-Elf group is equal to {GetTheSumOfRucksacksCommonItemsForPartTwo(path)}");

int GetTheSumOfRucksacksCommonItemsForPartOne(string path)
{
    List<Rucksack> rucksacks = new();

    using(StreamReader streamReader = new StreamReader(path))
    {
        string line;
        while((line = streamReader.ReadLine())!=null)
            rucksacks.Add(new Rucksack(line.Substring(0, line.Length/2), line.Substring(line.Length/2)));
    }
    return rucksacks.Select(rucksack => Array.IndexOf(letters, rucksack.CommonItem) + 1).Sum();
}

int GetTheSumOfRucksacksCommonItemsForPartTwo(string path)
{
    List<Group> rucksackGroups = new();
    using (StreamReader streamReader = new StreamReader(path))
    {
        string line;
        while ((line = streamReader.ReadLine()) != null)
            rucksackGroups.Add(new Group(line, streamReader.ReadLine(),streamReader.ReadLine()));
    }
    return rucksackGroups.Select(rucksackGroup => Array.IndexOf(letters, rucksackGroup.CommonItem) +1).Sum();
}


