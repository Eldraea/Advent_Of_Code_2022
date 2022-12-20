long[] input = File.ReadAllLines("../../../input.txt").Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => long.Parse(x)).ToArray();


long ComputeResult(LinkedListNode<long>[] nodes, LinkedList<long> linkedList)
{
	var result = 0L;
	var targetNode = nodes.Where(n => n.Value == 0L).Single();

	for (int i = 0; i < 3; ++i)
	{
		var moveCount = 1000 % linkedList.Count;
		while (moveCount-- > 0)
			targetNode = targetNode!.Next ?? linkedList.First;
		result += targetNode!.Value;
	}
	return result;
}

void MixFile(LinkedListNode<long>[] nodes, LinkedList<long> linkedList)
{
	foreach (var item in nodes)
	{
		var moveQuantity = item.Value % (nodes.Length - 1);

		if (moveQuantity > 0)
		{
			var after = item.Next ?? linkedList.First;
			linkedList.Remove(item);

			while (moveQuantity-- > 0)
				after = after!.Next ?? linkedList.First;
			linkedList.AddBefore(after!, item);
		}
		else if (moveQuantity < 0)
		{
			var before = item.Previous ?? linkedList.Last;
			linkedList.Remove(item);

			while (moveQuantity++ < 0)
				before = before!.Previous ?? linkedList.Last;

			linkedList.AddAfter(before!, item);
		}
	}
}

long GetTheSumOfNumbersFormingGroveCoordinates(int part)
{
	var linkedList = new LinkedList<long>();
	var nodes= input.Select(d => new LinkedListNode<long>(d)).ToArray(); 
	if (part == 1)
    {	
		foreach (var item in nodes)
			linkedList.AddLast(item);
		MixFile(nodes, linkedList);
		return ComputeResult(nodes, linkedList);
	}
	nodes = input.Select(d => new LinkedListNode<long>(d * 811589153)).ToArray();
	foreach (var item in nodes)
		linkedList.AddLast(item);

	for (int i = 0; i < 10; ++i)
		MixFile(nodes, linkedList);

	return ComputeResult(nodes, linkedList);
}

Console.WriteLine($"{GetTheSumOfNumbersFormingGroveCoordinates(1)}");
Console.WriteLine($"{GetTheSumOfNumbersFormingGroveCoordinates(2)}");