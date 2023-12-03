string path = "input.txt";

string[] lines = File.ReadAllLines(path);

var gears = new Dictionary<(int, int), List<int>>();

bool checkAround(int line, int start, int length)
{
    var points = new List<char>();
    var coords = new List<(int, int)>();
    if(line > 0)
    {
        for (int i = start; i < start + length; i++)
        {
            points.Add(lines[line - 1][i]);
            coords.Add((line - 1, i));
        }
        if(start > 0)
        {
            points.Add(lines[line - 1][start - 1]);
            coords.Add((line - 1, start - 1));
        }
        if(start + length < lines[line].Length)
        {
            points.Add(lines[line - 1][start + length]);
            coords.Add((line - 1, start + length));
        }
    }
    if(start > 0)
    {
        points.Add(lines[line][start -1]);
        coords.Add((line, start - 1));
    }
    if (start + length < lines[line].Length)
    {
        points.Add(lines[line][start + length]);
        coords.Add((line, start + length));
    }
    if(line < lines.Length - 1)
    {
        for (int i = start; i < start + length; i++)
        {
            points.Add(lines[line + 1][i]);
            coords.Add((line + 1, i));
        }
        if (start > 0)
        {
            points.Add(lines[line + 1][start - 1]);
            coords.Add((line + 1, start - 1));
        }
        if (start + length < lines[line].Length)
        {
            points.Add(lines[line + 1][start + length]);
            coords.Add((line + 1, start + length));
        }
    }

    for (int i = 0; i < points.Count; i++)
    {
        if(points[i] == '*')
        {
            var number = "";
            for (int j = start; j < start + length; j++)
            {
                number += lines[line][j];
            }
            if (gears.ContainsKey(coords[i]))
            {
                gears[coords[i]].Add(int.Parse(number));
            }
            else
            {
                gears[coords[i]] = new List<int>();
                gears[coords[i]].Add(int.Parse(number));
            }
        }
    }

    return !points.All(x => x == '.' || char.IsDigit(x));
}

var sum = 0;

for (int i = 0; i < lines.Length; i++)
{
    var line = lines[i];
    var current = "";
    var len = 0;
    var index = 0;
    for(int j = 0; j < line.Length; j++)
    {
        var c = line[j];
        if(j == line.Length - 1 && len != 0 && char.IsDigit(c))
        {
            current += c;
            var check = checkAround(i, index, len + 1);
            if (check)
            {
                sum += int.Parse(current);
            }
            index = 0;
            len = 0;
            current = "";
        }    
        else if (char.IsDigit(c))
        {
            if (len == 0)
            {
                index = j;
            }
            len += 1;
            current += c;
        }
        else
        {
            if(len != 0)
            {
                var check = checkAround(i, index, len);                
                if (check)
                { 
                    sum += int.Parse(current);
                }
                index = 0;
                len = 0;
                current = "";
            }
        }
    }
}

var sum2 = 0;

foreach(var gear in gears)
{
    if(gear.Value.Count == 2)
    {
        sum2 += gear.Value[0] * gear.Value[1];
    }
}

Console.WriteLine(sum);
Console.WriteLine(sum2);
