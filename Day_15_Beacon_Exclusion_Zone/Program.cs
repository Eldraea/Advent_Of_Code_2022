using System.Text.RegularExpressions;

string input = File.ReadAllText("../../../input.txt");
Console.WriteLine(PartOne(input));
Console.WriteLine(PartTwo(input));

int PartOne(string input)
{
    var pairing = Parse(input).ToArray();
    var y = 2000000;
    var res = 0;
    var rects = pairing.Select(pair => pair.ToRect()).ToArray();
    var left = rects.Select(r => r.Left).Min();
    var right = rects.Select(r => r.Right).Max();


    for (var x = left; x <= right; x++)
    {
        var pos = new Coordinate(x, y);
        if (pairing.Any(pair => pair.beacon != pos && pair.InRange(pos)))
            res++;
    }
    return res;
}

long PartTwo(string input)
{
    var pairing = Parse(input).ToArray();
    var area = GetUncoveredAreas(pairing, new Rectangle(0, 0, 4000001, 4000001)).First();
    return area.X * 4000000L + area.Y;
}

IEnumerable<AssembleOfSensorBeacon> Parse(string input)
{
    foreach (var line in input.Split("\n"))
    {
        var numbers = Regex.Matches(line, "-?[0-9]+").Select(m => int.Parse(m.Value)).ToArray();
        yield return new AssembleOfSensorBeacon(new Coordinate(numbers[0], numbers[1]),new Coordinate(numbers[2], numbers[3]));
    }
}

IEnumerable<Rectangle> GetUncoveredAreas(AssembleOfSensorBeacon[] pairing, Rectangle rect)
{
    if (rect.Width == 0 || rect.Height == 0)
        yield break;
   
    foreach (var pair in pairing)
        if (rect.Corners.All(corner => pair.InRange(corner)))
            yield break;
    
    if (rect.Width == 1 && rect.Height == 1)
    {
        yield return rect;
        yield break;
    }

    foreach (var rectT in rect.Split())
        foreach (var area in GetUncoveredAreas(pairing, rectT))
            yield return area;
}
record struct Coordinate(int X, int Y);
record struct AssembleOfSensorBeacon(Coordinate sensor, Coordinate beacon)
{
    public int Radius = Manhattan(sensor, beacon);

    public bool InRange(Coordinate pos) => Manhattan(pos, sensor) <= Radius;

    public Rectangle ToRect() =>
         new Rectangle(sensor.X - Radius, sensor.Y - Radius, 2 * Radius + 1, 2 * Radius + 1);

    static int Manhattan(Coordinate c1, Coordinate c2)
        =>  Math.Abs(c1.X - c2.X) + Math.Abs(c1.Y - c2.Y);
}

record struct Rectangle(int X, int Y, int Width, int Height)
{
    public int Left => X;
    public int Right => X + Width - 1;
    public int Top => Y;
    public int Bottom => Y + Height - 1;

    public IEnumerable<Coordinate> Corners
    {
        get
        {
            yield return new Coordinate(Left, Top);
            yield return new Coordinate(Right, Top);
            yield return new Coordinate(Right, Bottom);
            yield return new Coordinate(Left, Bottom);
        }
    }
    public IEnumerable<Rectangle> Split()
    {
        var w0 = Width / 2;
        var w1 = Width - w0;
        var h0 = Height / 2;
        var h1 = Height - h0;
        yield return new Rectangle(Left, Top, w0, h0);
        yield return new Rectangle(Left + w0, Top, w1, h0);
        yield return new Rectangle(Left, Top + h0, w0, h1);
        yield return new Rectangle(Left + w0, Top + h0, w1, h1);
    }
}