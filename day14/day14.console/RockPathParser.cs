namespace day14.console;

public class RockPathParser {
    ScanResult scanResult;
    public RockPathParser() {
        scanResult = new ScanResult();
    }

    public void ParsePath(string path) {
        string[] points = path.Split(" -> ");
        for (int i = 0; i < points.Count() - 1; i++) {
            scanResult.AddRockLine(
                GetPointFromString(points[i]),
                GetPointFromString(points[i+1])
            );
        }
    }

    public ScanResult GetScanResult() {
        return scanResult;
    }

    private (int, int) GetPointFromString(string point) {
        string[] coords = point.Split(",");
        int x = int.Parse(coords[0]);
        int y = int.Parse(coords[1]);
        return (x, y);
    }
}