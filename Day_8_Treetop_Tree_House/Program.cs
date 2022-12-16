using Day_8_Treetop_Tree_House;

var forestMap = File.ReadAllLines("../../../input.txt");

Direction left = new Direction(0, -1);
Direction right = new Direction(0, 1);
Direction top = new Direction(-1, 0);
Direction bottom = new Direction(1, 0);
Forest forest = new(forestMap, forestMap[0].Length, forestMap.Length);

Console.WriteLine(GetNumberOfVisibleTrees(forest));
Console.WriteLine(GetHighestScenicScoreForEachTree(forest));

int GetNumberOfVisibleTrees(Forest forest)
    => forest.GetTrees().Count(x =>
           forest.IsTreeTheTallest(x, top) || forest.IsTreeTheTallest(x, bottom) ||
           forest.IsTreeTheTallest(x, right) || forest.IsTreeTheTallest(x, bottom));


int GetHighestScenicScoreForEachTree(Forest forest)
    => forest.GetTrees().Select(x =>
            forest.GetDistance(x, top) * forest.GetDistance(x, bottom) *
            forest.GetDistance(x, right) * forest.GetDistance(x, left)
        ).Max();










