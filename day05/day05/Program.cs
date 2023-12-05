string path = "input.txt";

string[] lines = File.ReadAllLines(path);

var seeds = lines[0].Split(": ")[1].Split(" ").Select(x => Int64.Parse(x));
var seedsRanges = new List<(Int64 seed, Int64 len)>();

for (int i = 0; i < seeds.Count(); i += 2)
{
    seedsRanges.Add((seeds.ElementAt(i), seeds.ElementAt(i + 1)));
}

var maps = new List<List<(Int64 dest, Int64 source, Int64 len)>>();

var currentMap = new List<(Int64 dest, Int64 source, Int64 len)>();

for (int i = 3; i < lines.Length; i++)
{
    if(lines[i] == "")
    {
        i += 1;
        maps.Add(currentMap);
        currentMap = new List<(Int64 dest, Int64 source, Int64 len)>();
    }else if(i == lines.Length - 1)
    {
        var split = lines[i].Split(" ");
        currentMap.Add((Int64.Parse(split[0]), Int64.Parse(split[1]), Int64.Parse(split[2])));
        maps.Add(currentMap);
        currentMap = new List<(Int64 dest, Int64 source, Int64 len)>();
    }
    else
    {
        var split = lines[i].Split(" ");
        currentMap.Add((Int64.Parse(split[0]), Int64.Parse(split[1]), Int64.Parse(split[2])));
    }
}

var finals = new List<Int64>();

foreach (var seed in seeds)
{
    var finalVal = seed;
    foreach (var map in maps)
    {
        foreach (var line in map)
        {
            if (finalVal >= line.source && finalVal <= line.source + line.len)
            {
                //Console.WriteLine($"changing {finalVal} into {line.dest + finalVal - line.source}");
                finalVal = line.dest + finalVal - line.source;
                break;
            }
        }
    }
    finals.Add(finalVal);  
}

Console.WriteLine(finals.Min());

foreach (var map in maps)
{
    var newSeeds = new List<(Int64 seed, Int64 len)>();
    while(seedsRanges.Count > 0)
    {
        var seedRange = seedsRanges.ElementAt(0);
        var linesAll = map.FindAll(x => x.source <= seedRange.seed && x.source + x.len >= seedRange.seed + seedRange.len);
        var situation = 0; //0 - everything fits,1 - only taking last part, 2 - only taking first part, 3 - only taking middle part
        if (linesAll.Count == 0)
        {
            linesAll = map.FindAll(x => x.source >= seedRange.seed && x.source + x.len < seedRange.seed + seedRange.len);
            situation = 3;
        }
        if (linesAll.Count == 0)
        {
            linesAll = map.FindAll(x => x.source >= seedRange.seed && seedRange.seed + seedRange.len > x.source);
            situation = 1;
        }

        if (linesAll.Count == 0)
        {
            linesAll = map.FindAll(x => x.source + x.len > seedRange.seed && x.source < seedRange.seed + seedRange.len);
            situation = 2;
        }
        
        if (linesAll.Count == 0)
        {
            newSeeds.Add(seedRange);
            seedsRanges.RemoveAt(0);
            
        }
        else
        {
            var line = linesAll[0];
            if(situation == 0)
            {
                Int64 newSeed;
                Int64 newLen;
                newSeed = line.dest + seedRange.seed - line.source;
                newLen = seedRange.len;
                newSeeds.Add((newSeed, newLen));
                seedsRanges.RemoveAt(0);

            }
            else if(situation == 1)
            {
                Int64 newSeed;
                Int64 newLen;
                Int64 backSeed;
                Int64 backLen;
                newSeed = line.dest;
                newLen = seedRange.seed + seedRange.len - line.source;
                backSeed = seedRange.seed;
                backLen = seedRange.len - newLen;
                newSeeds.Add((newSeed, newLen));
                seedsRanges.Add((backSeed, backLen));
                seedsRanges.RemoveAt(0);

            }
            else if(situation == 2)
            {
                Int64 newSeed;
                Int64 newLen;
                Int64 backSeed;
                Int64 backLen;
                newLen = line.source + line.len - seedRange.seed;
                newSeed = line.dest + line.len - newLen;
                backSeed = seedRange.seed + newLen;
                backLen = seedRange.len - newLen;
                newSeeds.Add((newSeed, newLen));
                seedsRanges.Add((backSeed, backLen));
                seedsRanges.RemoveAt(0);
            }
            else if(situation == 3)
            {
                Int64 newSeed;
                Int64 newLen;
                Int64 backSeed1;
                Int64 backLen1;
                Int64 backSeed2;
                Int64 backLen2;
                newLen = line.len;
                newSeed = line.dest;
                backSeed1 = seedRange.seed;
                backLen1 = line.source - seedRange.seed;
                backSeed2 = line.source + line.len;
                backLen2 = seedRange.len - newLen - backLen1;
                newSeeds.Add((newSeed, newLen));
                seedsRanges.Add((backSeed1, backLen1));
                seedsRanges.Add((backSeed2, backLen2));
                seedsRanges.RemoveAt(0);
            }
        }
    }
    seedsRanges = newSeeds;
}

var results2 = seedsRanges;
Console.WriteLine(results2.Select(s => s.seed).Min());

