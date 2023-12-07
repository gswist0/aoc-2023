using day07;

string path = "input.txt";

string[] lines = File.ReadAllLines(path);

var cardMappings = new Dictionary<char, int>
{
    {'A', 14 },
    {'K', 13 },
    {'Q', 12 },
    {'J', 11 },
    {'T', 10 },
    {'9', 9 },
    {'8', 8 },
    {'7', 7 },
    {'6', 6 },
    {'5', 5 },
    {'4', 4 },
    {'3', 3 },
    {'2', 2 },
};

var cardMappings2 = new Dictionary<char, int>
{
    {'A', 14 },
    {'K', 13 },
    {'Q', 12 },
    {'T', 10 },
    {'9', 9 },
    {'8', 8 },
    {'7', 7 },
    {'6', 6 },
    {'5', 5 },
    {'4', 4 },
    {'3', 3 },
    {'2', 2 },
    {'J', 1 }
};


Figures checkFigure(string set, bool part2 = false)
{
    //part2
    var jokerCount = set.Count(x => x == 'J');
    if (!part2)
    {
        jokerCount = 0;
    }
    //

    if(set.Distinct().Count() == 5)
    {
        if(jokerCount == 1)
        {
            return Figures.para;
        }
        return Figures.wysoka;
    }
    if(set.Distinct().Count() == 4)
    {
        if(jokerCount > 0)
        {          
            return Figures.trojka;
        }
        return Figures.para;
    }
    if (set.Distinct().Count() == 1)
    {
        return Figures.poker;
    }
    if (set.Distinct().Count() == 2)
    {
        if(jokerCount > 0)
        {
            return Figures.poker;
        }
        var count = set.Count(x => x == set[0]);
        if (count == 2 || count == 3)
        {
            return Figures.full;
        }
        else
        {
            return Figures.kareta;
        }
    }
    foreach(char c in set)
    {
        if(set.Count(x => x == c) == 3)
        {
            if(jokerCount > 0)
            {
                return Figures.kareta;
            }
            return Figures.trojka;
        }
    }
    if(jokerCount == 1)
    {
        return Figures.full;
    }
    if(jokerCount == 2)
    {
        return Figures.kareta;
    }
    return Figures.dwiepary;
}


var result = lines.ToList();
var result2 = lines.ToList();

result.Sort((x, y) =>
{
    var figureX = checkFigure(x.Split(" ")[0]);
    var figureY = checkFigure(y.Split(" ")[0]);
    if (figureX == figureY)
    {
        for (int i = 0; i < 5; i++)
        {
            var cardX = cardMappings[x[i]];
            var cardY = cardMappings[y[i]];
            if (cardX > cardY)
            {
                return 1;
            }
            if (cardX < cardY)
            {
                return -1;
            }
        }
    }
    else if (figureX > figureY)
    {
        return -1;
    }
    return 1;
});

result2.Sort((x, y) =>
{
    var figureX = checkFigure(x.Split(" ")[0], true);
    var figureY = checkFigure(y.Split(" ")[0], true);
    if (figureX == figureY)
    {
        for (int i = 0; i < 5; i++)
        {
            var cardX = cardMappings2[x[i]];
            var cardY = cardMappings2[y[i]];
            if (cardX > cardY)
            {
                return 1;
            }
            if (cardX < cardY)
            {
                return -1;
            }
        }
    }
    else if (figureX > figureY)
    {
        return -1;
    }
    return 1;
});


var resultP1 = 0;

for(int i = 0; i < result.Count(); i++)
{
    resultP1 += int.Parse(result[i].Split(" ")[1]) * (i + 1);
}

Console.WriteLine(resultP1);

var resultP2 = 0;

for (int i = 0; i < result2.Count(); i++)
{
    resultP2 += int.Parse(result2[i].Split(" ")[1]) * (i + 1);
}

Console.WriteLine(resultP2);