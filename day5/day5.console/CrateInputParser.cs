namespace day5.console;

using System.Text.RegularExpressions;
using System.Collections.Generic;

public class CrateInputParser {
    private CrateManager _crateManager;
    public CrateInputParser(CrateManager crateManager) {
        _crateManager = crateManager;
    }

    public void parseLine(string line) {
        if (string.IsNullOrEmpty(line)) {
            return;
        } else if (line.StartsWith(" 1")) {
            FlipStacks();
        } else if (line.StartsWith("move")) {
            SendInstruction(line);
        } else {
            PopulateStack(line);
        }
    }

    private void SendInstruction(string line) {
        MatchCollection matches = Regex.Matches(line, "move ([0-9]*) from ([0-9]*) to ([0-9]*)");
        int count = int.Parse(matches[0].Groups[1].Value);
        int source = int.Parse(matches[0].Groups[2].Value);
        int destination = int.Parse(matches[0].Groups[3].Value);
        Console.WriteLine($"Sending {count} crates from {source} to {destination}");
        _crateManager.MoveCrates(source, destination, count);
    }

    private void PopulateStack(string line) {
        var chars = new List<char>(line);
        for (int i = 0; i < chars.Count; i++) {
            if (char.IsAsciiLetter(chars[i])) {
                _crateManager.PushToStack(((i-1)/4)+1, chars[i]);
            }
        }
    }

    private void FlipStacks() {
        _crateManager.FlipAllStacks();
    }
}