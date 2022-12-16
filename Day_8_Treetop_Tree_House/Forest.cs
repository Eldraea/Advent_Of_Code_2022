using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_8_Treetop_Tree_House
{
    public class Forest
    {
        public string[] TreesInForest { get; }
        public int Row { get; }
        public int Column { get; }

        public Forest(string[] trees, int row, int column)
        {
            TreesInForest = trees;
            Row = row;
            Column = column;
        }

        public IEnumerable<Tree> GetTrees()
        {
            var rows = Enumerable.Range(0, Row);
            var columns = Enumerable.Range(0, Column);
            List<Tree> trees = new();

            foreach (int row in rows)
                foreach (int column in columns)
                    trees.Add(new Tree(TreesInForest[row][column], row, column));
            return trees;
        }

        public int GetDistance(Tree tree, Direction direction) =>
            IsTreeTheTallest(tree, direction) 
            ? TreesInDirection(tree, direction).Count() 
            : GetNotWantedTrees(tree, direction).Count() + 1;

        public bool IsTreeTheTallest(Tree tree, Direction direction) =>
            TreesInDirection(tree, direction).All(x => x.Height < tree.Height);

        IEnumerable<Tree> TreesInDirection(Tree tree, Direction direction)
        {
            var (first, row, column) = (true, tree.Row, tree.Column);
            while (row >= 0 && row < Row && column >= 0 && column < Column)
            {
                if (!first)
                    yield return new Tree(TreesInForest[row][column], row, column);
                (first, row, column) = (false, row + direction.Row, column + direction.Column);
            }
        }
        IEnumerable<Tree> GetNotWantedTrees(Tree tree, Direction direction) =>
           TreesInDirection(tree, direction).TakeWhile(x => x.Height < tree.Height);

    }
}



