using Day_6_Tuning_Trouble;

string path = "../../../input.txt";
string text = File.ReadLines(path).First();

Day_6 daySix = new(text, 2);
Console.WriteLine(daySix.Result);

