string path = "input.txt";

Dictionary<(char, char), int> PossibleResultForPartOne = new()
{
    { ('A', 'X'), 4 }, { ('A', 'Y'), 8 }, { ('A', 'Z'), 3 },
    { ('B', 'X'), 1 }, { ('B', 'Y'), 5 }, { ('B', 'Z'), 9 },
    { ('C', 'X'), 7 }, { ('C', 'Y'), 2 }, { ('C', 'Z'), 6 },
};

Dictionary<(char, char), int> PossibleResultForPartTwo = new()
{
    { ('A', 'X'), 3 }, { ('A', 'Y'), 4 }, { ('A', 'Z'), 8 },
    { ('B', 'X'), 1 }, { ('B', 'Y'), 5 }, { ('B', 'Z'), 9 },
    { ('C', 'X'), 2 }, { ('C', 'Y'), 6 }, { ('C', 'Z'), 7 },
};

Console.WriteLine($"The total Score for part one is {GetMyTotalScore(path, 1)}");
Console.WriteLine($"The total score for part two is {GetMyTotalScore(path, 2)}");

int GetMyTotalScore(string path, int part) {
    int score = 0;
    using (StreamReader stream = new StreamReader(path))
    {
        string round;
        while ((round = stream.ReadLine()) != null)
            if (part == 1)
                score += PossibleResultForPartOne[(round[0], round[2])];
            else
                score += PossibleResultForPartTwo[(round[0], round[2])];
    }
    return score;
}
