string path = "inputTest.txt";

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
            var line = tasks.Replace(',', ' ').Replace('-', ' ').Split(' ').Select(x => int.Parse(x)).ToArray();
            if((part == 1 && DoesFullyOverlap(line) || part == 2 && DoesOverlap(line)))
                sum++;
        }
    }
    return sum;
}

bool DoesOverlap(int[] line)
    => (line[0] >= line[1] && line[0] <= line[3]) || (line[2] >= line[0] && line[2] <= line[1]);

bool DoesFullyOverlap(int[] line)
    => (line[0] >= line[2] && line[0] <= line[3] && line[1] <= line[3]) 
              || (line[2] >= line[0] && line[2] <= line[1] && line[3] <= line[1]);

