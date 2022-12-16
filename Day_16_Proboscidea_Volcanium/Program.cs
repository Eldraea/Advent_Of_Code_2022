using Day_16_Proboscidea_Volcanium;


Console.WriteLine(PartOne());
Console.WriteLine(PartTwo());


string PartOne()
{
    string filePath = @"../../../input.txt";
    var max = CalculateMaxPressureReleased(filePath, false);

    return max.ToString();
}


string PartTwo()
{
    string filePath = @"../../../input.txt";
    var max = CalculateMaxPressureReleased(filePath, true);

    return max.ToString();
}

long CalculateMaxPressureReleased(string filePath, bool useHelp)
{
    var valves = SetUpValves(filePath);

    var usedValveKeyRates = valves.Values.Where(v => v.ReleasePressure > 0).Select(v => (v.Name, v.ReleasePressure))
        .ToArray();

    if (useHelp)
    {
        var pressureReleasedWithHelp = GetReleasePressureWithHelp(new[] { 26, 26 }, usedValveKeyRates,
            new[] { "AA", "AA" }, valves);
        return pressureReleasedWithHelp;
    }

    var pressureReleased = GetReleasedPressure(30, usedValveKeyRates, "AA", valves);
    return pressureReleased;
}

long GetReleasedPressure(int timeToGo, (string name, int rate)[] usedValveKeyRates, string startingValveKey, Dictionary<string, Valve> valves)
{
    long best = 0;
    var current = valves[startingValveKey];


    foreach (var t in usedValveKeyRates)
    {
        int newTimeToGo = timeToGo - current.StepsToReach[t.name] - 1;
        if (newTimeToGo > 0)
        {

            long gain = newTimeToGo * t.rate + GetReleasedPressure(newTimeToGo, usedValveKeyRates.Where(c => c.name != t.name).ToArray(), t.name, valves);
            if (best < gain) best = gain;
        }
    }

    return best;
}

long GetReleasePressureWithHelp(int[] timeToGo, (string name, int rate)[] usedValveKeyRates,
    string[] startingValveKey, Dictionary<string, Valve> valves)
{
    long best = 0;
    var actorIndex = timeToGo[0] > timeToGo[1] ? 0 : 1;

    var current = valves[startingValveKey[actorIndex]];

    foreach (var t in usedValveKeyRates)
    {
        int newTimeToGo = timeToGo[actorIndex] - current.StepsToReach[t.name] - 1;
        if (newTimeToGo > 0)
        {
            var newTimes = new[] { newTimeToGo, timeToGo[1 - actorIndex] };
            var newNames = new[] { t.name, startingValveKey[1 - actorIndex] };            long gain = newTimeToGo * t.rate + GetReleasePressureWithHelp(newTimes, usedValveKeyRates.Where(c => c.name != t.name).ToArray(), newNames, valves);
            if (best < gain) best = gain;
        }
    }

    return best;
}

Dictionary<string, Valve> SetUpValves(string filePath)
{
    var valves = ParseFileToList(filePath, line => new Valve(line))
        .ToDictionary(v => v.Name, v => v);

    foreach (var key in valves.Keys)
    {
        var current = valves[key];
        foreach (var childKey in current.ConnectedValveNames)
        {
            var child = valves[childKey];
            if (!child.Connections.Exists(c => c.Name == current.Name))
                child.Connections.Add(current);

            if (!current.Connections.Exists(c => c.Name == child.Name))
                current.Connections.Add(child);
        }
    }

    foreach (var key in valves.Keys)
    {
        var stepsToOthers = new Dictionary<string, int>();
        foreach (var otherKey in valves.Keys.Where(ok => ok != key))
        {
            var steps = MinStepToOtherUsingBfs(valves[key], valves[otherKey]);
            if (steps != null)
                stepsToOthers.Add(otherKey, steps.Value);
        }

        valves[key].StepsToReach = stepsToOthers;
    }

    return valves;
}

int? MinStepToOtherUsingBfs(Valve start, Valve target)
{
    var previous = new Dictionary<string, Valve>();

    var queue = new Queue<(Valve, int)>();
    queue.Enqueue((start, 0));

    while (queue.Any())
    {
        var current = queue.Dequeue();
        if (current.Item1.Name == target.Name)
            return current.Item2;

        foreach (var connection in current.Item1.Connections)
        {

            if (previous.ContainsKey(connection.Name))
                continue;

            previous[connection.Name] = connection;
            queue.Enqueue((connection, current.Item2 + 1));
        }
    }
    return null;
}
List<T> ParseFileToList<T>(string filePath, Func<string, T> parser)
{
    List<T> splits = new List<T>();
    string line;
    StreamReader file = new StreamReader(filePath);

    while ((line = file.ReadLine()!) != null)
    {
        splits.Add(parser(line));
    }
    file.Close();
    return splits;
}