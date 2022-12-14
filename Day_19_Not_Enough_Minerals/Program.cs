using System.Text.RegularExpressions;

var regex = new Regex("Blueprint (\\d+): Each ore robot costs (\\d+) ore. Each clay robot costs (\\d+) ore. Each obsidian robot costs (\\d+) ore and (\\d+) clay. Each geode robot costs (\\d+) ore and (\\d+) obsidian.");

var blueprints = File.ReadAllLines("../../../input.txt").Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => new Blueprint(
    int.Parse(regex.Match(x).Groups[1].Value),
    int.Parse(regex.Match(x).Groups[2].Value),
    int.Parse(regex.Match(x).Groups[3].Value),
    int.Parse(regex.Match(x).Groups[4].Value),
    int.Parse(regex.Match(x).Groups[5].Value),
    int.Parse(regex.Match(x).Groups[6].Value),
    int.Parse(regex.Match(x).Groups[7].Value))).ToArray();

int GetMostGeodes(Blueprint blueprint, int time)
{
    var alreadyRun = new HashSet<GameState>();
    var toRun = new Stack<GameState>();
    toRun.Push(new GameState(0, 1, 0, 0, 0, 0, 0, 0, time));
    var best = 0;

    while (toRun.TryPop(out var state))
    {
        if (state.timeLeft == 0)
        {
            best = Math.Max(best, state.geodes);
            continue;
        }

        state.oreMachines = Math.Min(state.oreMachines, blueprint.MaxOreCost);
        state.ore = Math.Min(state.ore, state.timeLeft * blueprint.MaxOreCost - state.oreMachines * (state.timeLeft - 1));
        state.clayMachines = Math.Min(state.clayMachines, blueprint.obsidianCostClay);
        state.clay = Math.Min(state.clay, state.timeLeft * blueprint.obsidianCostClay - state.clayMachines * (state.timeLeft - 1));
        state.obsidianMachines = Math.Min(state.obsidianMachines, blueprint.geodeCostObsidian);
        state.obsidian = Math.Min(state.obsidian, state.timeLeft * blueprint.geodeCostObsidian - state.obsidianMachines * (state.timeLeft - 1));

        if (alreadyRun.Contains(state))
            continue;
        alreadyRun.Add(state);

        if (state.ore >= blueprint.geodeCostOre && state.obsidian >= blueprint.geodeCostObsidian)
            toRun.Push(new GameState(
                state.ore + state.oreMachines - blueprint.geodeCostOre, state.oreMachines,
                state.clay + state.clayMachines, state.clayMachines,
                state.obsidian + state.obsidianMachines - blueprint.geodeCostObsidian, state.obsidianMachines,
                state.geodes + state.geodeMachines, state.geodeMachines + 1,
                state.timeLeft - 1));


        if (state.ore >= blueprint.obsidianCostOre && state.clay >= blueprint.obsidianCostClay)
            toRun.Push(new GameState(
                state.ore + state.oreMachines - blueprint.obsidianCostOre, state.oreMachines,
                state.clay + state.clayMachines - blueprint.obsidianCostClay, state.clayMachines,
                state.obsidian + state.obsidianMachines, state.obsidianMachines + 1,
                state.geodes + state.geodeMachines, state.geodeMachines,
                state.timeLeft - 1));

        if (state.ore >= blueprint.clayCostOre)
            toRun.Push(new GameState(
                state.ore + state.oreMachines - blueprint.clayCostOre, state.oreMachines,
                state.clay + state.clayMachines, state.clayMachines + 1,
                state.obsidian + state.obsidianMachines, state.obsidianMachines,
                state.geodes + state.geodeMachines, state.geodeMachines,
                state.timeLeft - 1));

        if (state.ore >= blueprint.oreCostOre)
            toRun.Push(new GameState(
                state.ore + state.oreMachines - blueprint.oreCostOre, state.oreMachines + 1,
                state.clay + state.clayMachines, state.clayMachines,
                state.obsidian + state.obsidianMachines, state.obsidianMachines,
                state.geodes + state.geodeMachines, state.geodeMachines,
                state.timeLeft - 1));

        toRun.Push(new GameState(
            state.ore + state.oreMachines, state.oreMachines,
            state.clay + state.clayMachines, state.clayMachines,
            state.obsidian + state.obsidianMachines, state.obsidianMachines,
            state.geodes + state.geodeMachines, state.geodeMachines,
            state.timeLeft - 1));
    }
    return best;
}

long GetQualityLevelOfBluePrints(int part)
{
    long qualityLevel;
    if(part == 1)
    {
        qualityLevel = 0L;
        foreach (var blueprint in blueprints)
        {
            var blueprintResult = GetMostGeodes(blueprint, 24);
            qualityLevel += blueprint.id * blueprintResult;
        }
        return qualityLevel;
    }
    qualityLevel = 1L;
    foreach (var blueprint in blueprints.Take(3))
    {
        var blueprintResult = GetMostGeodes(blueprint, 32);
        qualityLevel *= blueprintResult;
    }
    return qualityLevel;

}

Console.WriteLine(GetQualityLevelOfBluePrints(1));
Console.WriteLine(GetQualityLevelOfBluePrints(2));

public class GameState
{
    public int ore;
    public int oreMachines;
    public int clay;
    public int clayMachines;
    public int obsidian;
    public int obsidianMachines;
    public int geodes;
    public int geodeMachines;
    public int timeLeft;

    public GameState(int ore, int oreMachines, int clay, int clayMachines, int obsidian, int obsidianMachines, int geodes, int geodeMachines, int timeLeft)
    {
        this.ore = ore;
        this.oreMachines = oreMachines;
        this.clay = clay;
        this.clayMachines = clayMachines;
        this.obsidian = obsidian;
        this.obsidianMachines = obsidianMachines;
        this.geodes = geodes;
        this.geodeMachines = geodeMachines;
        this.timeLeft = timeLeft;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 27;
            hash = (13 * hash) + ore;
            hash = (13 * hash) + oreMachines;
            hash = (13 * hash) + clay;
            hash = (13 * hash) + clayMachines;
            hash = (13 * hash) + obsidian;
            hash = (13 * hash) + obsidianMachines;
            hash = (13 * hash) + geodes;
            hash = (13 * hash) + geodeMachines;
            return hash;
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is not GameState other)
            return false;

        return
            ore == other.ore &&
            oreMachines == other.oreMachines &&
            clay == other.clay &&
            clayMachines == other.clayMachines &&
            obsidian == other.obsidian &&
            obsidianMachines == other.obsidianMachines &&
            geodes == other.geodes &&
            geodeMachines == other.geodeMachines &&
            timeLeft == other.timeLeft;
    }
}

record class Blueprint(int id, int oreCostOre, int clayCostOre, int obsidianCostOre, int obsidianCostClay, int geodeCostOre, int geodeCostObsidian)
{
    Lazy<int> maxOreCost = new(() => new[] { oreCostOre, clayCostOre, obsidianCostOre, geodeCostOre }.Max());

    public int MaxOreCost => maxOreCost.Value;
}