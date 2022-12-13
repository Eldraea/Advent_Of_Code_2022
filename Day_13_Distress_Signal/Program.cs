using System.Text.Json.Nodes;

string input = File.ReadAllText("../../../input.txt");

Console.WriteLine(GetResult(input, 1));
Console.WriteLine(GetResult(input, 2));

int GetResult(string input, int part) 
{
    if(part == 1)
        return GetPackets(input)
           .Chunk(2)
           .Select((jsonNodes, index) => Compare(jsonNodes[0], jsonNodes[1]) < 0 ? index + 1 : 0)
           .Sum();
    var addings = GetPackets("[[2]]\r\n[[6]]").ToList();
    var packets = GetPackets(input).Concat(addings).ToList();
    packets.Sort(Compare);
    return (packets.IndexOf(addings[0]) + 1) * (packets.IndexOf(addings[1]) + 1);
}
       
IEnumerable<JsonNode> GetPackets(string input) =>
    input.Split(Environment.NewLine, StringSplitOptions.None)
        .Where(str => !string.IsNullOrEmpty(str))
        .Select(str => JsonNode.Parse(str));
   

int Compare(JsonNode nodeA, JsonNode nodeB)
{
    if (nodeA is JsonValue && nodeB is JsonValue)
        return (int)nodeA - (int)nodeB;   
    var firstArray = nodeA as JsonArray ?? new JsonArray((int)nodeA);
    var secondArray = nodeB as JsonArray ?? new JsonArray((int)nodeB);
    return Enumerable.Zip(firstArray, secondArray)
            .Select(nodes => Compare(nodes.First, nodes.Second))
            .FirstOrDefault(c => c != 0, firstArray.Count - secondArray.Count);   
}
