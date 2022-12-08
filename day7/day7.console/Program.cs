namespace day7.console;

public class Program
{
    private static void Main(string[] args)
    {
        using FileStream fs = File.OpenRead("input.txt");
        using var sr = new StreamReader(fs);

        var fsNavigator = new FileSystemNavigator();
        string line;
        while ((line = sr.ReadLine()) != null) {
            fsNavigator.ParseLine(line);
        }

        int fileSizeCap = 100000;
        Directory root = fsNavigator.GetDirectoryStructure();
        Console.WriteLine($"Total under 100000: {DirectoryStructureManager.GetDirectorySizeTotalUnderThreshold(root, 100000)}");

        int dirSizeMinToDelete = 30000000 - (70000000 - root.size);
        Console.WriteLine(dirSizeMinToDelete);
        List<int> options = DirectoryStructureManager.DirSizesWeCouldDelete(root, dirSizeMinToDelete);
        foreach (var option in options) {
            Console.Write($"{option},");
        }
        Console.WriteLine("");
        Console.WriteLine(options.Min());
    }
}