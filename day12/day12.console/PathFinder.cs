namespace day12.console;

using System.Linq;

public class PathFinder {
    private TerrainMap map;
    private Dictionary<(int, int), int> shortestDistanceFromPoints;
    public PathFinder(TerrainMap map) {
        this.map = map;
        shortestDistanceFromPoints = new Dictionary<(int, int), int>();
    }

    public int GetShortestPath() {
        return GetShortestPathFromPoint(map.StartingPoint, map.DestinationPoint, new List<(int, int)>());
    }

    public int GetShortestPathFromPoint((int, int) start, (int, int) destination, List<(int, int)> previouslySeen) {
        List<(int, int)> nextPossibleSteps = GetValidAdjacentPoints(start, previouslySeen);
        if (nextPossibleSteps.Contains(destination)) {
            return 1;
        } else if (shortestDistanceFromPoints.ContainsKey(start))  {
            int alreadyFoundVal =  1 + shortestDistanceFromPoints[start];
            Console.WriteLine($"Getting value already found from {start} which is {alreadyFoundVal}");
            return alreadyFoundVal;
        } else {
            int minPathLen = -1;
            foreach (var point in nextPossibleSteps) {
                var previousPoints = new List<(int, int)>(previouslySeen);
                previousPoints.Add(start);
                int getShortestPathFromPoint = GetShortestPathFromPoint(point, destination, previousPoints);
                shortestDistanceFromPoints[point] = getShortestPathFromPoint;
                Console.WriteLine($"Shortest distance from {point} is {getShortestPathFromPoint} minPathLen is {minPathLen}");
                if (getShortestPathFromPoint < minPathLen || minPathLen == -1) {
                    minPathLen = getShortestPathFromPoint;
                }
            }
            return 1 + minPathLen;
        }
    }

    public List<(int, int)> GetValidAdjacentPoints((int, int) point, List<(int, int)> previouslySeen) {
        var squares = new List<(int, int)> {
            (point.Item1 - 1, point.Item2),
            (point.Item1, point.Item2 - 1),
            (point.Item1 + 1, point.Item2),
            (point.Item1, point.Item2 + 1)
        };

        return squares
            .Where(s => s.Item1 >= 0 && s.Item2 >= 0 )
            .Where(s => s.Item1 < map.Map.GetLength(0))
            .Where(s => s.Item2 < map.Map.GetLength(1))
            .Where(s => !previouslySeen.Contains(s))
            .Where(s => PointIsValidNextStep(point, (s.Item1, s.Item2))).ToList();
    }

    private bool PointIsValidNextStep((int, int) currentPoint, (int, int) destPoint) {
        //Console.WriteLine($"current: {currentPoint}, dest: {destPoint}");
        return map.Map[destPoint.Item1, destPoint.Item2] == map.Map[currentPoint.Item1, currentPoint.Item2] + 1 ||
            map.Map[destPoint.Item1, destPoint.Item2] == map.Map[currentPoint.Item1, currentPoint.Item2];
    }
}