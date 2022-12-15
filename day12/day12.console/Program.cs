namespace day12.console;

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

        var mapParser = new MapParser(rowCount, columnCount);

        string line;
        while ((line = sr.ReadLine()) != null) {
            mapParser.InputRow(line);
        }

        TerrainMap map = mapParser.GetMap();
        Console.WriteLine($"Start: {map.StartingPoint}, end: {map.DestinationPoint}");

        var pathFinder = new GraphPath(map);
        Console.WriteLine(pathFinder.GetShortestPath());
    }
}