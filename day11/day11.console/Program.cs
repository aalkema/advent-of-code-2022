namespace day11.console;

public class Program
{
    private static void Main(string[] args)
    {
        using FileStream fs = File.OpenRead("input.txt");
        using var sr = new StreamReader(fs);

        string line;
        while ((line = sr.ReadLine()) != null) {

        }
    }
}