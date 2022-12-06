namespace day5.console;

public class Program
{
    private static void Main(string[] args)
    {
        using FileStream fs = File.OpenRead("input.txt");
        using var sr = new StreamReader(fs);

        var stacks = new Dictionary<int, Stack<char>>();
        var crateManager = new CrateManager(stacks);
        var crateInputParser = new CrateInputParser(crateManager);
        string line;
        while ((line = sr.ReadLine()) != null) {
            crateInputParser.parseLine(line);
        }

        //SDGVVWLFL is not right
        Console.WriteLine(crateManager.GetTopsOfStacks());
    }


}