namespace Day_10_Carhode_Ray_Tube
{
    public class PartOne
    {
        public int Cycle { get; set; }
        public int Value { get; set; }
        
        public PartOne()
        {
            Cycle = 1;
            Value = 1;
        }

        public int GetSumOfSignals(string[] input)
        {
            int sumOfSignals = 0;
            foreach (string line in input)
            {
                var splitted = line.Split(' ');
                if (splitted.Length == 1)
                    Cycle++;
                else
                {
                    if ((Cycle + 1 - 20) % 40 == 0)
                        sumOfSignals += Value * (Cycle + 1);
                    Value += int.Parse(splitted[1]);
                    Cycle += 2;
                }

                if ((Cycle - 20) % 40 == 0)
                    sumOfSignals += Value * Cycle;
            }
            return sumOfSignals;
        }
    }
}
