string path = "input.txt";

Console.WriteLine($"The number of pairs that fully overlap is equal to {GetTheSumOfOverLappingPairs(path, 1)}");
Console.WriteLine($"The number of pairs that  overlap is equal to {GetTheSumOfOverLappingPairs(path, 2)}");

int GetTheSumOfOverLappingPairs(string path, int part)
{
    int sum = 0;
    using (StreamReader streamReader = new StreamReader(path))
    {
        string tasks;
        while ((tasks = streamReader?.ReadLine())is not null)
        {
            var line = tasks.Split(',');
            var firstNumbers = line[0].Split('-', 2, StringSplitOptions.None).Select(x => int.Parse(x)).ToArray();
            var secondNumbers = line[1].Split('-',2,  StringSplitOptions.None).Select(x => int.Parse(x)).ToArray();
            bool doesOverlap = (firstNumbers[0] >= secondNumbers[0] && firstNumbers[0] <= secondNumbers[1]) 
                || (secondNumbers[0] >= firstNumbers[0] && secondNumbers[0] <= firstNumbers[1]);
            bool doesFullyOverlap = (firstNumbers[0] >= secondNumbers[0] && firstNumbers[0] <= secondNumbers[1] && firstNumbers[1] <= secondNumbers[1])
                || (secondNumbers[0] >= firstNumbers[0] && secondNumbers[0] <= firstNumbers[1] && secondNumbers[1] <= firstNumbers[1]);
            if((part == 1 && doesFullyOverlap) || (part == 2 && doesOverlap))
                sum++;
        }
    }
    return sum;
}

