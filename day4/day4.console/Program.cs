namespace day4.console;

public class Program
{
    private static void Main(string[] args)
    {
        using FileStream fs = File.OpenRead("input.txt");
        using var sr = new StreamReader(fs);
        int fullyContainedPairs = 0;
        int overlappingPairs = 0;
        string line;
        while ((line = sr.ReadLine()) != null) {
            if (DoesPairFullyContainOther(line)) {
                fullyContainedPairs++;
            }
            if (DoPairsOverlap(line)) {
                overlappingPairs++;
            }
        }
        Console.WriteLine(fullyContainedPairs);
        Console.WriteLine($"Overlapping pairs: {overlappingPairs}");
    }

    public static bool DoesPairFullyContainOther(string pairs) {
        List<Sections> sections = GetSectionsFromString(pairs);
        sections.Sort((s1, s2) => s1.Length.CompareTo(s2.Length));
        return sections[1].Contains(sections[0]);
    }

    public static bool DoPairsOverlap(string pairs) {
        List<Sections> sections = GetSectionsFromString(pairs);
        if ( sections[1].Overlap(sections[0]) ) {
            Console.WriteLine($"Overlap: {string.Join(",", sections)}");
        } else {
            Console.WriteLine($"No overlap: {string.Join(",", sections)}");
        }
        return sections[1].Overlap(sections[0]);
    }

    public static List<Sections> GetSectionsFromString(string pairs) {
        List<Sections> results = new List<Sections>();
        string[] sections = pairs.Split(",");
        foreach ( string section in sections ) {
            results.Add(new Sections(section));
        }
        return results;
    }
}