namespace day14.console;

public class ScanResult {
    private Dictionary<(int, int), Obstacle> scan;
    private int farthestDown = 0;

    public ScanResult() {
        scan = new Dictionary<(int, int), Obstacle>();
    }

    public Dictionary<(int, int), Obstacle> Scan {
        get { return scan; }
    }

    public int FarthestDown {
        get { return farthestDown; }
    }

    public void AddRockLine((int, int) start, (int, int) finish) {
        var xs = new List<int> { start.Item1, finish.Item1 };
        var ys = new List<int> { start.Item2, finish.Item2 };
        xs.Sort();
        ys.Sort();
        foreach (int x in Enumerable.Range(xs[0], (xs[1]- xs[0]) + 1)) {
            //Console.WriteLine($"Adding rock at ({(x, start.Item2)})");
            scan[(x, start.Item2)] = Obstacle.Rock;
        }
        foreach (int y in Enumerable.Range(ys[0], (ys[1]- ys[0]) + 1)) {
            if (y > farthestDown) {
                farthestDown = y;
                //Console.WriteLine($"new deepest: {y}");
            }
            //Console.WriteLine($"Adding rock at ({(start.Item1, y)})");
            scan[(start.Item1, y)] = Obstacle.Rock;
        }
    }
}