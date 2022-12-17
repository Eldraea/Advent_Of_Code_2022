using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_17_Pyroclastic_Flow
{
    public class Board
    {
        public char[,] tower;
        public int Height = 0;

        public int[] wtab = { 4, 3, 3, 1, 2 };
        public int[,] BottomTab = { { 4, 4, 4, 4 }, { 3, 4, 3, 0 }, { 4, 4, 4, 0 }, { 4, 0, 0, 0 }, { 4, 4, 0, 0 } };
        public int[,] RightTab = { { 0, 0, 0, 4 }, { 0, 2, 3, 2 }, { 0, 3, 3, 3 }, { 1, 1, 1, 1 }, { 0, 0, 2, 2 } };
        public int[,] LeftTab = { { 0, 0, 0, 1 }, { 0, 2, 1, 2 }, { 0, 3, 3, 1 }, { 1, 1, 1, 1 }, { 0, 0, 1, 1 } };
        public int[,] TopTab = { { 4, 4, 4, 4 }, { 3, 2, 3, 0 }, { 4, 4, 2, 0 }, { 1, 0, 0, 0 }, { 3, 3, 0, 0 } };

        public Board(int maxHeight)
        {
            tower = new char[maxHeight, 7];
            for (int y = 0; y < maxHeight; y++)
                for (int x = 0; x < 7; x++)
                    tower[y, x] = ' ';
        }

        public bool CheckCollisionsOnOrdinates(int position, int abscissa, int ordinate)
        {
            if (ordinate == 3) 
                return true;
            for (int i = 0; i < 4; i++)
                if (BottomTab[position, i] > 0 && tower[ordinate - BottomTab[position, i], abscissa + i] == '#') 
                    return true;
            return false;
        }
       
        public void Drop(int p, int abscissa, int ordinate)
        {
            for (int col = 0; col < 4; col++)
            {
                if (TopTab[p, col] > 0)
                {
                    if (ordinate - TopTab[p, col] + 2 >= Height)
                        Height = ordinate - TopTab[p, col] + 2;
                    for (int row = TopTab[p, col]; row <= BottomTab[p, col]; row++)
                        tower[ordinate - row + 1, abscissa + col] = '#';
                }
            }
        }
        public bool CheckCollisionsOnAbsissa(int position, int abscissa, int ordinate, int[,] array)
        {
            if (abscissa < 0 || abscissa > 7 - wtab[position]) return true;
            for (int i = 0; i < 4; i++)
                if (array[position, i] > 0 && tower[ordinate - i, abscissa + array[position, i] - 1] == '#')
                    return true;
            return false;
        }

        public bool CheckAbscissa(int abscissa, int position)
          => abscissa < 0 || abscissa > 7 - wtab[position];
    }
}
