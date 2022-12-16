using Day_10_Carhode_Ray_Tube;

string[] input = File.ReadAllText("../../../input.txt").Split(Environment.NewLine, StringSplitOptions.None);
PartOne partOne = new PartOne();
PartTwo partTwo = new PartTwo();

Console.WriteLine(partOne.GetSumOfSignals(input));
partTwo.RenderCRT(input);



