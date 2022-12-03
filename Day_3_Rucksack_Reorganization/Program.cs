using Day_3_Rucksack_Reorganization;

string path = "input.txt";
int[] letters = Enumerable.Range(97, 26).Concat(Enumerable.Range(65, 26)).ToArray(); ;

Console.WriteLine($"The sum of the priotities of the item types is equal to {GetTheSumOfRucksacksCommonItems(path, 1)}");
Console.WriteLine($"The sum of the priotities of the item type that corresponds to the badges of each three-Elf group is equal to {GetTheSumOfRucksacksCommonItems(path,2)}");

int GetTheSumOfRucksacksCommonItems(string path, int part)
{
    List<int> rucksacksItems = new();
    using(StreamReader streamReader = new StreamReader(path))
    {
        string line;
        if(part == 1)
            while((line = streamReader.ReadLine())!=null)
                rucksacksItems.Add(new Rucksack(line.Substring(0, line.Length/2), line.Substring(line.Length/2)).CommonItem);
        else
            while ((line = streamReader.ReadLine()) != null)
                rucksacksItems.Add(new Rucksack(line, streamReader.ReadLine(), streamReader.ReadLine()).CommonItem);
    }
    return rucksacksItems.Select(rucksackItems => Array.IndexOf(letters, rucksackItems) + 1).Sum();
}



