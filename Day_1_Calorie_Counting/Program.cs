string text = File.ReadAllText("input.txt");
Console.WriteLine(GetTheMaximumTotalCaloriesCarried(text));

int GetTheMaximumTotalCaloriesCarried(string text)
{
    string[] textSplitted = text.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);

    List<int> calories = new List<int>();
    foreach (string line in textSplitted)
    {
        int[] lineSplitted = line.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)
             .Select(x => Int32.Parse(x)).ToArray();
        calories.Add(lineSplitted.Sum());
    }

    return calories.Max();
}









