namespace day14.console;

public class SandSimulator {
    private ScanResult scanResult;
    private (int, int) sandOrigin;
    public SandSimulator(ScanResult scan, (int, int) origin) {
        scanResult = scan;
        sandOrigin = origin;
    }

    public int GetSandCapacity() {
        bool sandFallingIntoTheAbyss = false;
        int numDroppedSand = 0;
        while (!sandFallingIntoTheAbyss) {
            bool result = DropSand();
            if (result) {
                sandFallingIntoTheAbyss = true;
            } else {
                numDroppedSand++;
            }
        }
        return numDroppedSand;
    }

    // Returns true when sand block falls into the abyss
    private bool DropSand() {
        (int, int) sandPosition = sandOrigin;
        while (sandPosition.Item2 <= scanResult.FarthestDown) {
            (int, int) downOne = (sandPosition.Item1, sandPosition.Item2 + 1);
            if (scanResult.Scan.ContainsKey(downOne)) {
                (int, int) downLeft = (sandPosition.Item1 - 1, sandPosition.Item2 + 1);
                if (scanResult.Scan.ContainsKey(downLeft)) {
                    (int, int) downRight = (sandPosition.Item1 + 1, sandPosition.Item2 + 1);
                    if (scanResult.Scan.ContainsKey(downRight)) {
                        scanResult.Scan[sandPosition] = Obstacle.Sand;
                        Console.WriteLine($"Sand settled at {sandPosition}");
                        return false;
                    } else {
                        sandPosition = downRight;
                    }
                } else {
                    sandPosition = downLeft;
                }
            } else {
                sandPosition = downOne;
            }
        }
        return true;
    }
}