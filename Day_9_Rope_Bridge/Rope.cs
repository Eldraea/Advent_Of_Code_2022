namespace Day_9_Rope_Bridge
{
    public class Rope
    {
        public Knot Head { get { return Knots[0]; } }
        public List<Knot> Knots = new();
        readonly HashSet<(int, int)> tailVisited = new();
        readonly Dictionary<string, (int, int)> directions = new()
        {
            { "U", (0, -1) },
            { "R", (1, 0) },
            { "D", (0, 1) },
            { "L", (-1, 0) },
        };

        public Rope(int knotsCount, int X = 0, int Y = 0)
        {
            for (int i = 0; i < knotsCount; i++)
                Knots.Add(new Knot(X,Y));
            tailVisited.Add((X, Y));
        }

        public void MoveHead(string direction, int steps)
        {
            var currentDirection = directions[direction];

            for (int i = 0; i < steps; i++)
            {
                Head.Move(currentDirection);

                for (int j = 1; j < Knots.Count; j++)
                {
                    var current = Knots[j];
                    var previous = Knots[j - 1];
                    MoveTail(previous, current);

                    if (current == Knots[^1])
                        tailVisited.Add((current.X, current.Y)); 
                }
            }
        }

        public int Visited() => tailVisited.Count();

        public static bool KnotsTouching(Knot head, Knot tail) => Math.Abs(head.X - tail.X) < 2 && Math.Abs(head.Y - tail.Y) < 2;

        public static void MoveTail(Knot head, Knot tail)
        {
            if (!KnotsTouching(head, tail))
            {
                var diffX = Math.Clamp(head.X - tail.X, -1, 1);
                var diffY = Math.Clamp(head.X - tail.Y, -1, 1);
                tail.X += diffX;
                tail.Y += diffY;
            }
        }
    }
}
