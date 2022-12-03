namespace Day_3_Rucksack_Reorganization
{
    public class Rucksack
    {
        public int CommonItem { get; set; }
        public Rucksack(string firstCompartment, string secondCompartment)
            => CommonItem = firstCompartment.Where(x => secondCompartment.IndexOf(x) != -1).ToArray()[0];
        public Rucksack(string firstRucksack, string secondRucksack, string thirdRucksack)
            => CommonItem = firstRucksack.Where(x => secondRucksack.IndexOf(x) != -1 && thirdRucksack.IndexOf(x) != -1).ToArray()[0];

    }
}
