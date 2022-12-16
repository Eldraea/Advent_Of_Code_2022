namespace Day_10_Carhode_Ray_Tube
{
    public class PartTwo
    {
        public int Cycle { get; set; }
        public int Value { get; set; }
        public List<char> CRT { get; set; }

        public PartTwo()
        {
            Cycle = 0;
            Value = 1;
            CRT = new List<char>();
        }

        public void RenderCRT(string[] input)
        {
            if (Math.Abs(Cycle % 40 - Value) < 2)
                CRT.Add('#');
            else
                CRT.Add('.');

            foreach (string line in input)
            {
                var splitted = line.Split(' ');
                if (splitted.Length == 2)
                {
                    Cycle++;
                    if (Math.Abs(Cycle % 40 - Value) < 2)
                        CRT.Add('#');
                    else
                        CRT.Add('.');
                    Value += int.Parse(splitted[1]);
                }
                Cycle++;
                if (Math.Abs(Cycle % 40 - Value) < 2)
                    CRT.Add('#');
                else
                    CRT.Add('.');
            }
            Console.WriteLine(String.Join("\r\n'", CRT.Chunk(40).Select(v => new String(v))));
        }
    }
}
