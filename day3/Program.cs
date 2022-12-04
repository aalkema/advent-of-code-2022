using System.Collections;

using FileStream fs = File.OpenRead("input.txt");
using var sr = new StreamReader(fs);

Dictionary<char, int> priorityDictionary = GetPriorityDictionary();
int totalPriority = 0;
string line;
int sack = 1;
while ((line = sr.ReadLine()) != null) {
    char[] letters = line.ToArray();
    char[] firstCompartmentItems = letters[0..(letters.Count()/2)];
    char[] secondCompartmentItems = letters[(letters.Count()/2)..];
    Array.Sort( secondCompartmentItems );

    Console.WriteLine($"rucksack {sack}:");
    foreach ( char item in GetItemsInCommon(firstCompartmentItems, secondCompartmentItems)) {
        Console.WriteLine($"{item} with priority {priorityDictionary[item]}");
        totalPriority += priorityDictionary[item];
    }
    sack++;
}

Console.WriteLine(totalPriority);

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