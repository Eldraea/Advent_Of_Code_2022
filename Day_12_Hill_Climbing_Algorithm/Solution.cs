using System.Collections.Immutable;

public class Solution
{
    record struct Coord(int lat, int lon);

    record struct Symbol(char value);
    record struct Elevation(char value);
    record struct Poi(Symbol symbol, Elevation elevation, int distanceFromGoal);

    Symbol startSymbol = new Symbol('S');
    Symbol goalSymbol = new Symbol('E');
    Elevation lowestElevation = new Elevation('a');
    Elevation highestElevation = new Elevation('z');

    public object PartOne(string input) =>
        GetPois(input)
            .Single(poi => poi.symbol == startSymbol)
            .distanceFromGoal;

    public object PartTwo(string input) =>
        GetPois(input)
            .Where(poi => poi.elevation == lowestElevation)
            .Select(poi => poi.distanceFromGoal)
            .Min();

    IEnumerable<Poi> GetPois(string input)
    {
        var map = ParseMap(input);
        var goal = map.Keys.Single(point => map[point] == goalSymbol);
        var poiByCoord = new Dictionary<Coord, Poi>() {
            {goal, new Poi(goalSymbol, GetElevation(goalSymbol), 0)}
        };

        var q = new Queue<Coord>();
        q.Enqueue(goal);
        while (q.Any())
        {
            var thisCoord = q.Dequeue();
            var thisPoi = poiByCoord[thisCoord];

            foreach (var nextCoord in Neighbours(thisCoord).Where(map.ContainsKey))
            {
                if (poiByCoord.ContainsKey(nextCoord))
                {
                    continue;
                }

                var nextSymbol = map[nextCoord];
                var nextElevation = GetElevation(nextSymbol);

                if (thisPoi.elevation.value - nextElevation.value <= 1)
                {
                    poiByCoord[nextCoord] = new Poi(
                        symbol: nextSymbol,
                        elevation: nextElevation,
                        distanceFromGoal: thisPoi.distanceFromGoal + 1
                    );
                    q.Enqueue(nextCoord);
                }
            }

        }
        return poiByCoord.Values;
    }

    Elevation GetElevation(Symbol symbol) =>
        symbol.value switch
        {
            'S' => lowestElevation,
            'E' => highestElevation,
            _ => new Elevation(symbol.value)
        };

    ImmutableDictionary<Coord, Symbol> ParseMap(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.None);
        return (
            from y in Enumerable.Range(0, lines.Length)
            from x in Enumerable.Range(0, lines[0].Length)
            select new KeyValuePair<Coord, Symbol>(
                new Coord(x, y), new Symbol(lines[y][x])
            )
        ).ToImmutableDictionary();
    }

    IEnumerable<Coord> Neighbours(Coord coord) =>
        new[] {
           coord with {lat = coord.lat + 1},
           coord with {lat = coord.lat - 1},
           coord with {lon = coord.lon + 1},
           coord with {lon = coord.lon - 1},
        };
}
