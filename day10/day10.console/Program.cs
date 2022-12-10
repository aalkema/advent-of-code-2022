namespace day10.console;

public class Program
{
    private static void Main(string[] args)
    {
        using FileStream fs = File.OpenRead("input.txt");
        using var sr = new StreamReader(fs);

        var processor = new Processor();
        string line;
        while ((line = sr.ReadLine()) != null) {
            processor.ProcessLine(line);
        }
    }
}