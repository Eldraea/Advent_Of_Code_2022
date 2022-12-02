using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_2_Rock_Paper_Scissors
{
    public abstract class Player
    {
        public Dictionary<(char, char), int> PossibleResults;
        public int Score { get; set; }

        public void Play((char, char) round)
           => Score += PossibleResults[round];

        public int GetScore()
            => Score;
    }
}
