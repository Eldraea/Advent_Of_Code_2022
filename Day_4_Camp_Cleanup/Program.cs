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
            int firstNumber = int.Parse(line[0].Substring(0, Math.Abs(0 - line[0].IndexOf('-'))));
            int secondNumber = int.Parse(line[0].Substring(line[0].IndexOf('-')+1));
            int thirdNumber = int.Parse(line[1].Substring(0, Math.Abs(0 - line[1].IndexOf('-'))));
            int fourthNumber = int.Parse(line[1].Substring(line[1].IndexOf('-')+1));
            bool doesOverlap = (firstNumber >= thirdNumber && firstNumber <= fourthNumber) || (thirdNumber >= firstNumber && thirdNumber <= secondNumber);
            bool doesFullyOverlap = (firstNumber >= thirdNumber && firstNumber <= fourthNumber && secondNumber <= fourthNumber) || (thirdNumber >= firstNumber && thirdNumber <= secondNumber && fourthNumber <= secondNumber);
            if(part == 1 && doesFullyOverlap)
                sum++;
            else if (part == 2 && doesOverlap)
                sum++;
        }
    }
    return sum;
}

