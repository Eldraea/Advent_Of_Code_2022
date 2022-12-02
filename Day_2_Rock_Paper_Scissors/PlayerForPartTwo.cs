using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_2_Rock_Paper_Scissors
{
    public class PlayerForPartTwo: Player
    {
        public PlayerForPartTwo()
        {
            Score = 0;
            PossibleResults = new()
            {
                { ('A', 'X'), 3 }, { ('A', 'Y'), 4 }, { ('A', 'Z'), 8 },
                { ('B', 'X'), 1 }, { ('B', 'Y'), 5 }, { ('B', 'Z'), 9 },
                { ('C', 'X'), 2 }, { ('C', 'Y'), 6 }, { ('C', 'Z'), 7 },
            };

        }
        
    }
}
