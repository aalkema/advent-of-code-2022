namespace day7.console;

using System.Text.RegularExpressions;

public class FileSystemNavigator {
    private Directory currentDirectory;

    public FileSystemNavigator() {
        currentDirectory = new Directory("", null);
        currentDirectory.AddDirectory(new Directory("/", currentDirectory));
    }

    public void ParseLine(string line) {
        if (line.StartsWith("$")) {
            HandleCommand(line);
        } else {
            AddLsItemToCurrentDirectory(line);
        }
    }

    public Directory GetDirectoryStructure() {
        Directory topLevel = currentDirectory;
        while (topLevel.GetParentDirectory() != null ) {
            topLevel = topLevel.GetParentDirectory();
        }
        return topLevel;
    }

    private void HandleCommand(string line) {
        (string, string) command = ParseCommand(line);
        if (command.Item1 == "cd") {
            if ( command.Item2 == ".." ) {
                this.currentDirectory = currentDirectory.GetParentDirectory();
            } else {
                currentDirectory = currentDirectory.GetSubDirectory(command.Item2);
            }
        }
    }

    private void AddLsItemToCurrentDirectory(string line) {
        if (line.StartsWith("dir")) {
            currentDirectory.AddDirectory(new Directory(line.Split(" ")[1], currentDirectory));
        } else {
            var splitLine = line.Split(" ");
            var newFile = new AdventFile(splitLine[1], int.Parse(splitLine[0]));
            currentDirectory.AddFile(newFile);
        }
    }

    private (string, string) ParseCommand(string line) {
        string pattern = "\\$\\s(cd|ls)\\s*(.*)";
        var rg = new Regex(pattern);
        MatchCollection matches = rg.Matches(line);
        string command = matches[0].Groups[1].Value;
        string commandInput = "";
        if (matches[0].Groups.Count == 3) {
            commandInput = matches[0].Groups[2].Value;
        }
        return (command, commandInput);
    }
}