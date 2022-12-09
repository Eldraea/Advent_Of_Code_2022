using Day_9_Rope_Bridge;

IEnumerable<string> input = File.ReadLines("../../../input.txt");

Dictionary<string, (int x, int y)> map = new()
{
    { "U", (0, 1) },
    { "D", (0, -1) },
    { "L", (-1, 0) },
    { "R", (1, 0) },
};

Console.WriteLine($"part1: {GetNumberOfPositions(2)}");
Console.WriteLine($"part2: {GetNumberOfPositions(10)}");


int GetNumberOfPositions(int size)
{
    List<(int x, int y)> rope = Enumerable.Repeat((0, 0), size).ToList();
    HashSet<(int x, int y)> visited = new() { (0, 0) };
    foreach (string line in input)
    {
        var splittedLine = line.Split(' ');
        var direction = splittedLine[0];
        var distance = int.Parse(splittedLine[1]);
        var dirVector = map[direction];
        foreach (var _ in Enumerable.Range(0, distance))
        {
            rope[0] = (rope[0].x + dirVector.x, rope[0].y + dirVector.y);
            for (int i = 1; i < rope.Count; i++)
            {
                var retroDirVector = (x: rope[i - 1].x - rope[i].x, y: rope[i - 1].y - rope[i].y);
                if (Math.Abs(retroDirVector.x) < 2 && Math.Abs(retroDirVector.y) < 2) continue;

                retroDirVector.x /= retroDirVector.x != 0 ? Math.Abs(retroDirVector.x) : 1;
                retroDirVector.y /= retroDirVector.y != 0 ? Math.Abs(retroDirVector.y) : 1;
                rope[i] = (rope[i].x + retroDirVector.x, rope[i].y + retroDirVector.y);
            }
            visited.Add((x: rope.Last().x, y: rope.Last().y));
        }
    }
    return visited.Count();
}
