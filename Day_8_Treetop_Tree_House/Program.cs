
using Day_8_Treetop_Tree_House;

var forestMap = File.ReadAllLines("../../../input.txt");

Direction left = new Direction(0, -1);
Direction right = new Direction(0, 1);
Direction top = new Direction(-1, 0);
Direction bottom = new Direction(1, 0);
Forest forest = new(forestMap, forestMap[0].Length, forestMap.Length);

var resultPartOne = forest.GetTrees().Count(x =>
           forest.IsTallest(x, top) || forest.IsTallest(x, bottom) ||
           forest.IsTallest(x, right) || forest.IsTallest(x, bottom));
var resultPartTwo = forest.GetTrees().Select(x =>
            forest.ViewDistance(x, top) * forest.ViewDistance(x, bottom) *
            forest.ViewDistance(x, right) * forest.ViewDistance(x, left)
        ).Max();

Console.WriteLine(resultPartOne);
Console.WriteLine(resultPartTwo);







