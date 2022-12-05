using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day_5_Supply_Stacks
{
   
    public class PartTwo
    {
        public string Result { get; }

        public PartTwo(string path)
        {
            Result = GetTopOfStacks(FillTheDictionary(CreateDictionary(GetInputSize(path)), path));
        }

        int GetInputSize(string path)
        {
            string line;
            using (StreamReader streamReader = new StreamReader(path))
            {
                while ((line = streamReader.ReadLine()) is not null)
                    if (line[1] == '1')
                        break;
            }
            return (int)Char.GetNumericValue(line[line.Length - 2]);
        }

        Dictionary<int, List<char>> CreateDictionary(int size)
        {
            Dictionary<int, List<char>> crates = new();
            for (int i = 1; i <= size; i++)
                crates.Add(i, new List<char>());
            return crates;
        }
        void RearrangeDictionary(Dictionary<int, List<char>> crates, int number, int source, int destination)
        {
            if (number == 1)
            {

                crates[destination].Insert(0, crates[source].First());
                crates[source].Remove(crates[source].First());
            }
            else
            {
                List<char> stack = new();
                for (int i = 1; i <= number; i++)
                {
                    stack.Add(crates[source].First());
                    crates[source].Remove(crates[source].First());
                }
                crates[destination].InsertRange(0, stack);
            }
        }

        Dictionary<int, List<char>> FillTheDictionary(Dictionary<int, List<char>> dictionary, string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                string line;

                while ((line = streamReader.ReadLine()) is not null && line[1] != '1')
                {
                    int j = 1;
                    for (int i = 1; i < line.Length; i = i + 4)
                    {
                        if (line[i] != ' ')
                            dictionary[j].Add(line[i]);
                        j++;
                    }

                }
                streamReader.ReadLine();
                while ((line = streamReader.ReadLine()) is not null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;
                    else
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        RearrangeDictionary(dictionary, int.Parse(numbers[1]), int.Parse(numbers[2]), int.Parse(numbers.Last()));
                    }
                }
            }
            return dictionary;
        }
        string GetTopOfStacks(Dictionary<int, List<char>> stacks)
        {
            string result = "";
            foreach (List<char> stack in stacks.Values)
                result += stack.First();
            return result;

        }
    }
}
