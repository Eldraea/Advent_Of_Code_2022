using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_8_Treetop_Tree_House
{
    public class Tree
    {
        public int Row { get; }
        public int Column { get; }
        public int Height { get; }


        public Tree(int value, int row, int column)
        {
            Height = value;
            Row = row;
            Column = column;
        }
    }
}

     
