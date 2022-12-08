namespace day7.console;

using System.Text;

public static class DirectoryStructureManager {

    public static List<int> DirSizesWeCouldDelete(Directory directory, int desiredSize) {
        if (directory.Directories.Count == 0) {
            if (directory.size >= desiredSize) {
                return new List<int>() { directory.size };
            } else {
                return new List<int>();
            }
        } else {
            if (directory.size >= desiredSize) {
                return directory.Directories.Values.SelectMany(d => DirSizesWeCouldDelete(d, desiredSize))
                    .Concat(new List<int>() {directory.size}).ToList();
            } else {
                return directory.Directories.Values.SelectMany(d => DirSizesWeCouldDelete(d, desiredSize))
                    .Concat(new List<int>()).ToList();
            }
        }
    }

    public static int GetDirectorySizeTotalUnderThreshold(Directory directory, int threshold) {
        if (directory.Directories.Count == 0) {
             if (directory.size < threshold) {
                return directory.size;
             } else {
                return 0;
             }
        } else {
            if (directory.size < threshold) {
                return directory.size + directory.Directories.Values.Sum(d => GetDirectorySizeTotalUnderThreshold(d, threshold));
            } else {
                return 0 + directory.Directories.Values.Sum(d => GetDirectorySizeTotalUnderThreshold(d, threshold));
            }
        }
    }

    public static void PrintDirectoryStructure(Directory directory, int depth) {
        if (directory.Directories.Count == 0) {
             PrintFiles(directory, depth);
        } else {
            Console.WriteLine($"{GetSpaces(depth)}{directory.Name}:{directory.size}");
            PrintFiles(directory, depth);
            foreach ( var dir in directory.Directories.Values ) {
                PrintDirectoryStructure(dir, depth+1);
            }
        }
    }

    private static void PrintFiles(Directory directory, int depth) {
        foreach ( var file in directory.Files.Keys ) {
            Console.WriteLine(GetSpaces(depth) + file);
        }
    }

    private static string GetSpaces(int depth) {
        StringBuilder spaces = new StringBuilder();
        for (int i = 1; i <= depth; i++) {
            spaces.Append(" ");
        }
        return spaces.ToString();
    }
}