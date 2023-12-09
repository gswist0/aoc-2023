string path = "example.txt";

string[] lines = File.ReadAllLines(path);

int count = 0;
int count2 = 0;

foreach (var line in lines)
{
    var numbers = line.Split(' ').Select(x => int.Parse(x)).ToList();
    var values = new List<List<int>>
    {
        numbers
    };

    bool zerosOnly = false;
    int index = 0;
    while (!zerosOnly)
    {
        var newNumbers = new List<int>();
        var prev = values[index];
        for (int i = 1; i < prev.Count; i++)
        {
            newNumbers.Add(prev[i] - prev[i - 1]);
        }
        if (newNumbers.All(x => x == 0))
        {
            zerosOnly = true;
        }
        else
        {
            index++;
        }
        values.Add(newNumbers);
    }

    values.Last().Add(0);
    values[values.Count - 1] = values.Last().Prepend(0).ToList();

    for (int i = values.Count - 2; i >= 0; i--)
    {
        values[i].Add(values[i].Last() + values[i + 1].Last());
        values[i] = values[i].Prepend(values[i].First() - values[i + 1].First()).ToList();
    }

    count += values[0].Last();
    count2 += values[0].First();
}

Console.WriteLine(count);
Console.WriteLine(count2);