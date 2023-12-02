string path = "input.txt";

string[] lines = File.ReadAllLines(path);

var dict = new Dictionary<string, int>{
    {"blue", 14},
    {"red", 12},
    {"green", 13}
};

var result = lines.Select(x => x.Split(":")).Select(x => (int.Parse(x[0].Split(" ")[1]), x[1].Split(";"))).Where(x => x.Item2.All(p => p.Split(",").All(j => int.Parse(j.Trim().Split(" ")[0]) <= dict[j.Trim().Split(" ")[1]]))).Select(x => x.Item1).Sum();

Console.WriteLine(result);

var result2 = lines.Select(x => x.Split(":")).Select(x => x[1].Replace(';', ',').Split(",")).Select(x => x.Aggregate((0, 0, 0), (fewest, next) =>
  {
      var color = next.Trim().Split(" ")[1];
      var number = int.Parse(next.Trim().Split(" ")[0]);
      switch (color)
      {
          case "blue":
              fewest.Item1 = number >= fewest.Item1 ? number : fewest.Item1;
              break;
          case "red":
              fewest.Item2 = number >= fewest.Item2 ? number : fewest.Item2;
              break;
          case "green":
              fewest.Item3 = number >= fewest.Item3 ? number : fewest.Item3;
              break;
      }
      return fewest;
  })).Select(x => x.Item1*x.Item2*x.Item3).Sum();

Console.WriteLine(result2);
