using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_11_Monkey_In_The_Middle
{
    public class Monkey
    {
        public Queue<long> Items { get; set; }
        public Func<long, long> Operation { get; set; }
        public int InspectedItems { get; set; }
        public int Mod { get; set; }
        public int PassToMonkeyIfDivides { get; set; }
        public int PassToMonkeyOtherwise { get; set; }
    }
}
