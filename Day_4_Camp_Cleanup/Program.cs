string path = "input.txt";

Console.WriteLine($"The number of pairs that fully overlap is equal to {GetTheSumOfOverLappingPairs(path, 1)}");
Console.WriteLine($"The number of pairs that  overlap is equal to {GetTheSumOfOverLappingPairs(path, 2)}");

int GetTheSumOfOverLappingPairs(string path, int part)
{
    int sum = 0;
    using (StreamReader streamReader = new StreamReader(path))
    {
        string line;
        while ((line = streamReader.ReadLine()) is not null)
        {
            var lineSplitted = line.Split(',');
            int firstNumber = int.Parse(lineSplitted[0].Substring(0, lineSplitted[0].IndexOf('-'))) - 1;
            int secondNumber = int.Parse(lineSplitted[0].Substring(lineSplitted[0].IndexOf('-') + 1));
            int thirdNumber = int.Parse(lineSplitted[1].Substring(0, lineSplitted[1].IndexOf('-'))) - 1;
            int fourthNumber = int.Parse(lineSplitted[1].Substring(lineSplitted[1].IndexOf('-') + 1));
            var firstTasks = Enumerable.Range(firstNumber, secondNumber - firstNumber).ToArray();
            var secondTasks = Enumerable.Range(thirdNumber, fourthNumber - thirdNumber).ToArray();
            var containSameTasks = firstTasks.Where(x => Array.IndexOf(secondTasks, x) != -1);
            if (part == 1 && (containSameTasks.Count() == firstTasks.Count() || containSameTasks.Count() == secondTasks.Count()))
                sum++;
            else if (part == 2 && (containSameTasks.Count() != 0))
                sum++;
        }
    }
    return sum;
}

