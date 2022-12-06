using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_6_Tuning_Trouble
{
    public class Day_6
    {
        public int Result { get; }
        public Day_6(string input, int part)
         => Result = GetResult(input, part);
     
        private int GetResult(string input, int part)
        {
                int marker = part == 1 ? 4 : 14;
                for (var i = 0; i < input.Length; i++)
                {
                    var charactersBeforeMarker = input.Substring(i, marker).ToCharArray();
                    if (charactersBeforeMarker.Length == charactersBeforeMarker.Distinct().Count())
                        return i + marker;   
                }

                return -1;
        } 
        
    }
}

