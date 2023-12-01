string path = "input.txt";

string[] lines = File.ReadAllLines(path);

var result1 = lines.Select(x => x.Where(c => char.IsDigit(c)).ToArray()).Select(x => int.Parse($"{x.First()}{x.Last()}")).Sum();

Dictionary<string, string> numbersDict = new Dictionary<string, string>{
    {"one" , "1"},
    {"two", "2"},
    {"three", "3"},
    {"four", "4"},
    {"five", "5"},
    {"six", "6"},
    {"seven", "7"},
    {"eight", "8"},
    {"nine", "9"}
};

var lines2 = new List<int>();

foreach (var line in lines){
    int indexFirst = line.Length;
    int indexLast = 0;
    string first = "";
    string last = "";
    
    foreach (var num in numbersDict.Keys){
        if(!line.Contains(num)) continue;

        if(line.IndexOf(num) < indexFirst){
            indexFirst = line.IndexOf(num);
            first = numbersDict[num];
        }
        if(line.LastIndexOf(num) > indexLast){
            indexLast = line.LastIndexOf(num);
            last = numbersDict[num];
        }
    }
    for (int i = 1; i <= 9; i++){
        if(!line.Contains(i.ToString())) continue;
        if(line.IndexOf(i.ToString()) < indexFirst){
            indexFirst = line.IndexOf(i.ToString());
            first = i.ToString();
        }
        if(line.LastIndexOf(i.ToString()) > indexLast){
            indexLast = line.LastIndexOf(i.ToString());
            last = i.ToString();
        }
    }

    if(last == "") last = first;

    int str = int.Parse($"{first}{last}");

    lines2.Add(str);
}

var result2 = lines2.Sum();

Console.WriteLine(result1);
Console.WriteLine(result2);
