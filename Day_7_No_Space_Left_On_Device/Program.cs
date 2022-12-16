using System.Text.RegularExpressions;

string text = File.ReadAllText("../../../input.txt");

Console.WriteLine(GetTotalSizeOfDirectory(text, 1));
Console.WriteLine(GetTotalSizeOfDirectory(text, 2));

int GetTotalSizeOfDirectory(string input, int part)
{
    var currentDirectory = new Stack<string>();
    var directorySizes = new Dictionary<string, int>();
    foreach (var line in input.Split(Environment.NewLine, StringSplitOptions.None))
    {
        if (line == "$ cd ..")
            currentDirectory.Pop();
        else if (line.StartsWith("$ cd"))
            currentDirectory.Push(string.Join("", currentDirectory) + line.Split(" ").Last());
        else if (Regex.Match(line, @"\d+").Success)
        {
            int size;
            int.TryParse(line.Split(" ").First(), out size);
            foreach (var directory in currentDirectory)
                directorySizes[directory] = directorySizes.GetValueOrDefault(directory) + size; 
        }
    }
    if (part == 1)
        return directorySizes.Values.Where(x => x <= 100000).Sum();
    else
    {
        var totalDiskSpace = 70000000 - directorySizes.Values.Max();
        return directorySizes.Values.Where(x => totalDiskSpace + x >= 30000000).Min();
    }
}


