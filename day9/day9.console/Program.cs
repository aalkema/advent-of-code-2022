namespace day9.console;

public class Program
{
    private static void Main(string[] args)
    {
        using FileStream fs = File.OpenRead("input.txt");
        using var sr = new StreamReader(fs);

        var ropeManager = new RopeManager(10);
        string line;
        while ((line = sr.ReadLine()) != null) {
            string[] command = line.Split(" ");
            string direction = command[0];
            int amount = int.Parse(command[1]);
            ropeManager.Move(direction, amount);
        }

        Console.WriteLine(ropeManager.GetNumberOfUniqueTailPositions());
    }
}