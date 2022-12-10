IEnumerable<string> input = File.ReadLines("../../../input.txt");
Part1();
Part2();

void Part2()
{
    int cycle = 0;
    int value = 1;
    List<char> crt = new List<char>();

    if (Math.Abs(cycle % 40 - value) < 2)
        crt.Add('#');
    else
        crt.Add('.');

    foreach (string line in input)
    {
        var splitted = line.Split(' ');
        if (splitted.Length == 2)
        {
            cycle ++;
            if (Math.Abs(cycle % 40 - value) < 2)
                crt.Add('#');
            else
                crt.Add('.');
            value += int.Parse(splitted[1]);
        }
        cycle ++;
        if (Math.Abs(cycle % 40 - value) < 2)
            crt.Add('#');
        else
            crt.Add('.');
    }
    Console.WriteLine($"part2:");
    Console.WriteLine(String.Join('\n', crt.Chunk(40).Select(v => new String(v))));
}

void Part1()
{
    int cycle = 1;
    int value = 1;
    int solution = 0;
    foreach (string line in input)
    {
        var splitted = line.Split(' ');
        if (splitted.Length == 1)
            cycle++;
        
        else
        {
            if ((cycle + 1 - 20) % 40 == 0)
                solution += value * (cycle + 1);

            value += int.Parse(splitted[1]);
            cycle += 2;
        }

        if ((cycle - 20) % 40 == 0)
            solution += value * cycle;
    }
    Console.WriteLine($"part1: {solution}");
}
