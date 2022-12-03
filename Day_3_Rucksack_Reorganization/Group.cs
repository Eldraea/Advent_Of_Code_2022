using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_3_Rucksack_Reorganization
{
    public class Group
    { 
        public string FirstRuckStack { get; set; }
        public string SecondRuckStack { get; set; }
        public string ThirdRuckstack { get; set; }
        public int CommonItem { get; set; }

        public Group(string firstRucktack, string secondRuckstack, string thirdRuckstack)
        {
            FirstRuckStack = firstRucktack;
            SecondRuckStack = secondRuckstack;
            ThirdRuckstack = thirdRuckstack;
            CommonItem = FirstRuckStack.Where(x => SecondRuckStack.IndexOf(x) != -1 && ThirdRuckstack.IndexOf(x) != -1).ToArray()[0];
        }

    }
}
