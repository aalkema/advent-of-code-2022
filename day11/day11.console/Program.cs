namespace day11.console;

public class Program
{
    private static void Main(string[] args)
    {
        using FileStream fs = File.OpenRead("input.txt");
        using var sr = new StreamReader(fs);

        var monkeyParser = new MonkeyParser();
        string line;
        while ((line = sr.ReadLine()) != null) {
            monkeyParser.ParseLine(line);
        }

        var monkeyManager = new MonkeyManager(monkeyParser.GetMonkeys());

        monkeyManager.PerformRounds(10000);

        Console.WriteLine($"Monkey business: {monkeyManager.GetMonkeyBusiness()}");
    }
}