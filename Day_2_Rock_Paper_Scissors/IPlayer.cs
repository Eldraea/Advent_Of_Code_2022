using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_2_Rock_Paper_Scissors
{
    public interface IPlayer
    {
        void Play(ValueTuple<char, char> round);
        int GetTotalScore();
    }
}
