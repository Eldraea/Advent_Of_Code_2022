using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Day_14_Regolith_Reservoir
{
    public class Cave
    {
        bool HasFloor { get; set; }
        Dictionary<Complex, char> Map { get; set; }
        int MaxImaginary { get; set; }

        public Cave(string input, bool hasFloor)
        {
            HasFloor = hasFloor;
            Map = new Dictionary<Complex, char>();

            foreach (var line in input.Split(Environment.NewLine, StringSplitOptions.None))
            {
                var steps = line.Split("->").Select(x => x.Split(",")).Select(x => new Complex(int.Parse(x[0]), int.Parse(x[1]))).ToArray();
                for (var i = 1; i < steps.Length; i++)
                    FillWithRocks(steps[i - 1], steps[i]);
            }
            MaxImaginary = (int)Map.Keys.Select(pos => pos.Imaginary).Max();
        }

        public int FillWithRocks(Complex from, Complex to)
        {
            var direction = new Complex(Math.Sign(to.Real - from.Real),Math.Sign(to.Imaginary - from.Imaginary));

            var steps = 0;
            for (var position = from; position != to + direction; position += direction)
            {
                Map[position] = '#';
                steps++;
            }
            return steps;
        }

        public int FillWithSand(Complex sandSource)
        {
            while (true)
            {
                Complex location = SimulateFallingSand(sandSource);

                if (Map.ContainsKey(location))
                    break;
                if (!HasFloor && location.Imaginary == MaxImaginary + 1)
                    break;
                Map[location] = 'o';
            }
            return Map.Values.Count(x => x == 'o');
        }

        Complex SimulateFallingSand(Complex simulatedSand)
        {
            Complex right = new Complex(1, 1);
            Complex left = new Complex(-1, 1);
            Complex down = new Complex(0, 1);

            while (simulatedSand.Imaginary < MaxImaginary + 1)
            {
                if (!Map.ContainsKey(simulatedSand + down))
                    simulatedSand += down;
                else if (!Map.ContainsKey(simulatedSand + left))
                    simulatedSand += left;
                else if (!Map.ContainsKey(simulatedSand + right))
                    simulatedSand += right;
                else
                    break;
            }
            return simulatedSand;
        }
    }
}
