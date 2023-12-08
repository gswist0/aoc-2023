string path = "example.txt";

string[] lines = File.ReadAllLines(path);


var moves = lines[0].Select(c => c == 'L' ? 0 : 1).ToArray();
var data = lines.Skip(2).Select(line => (label: string.Concat(line.Take(3)), left: string.Concat(line.Skip(7).Take(3)), right: string.Concat(line.Skip(12).Take(3)))).ToDictionary(a => a.label, a =>
    {
        var arr = new string[2];
        arr[0] = a.left;
        arr[1] = a.right;
        return arr;
    });

//p1

var current = "AAA";
var count = 0;
while (current != "ZZZ")
{
    current = data[current][moves[count % moves.Length]];
    count++;
}

Console.WriteLine(count);

//p2

Int64 counter(string s)
{
    var current = s;
    var count = 0;
    while (current[2] != 'Z')
    {
        current = data[current][moves[count % moves.Length]];
        count++;
    }
    return count;
}

Int64 findLCM(Int64 a, Int64 b)
{
    Int64 num1;
    Int64 num2;

    if (a > b)
    {
        num1 = a;
        num2 = b;
    }
    else
    {
        num1 = b;
        num2 = a;
    }

    for (Int64 i = 1; i <= num2; i++)
    {
        if ((num1 * i) % num2 == 0)
        {
            return i * num1;
        }
    }
    return num2;
}

var strings = data.Keys.Where(x => x[2] == 'A').ToArray();

var count2 = strings.Select(x => counter(x)).Aggregate((prev, next) =>
{
    return findLCM(prev, next);
});

Console.WriteLine(count2);
