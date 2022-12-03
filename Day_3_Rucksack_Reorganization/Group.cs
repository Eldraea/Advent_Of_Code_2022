namespace Day_3_Rucksack_Reorganization
{
    public class Group
    { 
        public string FirstRucksack { get; set; }
        public string SecondRucksack { get; set; }
        public string ThirdRucksack { get; set; }
        public int CommonItem { get; set; }

        public Group(string firstRucktack, string secondRuckstack, string thirdRuckstack)
        {
            FirstRucksack = firstRucktack;
            SecondRucksack = secondRuckstack;
            ThirdRucksack = thirdRuckstack;
            CommonItem = FirstRucksack.Where(x => SecondRucksack.IndexOf(x) != -1 && ThirdRucksack.IndexOf(x) != -1).ToArray()[0];
        }

    }
}
