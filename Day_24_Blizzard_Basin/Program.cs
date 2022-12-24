using Day_24_Blizzard_Basin;
using Nito.Collections;


string[] input = File.ReadAllLines("../../../input.txt").Where(x => !string.IsNullOrEmpty(x)).ToArray();

var maxAbscissa = input[0].Length - 1;
var maxOrdinate = input.Length - 1;
var blizzards = new Blizzard(maxOrdinate, maxAbscissa, input);
var maxBoards = (maxAbscissa - 1) * (maxOrdinate - 1);
var occupiedSpotsByRound = new List<HashSet<(int x, int y)>>();
var currentBlizzards = blizzards;
for (int boardNum = 0; boardNum < maxBoards; ++boardNum)
{
	var newBlizzards = new List<(int x, int y, char direction)>();
	var occupiedSpots = new HashSet<(int x, int y)>();

	foreach (var blizzard in currentBlizzards.blizzardPoints)
	{
		occupiedSpots.Add((blizzard.x, blizzard.y));

		var newBlizzard = blizzard;
		if (blizzard.direction == '<')
		{
			if (--newBlizzard.x < 1)
				newBlizzard.x = maxAbscissa - 1;
		}
		else if (blizzard.direction == 'v')
		{
			if (++newBlizzard.y > maxOrdinate - 1)
				newBlizzard.y = 1;
		}
		else if (blizzard.direction == '>')
		{
			if (++newBlizzard.x > maxAbscissa - 1)
				newBlizzard.x = 1;
		}
		else if (blizzard.direction == '^')
		{
			if (--newBlizzard.y < 1)
				newBlizzard.y = maxOrdinate - 1;
		}

		newBlizzards.Add(newBlizzard);
	}

	occupiedSpotsByRound.Add(occupiedSpots);
	currentBlizzards.blizzardPoints = newBlizzards;
}


var visited = new HashSet<(int x, int y, int step, short stage)>();
var toRun = new Deque<(int x, int y, int step, short stage)>();
toRun.AddToBack((1, 0, 0, 0));
var foundPart1 = false;

while (toRun.Count != 0)
{
	var current = toRun.RemoveFromFront();
	var (x, y, step, stage) = current;
	if (x < 0 || x > maxAbscissa || y < 0 || y > maxOrdinate || input[y][x] == '#')
		continue;

	if (stage == 1 && y == 0)
		stage = 2;

	if (y == maxOrdinate)
	{
		if (stage == 0)
		{
			if (!foundPart1)
			{
				Console.WriteLine($"{step - 1}");
				foundPart1 = true;
			}
			stage = 1;
		}
		else if (stage == 2)
		{
			Console.WriteLine($"{step - 1}");
			break;
		}
	}

	if (visited.Contains(current))
		continue;

	visited.Add(current);

	var obstacles = occupiedSpotsByRound[step % occupiedSpotsByRound.Count];
	if (!obstacles.Contains((x, y)))
		toRun.AddToBack((x, y, step + 1, stage));
	if (!obstacles.Contains((x + 1, y)))
		toRun.AddToBack((x + 1, y, step + 1, stage));
	if (!obstacles.Contains((x - 1, y)))
		toRun.AddToBack((x - 1, y, step + 1, stage));
	if (!obstacles.Contains((x, y + 1)))
		toRun.AddToBack((x, y + 1, step + 1, stage));
	if (!obstacles.Contains((x, y - 1)))
		toRun.AddToBack((x, y - 1, step + 1, stage));
}

