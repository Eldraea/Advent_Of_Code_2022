namespace Day_18_Boiling_Boulders
{
    public class Cube
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        private readonly List<(int, int, int)> Mods = new List<(int, int, int)>()
    {
        (0, 0, 1), (0, 0, -1), (1, 0, 0), (-1, 0, 0), (0, 1, 0), (0, -1, 0)
    };


        public Cube(string input)
        {
            var split = input.Split(',');
            X = int.Parse(split[0]);
            Y = int.Parse(split[1]);
            Z = int.Parse(split[2]);
        }

        public Cube(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int FindOpenSides(Dictionary<string, Cube> cubes)
            => Mods.Select(i => !cubes.ContainsKey($"{X + i.Item1},{Y + i.Item2},{Z + i.Item3}")? 1 : 0).Sum();

        public List<Cube> GetNeighbors()
           => Mods.Select(i => new Cube(X + i.Item1, Y + i.Item2, Z + i.Item3)).ToList();
        
        public override string ToString()
            => $"{X},{Y},{Z}";
        
    }
}
