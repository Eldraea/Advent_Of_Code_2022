using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_2_Rock_Paper_Scissors
{
    public class PlayerForPartOne: IPlayer
    {
        public static Dictionary<(char, char), int> PossibleResults = new()
        {
            { ('A', 'X'), 4 }, { ('A', 'Y'), 8 }, { ('A', 'Z'), 3 },
            { ('B', 'X'), 1 }, { ('B', 'Y'), 5 }, { ('B', 'Z'), 9 },
            { ('C', 'X'), 7 }, { ('C', 'Y'), 2 }, { ('C', 'Z'), 6 },

        };
        public List<int> Scores { get; set; }

        public PlayerForPartOne()
           => Scores = new();

        public void Play((char, char) round)
           => Scores.Add(PossibleResults[round]);

        public int GetTotalScore()
            => Scores.Sum(); 
    }
}
