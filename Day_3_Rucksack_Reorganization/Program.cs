string path = "input.txt";
int[] letters = Enumerable.Range(97, 26).Concat(Enumerable.Range(65, 26)).ToArray(); ;

Console.WriteLine($"The sum of the priotities of the item types is equal to {GetTheSumOfRucksacksCommonItems(path, 1)}");
Console.WriteLine($"The sum of the priotities of the item type that corresponds to the badges of each three-Elf group is equal to {GetTheSumOfRucksacksCommonItems(path,2)}");

int GetTheSumOfRucksacksCommonItems(string path, int part)
{
    List<int> rucksacksItems = new();
    using(StreamReader streamReader = new StreamReader(path))
    {
        string rucksack;
        while ((rucksack = streamReader.ReadLine()) != null)
        {
            if (part == 1)
                rucksacksItems.Add(GetCommonItem(rucksack.Substring(0, rucksack.Length / 2), rucksack.Substring(rucksack.Length / 2)));
            else
                rucksacksItems.Add(GetCommonItem(rucksack, streamReader.ReadLine(), streamReader.ReadLine()));
        }
    }
    return rucksacksItems.Select(rucksackItems => Array.IndexOf(letters, rucksackItems) + 1).Sum();
}

int GetCommonItem(string firstRucksack, string secondRucksack, string? thirdRucksack = null)
{
    return thirdRucksack is not null
       ? firstRucksack.Where(x => secondRucksack.IndexOf(x) != -1 && thirdRucksack?.IndexOf(x) != -1).ToArray()[0]
       : firstRucksack.Where(x => secondRucksack.IndexOf(x) != -1).ToArray()[0];
}



