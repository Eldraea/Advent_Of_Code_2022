using System.Diagnostics;
using System.Text;


string[] input = File.ReadAllLines("../../../input.txt").Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

var mapping = new Dictionary<char, long>
{
	{ '=', -2 },
	{ '-', -1 },
	{ '0', 0 },
	{ '1', 1 },
	{ '2', 2 }
};

var reverseMapping = mapping.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

long FromSNAFU(string value)
{
	var place = 1L;
	var result = 0L;

	foreach (var digit in value.Reverse())
	{
		result += (place * mapping[digit]);
		place *= 5L;
	}

	return result;
}

string ToSnafu(long value)
{
	var digitStack = new Stack<char>();

	while (value > 0)
	{
		digitStack.Push(reverseMapping[((value + 2) % 5) - 2]);
		value = (value + 2) / 5;
	}

	var result = new StringBuilder();
	foreach (var c in digitStack)
		result.Append(c);

	return result.ToString();
}

string GetSNAFUNumber()
{
	var result = 0L;
	foreach (var line in input)
	{
		var value = FromSNAFU(line);
		result += value;
	}
	return ToSnafu(result);
}

Console.WriteLine($"{GetSNAFUNumber()}");