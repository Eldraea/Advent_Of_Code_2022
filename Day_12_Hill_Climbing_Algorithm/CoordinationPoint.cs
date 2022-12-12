using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_12_Hill_Climbing_Algorithm
{
    public class CoordinationPoint
    {
        public int Latitude { get; }
        public int Longitude { get; }

        public CoordinationPoint(int latitude, int longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
