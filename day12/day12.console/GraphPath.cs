namespace day12.console;

using System.Collections.Generic;

public class GraphPath {
    Queue<(int, int)> frontier;
    Dictionary<(int, int), (int, int)> cameFrom;
    private TerrainMap map;
    public GraphPath(TerrainMap map) {
        this.map = map;
        frontier = new Queue<(int, int)>();
        cameFrom = new Dictionary<(int, int), (int, int)>();
        frontier.Enqueue(map.StartingPoint);
        cameFrom.Add(map.StartingPoint, (-1, -1));
    }

    public int GetShortestPath() {
        int steps = 0;
        while (frontier.Count > 0) {
            var point = frontier.Dequeue();
            if (point == map.DestinationPoint) {
                return GetDistanceFromStart(point);
            }
            var neighbors = GetValidAdjacentPoints(point);
            foreach (var neighbor in neighbors) {
                if (!cameFrom.ContainsKey(neighbor)) {
                    //Console.WriteLine($"Enqueing {neighbor}");
                    File.AppendAllText("./output.txt", $"Enqueing {neighbor} from {point}" + Environment.NewLine);
                    cameFrom.Add(neighbor, point);
                    frontier.Enqueue(neighbor);
                }
            }
        }
        return -1;
    }

    public int GetDistanceFromStart((int, int) point) {
        (int, int) previous = map.DestinationPoint;
        int steps = 0;
        Console.WriteLine($"Getting distance from {point} to {map.StartingPoint}");
        while (previous != map.StartingPoint) {
            previous = cameFrom[previous];
            steps++;
        }
        return steps;
    }

    private List<(int, int)> GetValidAdjacentPoints((int, int) point) {
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
        .Where(s => PointIsValidNextStep(point, (s.Item1, s.Item2))).ToList();
    }

    private bool PointIsValidNextStep((int, int) currentPoint, (int, int) destPoint) {
        //Console.WriteLine($"current: {currentPoint}, dest: {destPoint}");
        return map.Map[destPoint.Item1, destPoint.Item2] <= map.Map[currentPoint.Item1, currentPoint.Item2] + 1;
    }
}