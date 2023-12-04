string path = "input.txt";

string[] lines = File.ReadAllLines(path);


var result = lines.Select(x => x.Split(": ")[1]).Select(x => x.Split(" | ")).Select(x => (x[0].Trim().Split(" ").Where(a => a.Trim() != "").Select(i => int.Parse(i)), x[1].Trim().Split(" ").Where(a => a.Trim() != "").Select(i => int.Parse(i)))).Aggregate(0, (sum, next) =>
{
    var tempSum = 0;
    foreach (var card in next.Item1)
    {
        if (next.Item2.Contains(card))
        {
            if (tempSum == 0)
            {
                tempSum += 1;
            }
            else
            {
                tempSum *= 2;
            }
        }
    }
    return sum + tempSum;
});


Console.WriteLine(result);

var result2 = lines.Select(x => x.Split(": ")[1]).Select(x => x.Split(" | ")).Select(x => (x[0].Trim().Split(" ").Where(a => a.Trim() != "").Select(i => int.Parse(i)), x[1].Trim().Split(" ").Where(a => a.Trim() != "").Select(i => int.Parse(i)))).Aggregate((0, new Dictionary<int, int>()), (prev, next) =>
{
    var index = prev.Item1;
    var dict = prev.Item2;
    if (!dict.ContainsKey(index))
    {
        dict.Add(index, 1);
    }
    var hits = 0;
    foreach (var card in next.Item1)
    {
        if (next.Item2.Contains(card))
        {
            hits += 1;
        }
    }
    for (int num = 0; num < dict[index]; num++)
    {
        for (int i = 1; i <= hits; i++)
        {
            if (!dict.ContainsKey(index + i))
            {
                dict[index + i] = 1;
            }
            dict[index + i] += 1;
        }
    }
    return (index + 1, dict);
}).Item2.Values.Sum();


Console.WriteLine(result2);