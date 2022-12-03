using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_3_Rucksack_Reorganization
{
    public class Rucksack
    {
        public int CommonItem { get; set; }
        public string FirstCompartment { get; set; }
        public string SecondCompartment { get; set; }

        public Rucksack(string firstCompartment, string secondCompatment)
        {
            FirstCompartment = firstCompartment;
            SecondCompartment = secondCompatment;
            CommonItem = FirstCompartment.Where(x => SecondCompartment.IndexOf(x) != -1).ToArray()[0];
        }
    }
}
