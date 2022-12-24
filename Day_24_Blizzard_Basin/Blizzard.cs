using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_24_Blizzard_Basin
{
    public class Blizzard
    {
        public List<(int x, int y, char direction)> blizzardPoints { get; set; }

        public Blizzard(int maxOrdinate, int maxAbscissa, string[] input)
        {
            blizzardPoints = new();
            for (int y = 1; y < maxOrdinate; ++y)
                for (int x = 1; x < maxAbscissa; ++x)
                    if (input[y][x] != '.')
                        blizzardPoints.Add((x, y, input[y][x]));
        }
    }
}
