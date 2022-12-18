using Day_18_Boiling_Boulders;

string input = "../../../input.txt";
Console.WriteLine(GetSurfaceAreaOfScannedLavaDroplets(input, 1));
Console.WriteLine(GetSurfaceAreaOfScannedLavaDroplets(input, 2));


int GetSurfaceAreaOfScannedLavaDroplets(string input, int part)
    => part == 1 ? CountOpenSides(input) : CountOutsideSidesWithFlood(input);


 int CountOpenSides(string filePath)
{
    var cubes = ParseFile(filePath, (line => line, line => new Cube(line)));
    return cubes.Select(c => c.Value.FindOpenSides(cubes)).Sum();
}


int CountOutsideSidesWithFlood(string filePath)
{
    var cubes = ParseFile(filePath, (line => line, line => new Cube(line)));
    var minX = cubes.Min(c => c.Value.X);
    var minY = cubes.Min(c => c.Value.Y);
    var minZ = cubes.Min(c => c.Value.Z);
    var maxX = cubes.Max(c => c.Value.X);
    var maxY = cubes.Max(c => c.Value.Y);
    var maxZ = cubes.Max(c => c.Value.Z);

    var xRange = Enumerable.Range(minX, maxX + 1).ToList();
    var yRange = Enumerable.Range(minY, maxY + 1).ToList();
    var zRange = Enumerable.Range(minZ, maxZ + 1).ToList();

    return cubes.SelectMany(c => c.Value.GetNeighbors())
        .Where(c => IsOutside(c, cubes, xRange, yRange, zRange)).Count();
}

bool IsOutside(Cube cube, Dictionary<string, Cube> cubes, List<int> xRange, List<int> yRange, List<int> zRange)
{
    if (cubes.ContainsKey(cube.ToString()))
        return false;

    var checkedCubes = new HashSet<string>();

    var queue = new Queue<Cube>();
    queue.Enqueue(cube);

    while (queue.Any())
    {
        var currentCube = queue.Dequeue();

        if (checkedCubes.Contains(currentCube.ToString()))
            continue;
        checkedCubes.Add(currentCube.ToString());

        if (!xRange.Contains(currentCube.X) || !yRange.Contains(currentCube.Y) || !zRange.Contains(currentCube.Z))
            return true;

        if (!cubes.ContainsKey(currentCube.ToString()))
            foreach (var neighbor in currentCube.GetNeighbors())
                queue.Enqueue(neighbor);  
    }
    return false;
}

Dictionary<U, T> ParseFile<U, T>(string filePath, (Func<string, U>, Func<string, T>) parser) where U : notnull
{
    Dictionary<U, T> results = new Dictionary<U, T>();

    using (StreamReader file = new StreamReader(filePath))
        while (file.ReadLine()! is { } line)
            results.Add(parser.Item1(line), parser.Item2(line));
    return results;
}
