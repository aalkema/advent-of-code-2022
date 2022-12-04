using System.Collections;

using FileStream fs = File.OpenRead("input.txt");
using var sr = new StreamReader(fs);

Dictionary<char, int> priorityDictionary = GetPriorityDictionary();
int totalPriority = 0;
int totalBadgePriority = 0;
string line;
int groupCounter = 0;
List<char[]> group = new List<char[]>();
while ((line = sr.ReadLine()) != null) {
    char[] letters = line.ToArray();
    totalPriority += GetPriorityOfItemsInBothCompartments(letters, priorityDictionary);

    if (groupCounter == 3) {
        totalBadgePriority += GetGroupBadgePriority(group, priorityDictionary);
        group = new List<char[]>();
        group.Add(letters);
        groupCounter = 1;
    } else {
        group.Add(letters);
        groupCounter++;
    }
}
totalBadgePriority += GetGroupBadgePriority(group, priorityDictionary);
// 2531 is too low
Console.WriteLine($"Priority of badges: {totalBadgePriority}");
Console.WriteLine($"Priority of items in common: {totalPriority}");

static int GetGroupBadgePriority(List<char[]> group, Dictionary<char, int> priorityDictionary) {
    List<HashSet<char>> rucksackSets = new List<HashSet<char>>();
    foreach ( char[] rucksack in group ) {
        rucksackSets.Add( new HashSet<char>(rucksack) );
    }

    HashSet<char> itemSet = new HashSet<char>(priorityDictionary.Keys);
    foreach ( HashSet<char> rucksack in rucksackSets ) {
        itemSet.IntersectWith(rucksack);
        Console.WriteLine($"iterate on: {string.Join("", rucksack)}");
        foreach(var item in itemSet) {
            Console.WriteLine(item);
        }
    }

    if (itemSet.Count != 1 ) {
        throw new Exception($"Unexpected number of badges: {itemSet.Count}");
    }

    Console.WriteLine($"Group badge in common: {itemSet.First()}");

    return priorityDictionary[itemSet.First()];
}

static int GetPriorityOfItemsInBothCompartments(char[] items, Dictionary<char, int> priorityDictionary) {
    int totalPriority = 0;
    char[] firstCompartmentItems = items[0..(items.Count()/2)];
    char[] secondCompartmentItems = items[(items.Count()/2)..];
    Array.Sort( secondCompartmentItems );

    foreach ( char item in GetItemsInCommon(firstCompartmentItems, secondCompartmentItems)) {
        totalPriority += priorityDictionary[item];
    }
    return totalPriority;
}

static IEnumerable<char> GetItemsInCommon(char[] firstCompartmentItems, char[] secondCompartmentItems) {
    HashSet<char> itemTypesFound = new HashSet<char>();
    foreach ( char item in firstCompartmentItems ) {
        int index = Array.BinarySearch( secondCompartmentItems, item );
        if ( index > -1 ) {
            bool firstTimeSeeingItem = itemTypesFound.Add(item);
            if ( firstTimeSeeingItem ) {
                yield return item;
            }
        }
    }
}

static Dictionary<char, int> GetPriorityDictionary() {
    var priorityDictionary = new Dictionary<char, int>();
    List<char> letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();
    for ( int i = 0; i < letters.Count; i++ ) {
        priorityDictionary.Add(letters[i], i+1);
    }

    return priorityDictionary;
}