namespace day9.console;

using System.IO;

public class RopeManager {
    (int, int) headPosition;
    List<(int, int)> tailPositions;
    HashSet<(int, int)> tailPositionTracker;
    public RopeManager(int numTails) {
        headPosition = (0, 0);
        tailPositions = new List<(int, int)>();
        for (int i = 0; i < numTails; i++) {
            tailPositions.Add((0,0));
        }
        tailPositionTracker = new HashSet<(int, int)>();
        tailPositionTracker.Add(tailPositions.Last());
    }

    public int GetNumberOfUniqueTailPositions() {
        return tailPositionTracker.Count;
    }

    public void Move(string direction, int amount) {
        for (int i =0; i < amount; i++) {
            MoveOnce(direction);
        }
    }

    private void MoveOnce(string direction) {
        switch(direction) {
            case "U":
                headPosition = (headPosition.Item1, headPosition.Item2+1);
                break;
            case "D":
                headPosition = (headPosition.Item1, headPosition.Item2-1);
                break;
            case "R":
                headPosition = (headPosition.Item1+1, headPosition.Item2);
                break;
            case "L":
                headPosition = (headPosition.Item1-1, headPosition.Item2);
                break;
        }
        AdjustTailPositions();
    }

    private void AdjustTailPositions() {
        (int, int) currentHead = headPosition;
        for (int i = 0; i < tailPositions.Count; i++ ) {
            if ( i!= 0 ) {
                currentHead = tailPositions[i-1];
            }
            int distanceBetweenXPositions = currentHead.Item1 - tailPositions[i].Item1;
            int distanceBetweenYPositions = currentHead.Item2 - tailPositions[i].Item2;
            if (Math.Abs(distanceBetweenXPositions) > 1 || Math.Abs(distanceBetweenYPositions)  > 1) {
                File.AppendAllText("./output.txt", $"Not adjacent - Head:{currentHead.Item1},{currentHead.Item2} Tail:{tailPositions[i].Item1},{tailPositions[i].Item2}" + Environment.NewLine);

                int newTailXPosition = tailPositions[i].Item1;
                if (distanceBetweenXPositions != 0) {
                    newTailXPosition = tailPositions[i].Item1 + (distanceBetweenXPositions / Math.Abs(distanceBetweenXPositions));
                }
                int newTailYPosition = tailPositions[i].Item2;
                if (distanceBetweenYPositions != 0) {
                    newTailYPosition = tailPositions[i].Item2 + (distanceBetweenYPositions / Math.Abs(distanceBetweenYPositions));
                }
                
                tailPositions[i] = (newTailXPosition, newTailYPosition);
                Console.WriteLine($"x: {tailPositions[i].Item1}, y: {tailPositions[i].Item2}");
                File.AppendAllText("./output.txt", $"x: {tailPositions[i].Item1}, y: {tailPositions[i].Item2}" + Environment.NewLine);

                if ( i == tailPositions.Count - 1) {
                    tailPositionTracker.Add(tailPositions[i]);
                }
            } else {
                File.AppendAllText("./output.txt", "No tail change" + Environment.NewLine);
            }
        }
        
    }
}