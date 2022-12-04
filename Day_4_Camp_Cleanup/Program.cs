string path = "input.txt";


PartOne(path);
PartTwo(path);

void PartOne(string path)
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
            if (containSameTasks.Count() == firstTasks.Count() || containSameTasks.Count() == secondTasks.Count())
                sum++;
        }
    }
    Console.WriteLine(sum);

}

void PartTwo(string path)
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
            if (containSameTasks.Count() !=0)
                sum++;
        } 
    }
    Console.WriteLine(sum);
}
