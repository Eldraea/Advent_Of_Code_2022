namespace Day_2_Rock_Paper_Scissors
{
    public class PlayerForPartOne : Player
    {
       public PlayerForPartOne()
        => PossibleResults = new()
           {
                { ('A', 'X'), 4 }, { ('A', 'Y'), 8 },{ ('A', 'Z'), 3 },
                { ('B', 'X'), 1 }, { ('B', 'Y'), 5 },{ ('B', 'Z'), 9 },
                { ('C', 'X'), 7 }, { ('C', 'Y'), 2 },{ ('C', 'Z'), 6 },
           };
    }
}
