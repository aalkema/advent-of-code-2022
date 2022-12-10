namespace day10.console;

public class Processor {
    Dictionary<string, int> registers;
    HashSet<int> significantCycles;
    int cycle = 0;
    int signalSum = 0;

    int line = 0;

    public Processor() {
        registers = new Dictionary<string, int>();
        registers.Add("x", 1);

        significantCycles = new HashSet<int> { 40, 80, 120, 160, 200, 240 };
    }

    public void ProcessLine(string line) {
        if (line.Equals("noop")) {
            IncrementCycle();
        } else if (line.StartsWith("addx")) {
            int amount = int.Parse(line.Split(" ")[1]);
            IncrementCycle();
            IncrementCycle();
            registers["x"] += amount;
        }
    }

    public int GetSumOfSignificantCycles() {
        return signalSum;
    }

    private void IncrementCycle() {
        DrawPixel();
        cycle++;
        if (significantCycles.Contains(cycle)) {
            line++;
            Console.Write("\n");
        }
    }

    private void DrawPixel() {
        int horizontalPixelLocation = cycle - (line * 40);
        if (Math.Abs(registers["x"] - horizontalPixelLocation) <= 1) {
            Console.Write("#");
        } else {
            Console.Write(" ");
        }
    }
}