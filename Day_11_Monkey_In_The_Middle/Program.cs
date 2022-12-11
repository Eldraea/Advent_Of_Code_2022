using Day_11_Monkey_In_The_Middle;
using System.Text.RegularExpressions;

string input = File.ReadAllText("../../../input.txt");
Console.WriteLine(PartOne(input));
Console.WriteLine(PartTwo(input));


object PartOne(string input)
{
    var monkeys = ParseMonkeys(input);
    Run(20, monkeys, w => w / 3);
    return GetMonkeyBusinessLevel(monkeys);
}

object PartTwo(string input)
{
    var monkeys = ParseMonkeys(input);
    var mod = monkeys.Aggregate(1, (mod, monkey) => mod * monkey.Mod);
    Run(10_000, monkeys, w => w % mod);
    return GetMonkeyBusinessLevel(monkeys);
}

Monkey[] ParseMonkeys(string input) =>
    input.Split("\r\n\r\n").Select(ParseMonkey).ToArray();

Monkey ParseMonkey(string input)
{
    var monkey = new Monkey();

    foreach (var line in input.Split("\r\n"))
    {
        var tryParse = LineParser(line);
        if (tryParse(@"Monkey (\d+)", out var arg))
        {
            // pass
        }
        else if (tryParse("Starting items: (.*)", out arg))
        {
            monkey.Items = new Queue<long>(arg.Split(", ").Select(long.Parse));
        }
        else if (tryParse(@"Operation: new = old \* old", out _))
        {
            monkey.Operation = old => old * old;
        }
        else if (tryParse(@"Operation: new = old \* (\d+)", out arg))
        {
            monkey.Operation = old => old * int.Parse(arg);
        }
        else if (tryParse(@"Operation: new = old \+ (\d+)", out arg))
        {
            monkey.Operation = old => old + int.Parse(arg);
        }
        else if (tryParse(@"Test: divisible by (\d+)", out arg))
        {
            monkey.Mod = int.Parse(arg);
        }
        else if (tryParse(@"If true: throw to monkey (\d+)", out arg))
        {
            monkey.PassToMonkeyIfDivides = int.Parse(arg);
        }
        else if (tryParse(@"If false: throw to monkey (\d+)", out arg))
        {
            monkey.PassToMonkeyOtherwise = int.Parse(arg);
        }
        else
        {
            throw new ArgumentException(line);
        }
    }
    return monkey;
}

long GetMonkeyBusinessLevel(IEnumerable<Monkey> monkeys) =>
    monkeys
        .OrderByDescending(monkey => monkey.InspectedItems)
        .Take(2)
        .Aggregate(1L, (res, monkey) => res * monkey.InspectedItems);

void Run(int rounds, Monkey[] monkeys, Func<long, long> updateWorryLevel)
{
    for (var i = 0; i < rounds; i++)
    {
        foreach (var monkey in monkeys)
        {
            while (monkey.Items.Any())
            {
                monkey.InspectedItems++;

                var item = monkey.Items.Dequeue();
                item = monkey.Operation(item);
                item = updateWorryLevel(item);

                var targetMonkey = item % monkey.Mod == 0 ?
                    monkey.PassToMonkeyIfDivides :
                    monkey.PassToMonkeyOtherwise;

                monkeys[targetMonkey].Items.Enqueue(item);
            }
        }
    }
}
TryParse LineParser(string line)
{
    bool match(string pattern, out string arg)
    {
        var m = Regex.Match(line, pattern);
        if (m.Success)
        {
            arg = m.Groups[m.Groups.Count - 1].Value;
            return true;
        }
        else
        {
            arg = "";
            return false;
        }
    }
    return match;
}
delegate bool TryParse(string pattern, out string arg);

