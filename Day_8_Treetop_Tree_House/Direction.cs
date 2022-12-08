using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_8_Treetop_Tree_House
{
    public class Direction
    {
        public int Row { get; }
        public int Column { get; }

        public Direction(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
