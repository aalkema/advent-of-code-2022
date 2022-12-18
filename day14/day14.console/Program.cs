namespace day14.console;

public class Program
{
    private static void Main(string[] args)
    {
        using FileStream fs = File.OpenRead("input.txt");
        using var sr = new StreamReader(fs);

        var rockPathParser = new RockPathParser();
        string line;
        while ((line = sr.ReadLine()) != null) {
            rockPathParser.ParsePath(line);
        }

        var scan = rockPathParser.GetScanResult();

        Console.WriteLine($"Deepest rock is {scan.FarthestDown}");
        var sandSimulator = new SandSimulator(scan, (500, 0));
        Console.WriteLine($"Sand capacity is: {sandSimulator.GetSandCapacity()}");
    }
}