namespace day12.console;

using System.Linq;

public class PathFinder {
    private TerrainMap map;
    public PathFinder(TerrainMap map) {
        this.map = map;
    }

    public int GetShortestPath() {
        return GetShortestPathFromPoint(map.StartingPoint, map.DestinationPoint);
    }

    public int GetShortestPathFromPoint((int, int) start, (int, int) destination) {
        List<(int, int)> nextPossibleSteps = GetValidAdjacentPoints(start);
        if (nextPossibleSteps.Contains(destination)) {
            return 1;
        } else {
            int minPathLen = int.MaxValue;
            foreach (var point in nextPossibleSteps) {
                int getShortestPathFromPoint = GetShortestPathFromPoint(point, destination);
                if (getShortestPathFromPoint < minPathLen) {
                    minPathLen = getShortestPathFromPoint;
                }
            }
            return 1 + minPathLen;
        }
    }

    public List<(int, int)> GetValidAdjacentPoints((int, int) point) {
        var squares = new List<(int, int)> {
            (point.Item1 - 1, point.Item2),
            (point.Item1, point.Item2 - 1),
            (point.Item1 + 1, point.Item2),
            (point.Item1, point.Item2 + 1)
        };

        return squares
            .Where(s => s.Item1 >= 0 && s.Item2 >= 0 )
            .Where(s => PointIsValidNextStep(point, (s.Item1, s.Item2))).ToList();
    }

    private bool PointIsValidNextStep((int, int) currentPoint, (int, int) destPoint) {
        return map.Map[destPoint.Item1, destPoint.Item2] <= map.Map[currentPoint.Item1, currentPoint.Item2] + 1;
    }
}