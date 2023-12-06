string path = "input.txt";

string[] lines = File.ReadAllLines(path);

var times = lines[0].Trim().Split(" ").Skip(1).Where(x => x.Trim() != "").Select(x => int.Parse(x));
var distances = lines[1].Trim().Split(" ").Skip(1).Where(x => x.Trim() != "").Select(x => int.Parse(x));

var result = new List<int>();

for(int i = 0; i < times.Count(); i++)
{
    var time = times.ElementAt(i);
    var distance = distances.ElementAt(i);
    var x1 = Math.Floor(((time - Math.Sqrt(Math.Pow(time, 2) - 4 * distance)) / 2) + 1);
    var x2 = Math.Ceiling(((time + Math.Sqrt(Math.Pow(time, 2) - 4 * distance)) / 2) - 1);
    result.Add((int)(x2 - x1 + 1));
}

Console.WriteLine(result.Aggregate(1, (prev, next) => prev *= next));


var time2 = Int64.Parse(lines[0].Where(x => char.IsDigit(x)).ToArray());
var distance2 = Int64.Parse(lines[1].Where(x => char.IsDigit(x)).ToArray());

var x12 = Math.Floor(((time2 - Math.Sqrt(Math.Pow(time2, 2) - 4 * distance2)) / 2) + 1);
var x22 = Math.Ceiling(((time2 + Math.Sqrt(Math.Pow(time2, 2) - 4 * distance2)) / 2) - 1);
Console.WriteLine(x22 - x12 + 1);