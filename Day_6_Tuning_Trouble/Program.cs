string path = "../../../input.txt";
string text = File.ReadLines(path).First();

Console.WriteLine(GetNumberOfCharactersToBeProcessed(text, 1));
Console.WriteLine(GetNumberOfCharactersToBeProcessed(text, 2));

int GetNumberOfCharactersToBeProcessed(string input, int part)
{
    int marker = part == 1 
        ? 4 
        : 14;
    for (var i = 0; i < input.Length; i++)
    {
        var charactersBeforeMarker = input.Substring(i, marker).ToCharArray();
        if (charactersBeforeMarker.Length == charactersBeforeMarker.Distinct().Count())
            return i + marker;
    }
    return -1;
}

