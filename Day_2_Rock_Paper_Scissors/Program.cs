using Day_2_Rock_Paper_Scissors;

string path = "input.txt";

PlayerForPartOne playerForPartOne = new();
PlayerForPartTwo playerForPartTwo = new();


Console.WriteLine($"The total Score for part one is {GetMyTotalScore(path, playerForPartOne)}");
Console.WriteLine($"The total score for part two is {GetMyTotalScore(path, playerForPartTwo)}");


int GetMyTotalScore(string path, Player player) {

    using (StreamReader stream = new StreamReader(path))
    {
        string line;
        while ((line = stream.ReadLine()) != null)
            player.Play(new ValueTuple<char, char>(line[0], line[2]));  
    }
    return player.Score;
}
