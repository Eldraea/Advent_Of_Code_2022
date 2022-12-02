﻿namespace Day_2_Rock_Paper_Scissors
{
    public class PlayerForPartTwo: Player
    {
        public PlayerForPartTwo()
            => PossibleResults = new()
                {
                    { ('A', 'X'), 3 }, { ('A', 'Y'), 4 }, { ('A', 'Z'), 8 },
                    { ('B', 'X'), 1 }, { ('B', 'Y'), 5 }, { ('B', 'Z'), 9 },
                    { ('C', 'X'), 2 }, { ('C', 'Y'), 6 }, { ('C', 'Z'), 7 },
                };   
    }
}
