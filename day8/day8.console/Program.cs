namespace day8.console;

public class Program
{
    private static void Main(string[] args)
    {
        using FileStream fs = File.OpenRead("input.txt");
        using var sr = new StreamReader(fs);

        var lines = File.ReadLines(@"./input.txt");
        var rowCount = lines.Count();
        var columnCount = lines.First().Length;
        Console.WriteLine($"Height: {rowCount}, Width: {columnCount}");

        var forestGrid = new ForestGrid(rowCount, columnCount);

        string line;
        int lineNum = 0;
        while ((line = sr.ReadLine()) != null) {
            forestGrid.AddRow(line, lineNum);
            lineNum++;
        }

        Console.WriteLine(forestGrid.GetNumberOfVisibleTrees());
    }
}