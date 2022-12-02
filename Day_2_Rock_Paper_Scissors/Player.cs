namespace Day_2_Rock_Paper_Scissors
{
    public abstract class Player
    {
        public Dictionary<(char, char), int> PossibleResults;
        public int Score = 0;

        public void Play((char, char) round)
           => Score += PossibleResults[round];
    }
}
