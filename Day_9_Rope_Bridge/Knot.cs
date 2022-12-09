namespace Day_9_Rope_Bridge
{
    public class Knot
    {
        public int X { get;  set; }
        public int Y { get; set; }

        public Knot(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Move((int x, int y) direction)
        {
            X += direction.x;
            Y += direction.y;
        }
    }
}
