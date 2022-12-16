
string[] input = File.ReadAllText("../../../input.txt").Split(Environment.NewLine, StringSplitOptions.None);

Dictionary<string, (int x, int y)> map = new()
{
    { "U", (0, 1) },
    { "D", (0, -1) },
    { "L", (-1, 0) },
    { "R", (1, 0) },
};

Console.WriteLine(GetNumberOfPositions(input, 2));
Console.WriteLine(GetNumberOfPositions(input, 10));


int GetNumberOfPositions(string[] input, int size)
{
    List<(int x, int y)> rope = Enumerable.Repeat((0, 0), size).ToList();
    HashSet<(int x, int y)> visited = new() { (0, 0) };
    foreach (string line in input)
    {
        var splittedLine = line.Split(' ');
        var direction = splittedLine[0];
        var distance = int.Parse(splittedLine[1]);
        var mapDirection = map[direction];
        foreach (var _ in Enumerable.Range(0, distance))
        {
            rope[0] = (rope[0].x + mapDirection.x, rope[0].y + mapDirection.y);
            for (int i = 1; i < rope.Count; i++)
            {
                var secondMapDirection = (x: rope[i - 1].x - rope[i].x, y: rope[i - 1].y - rope[i].y);
                if (Math.Abs(secondMapDirection.x) < 2 && Math.Abs(secondMapDirection.y) < 2)
                    continue;
                secondMapDirection.x /= secondMapDirection.x != 0 
                    ? Math.Abs(secondMapDirection.x) : 
                    1;
                secondMapDirection.y /= secondMapDirection.y != 0 
                    ? Math.Abs(secondMapDirection.y) 
                    : 1;
                rope[i] = (rope[i].x + secondMapDirection.x, rope[i].y + secondMapDirection.y);
            }
            visited.Add((x: rope.Last().x, y: rope.Last().y));
        }
    }
    return visited.Count();
}
