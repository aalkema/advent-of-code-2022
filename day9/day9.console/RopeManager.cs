namespace day9.console;

using System.IO;

public class RopeManager {
    (int, int) headPosition;
    (int, int) tailPosition;

    HashSet<(int, int)> tailPositions;
    public RopeManager() {
        headPosition = (0, 0);
        tailPosition = (0, 0);
        tailPositions = new HashSet<(int, int)>();
        tailPositions.Add(tailPosition);
    }

    public int GetNumberOfUniqueTailPositions() {
        return tailPositions.Count;
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
        AdjustTailPosition();
    }

    private void AdjustTailPosition() {
        int distanceBetweenXPositions = headPosition.Item1 - tailPosition.Item1;
        int distanceBetweenYPositions = headPosition.Item2 - tailPosition.Item2;
        if (Math.Abs(distanceBetweenXPositions) > 1 || Math.Abs(distanceBetweenYPositions)  > 1) {
            File.AppendAllText("./output.txt", $"Not adjacent - Head:{headPosition.Item1},{headPosition.Item2} Tail:{tailPosition.Item1},{tailPosition.Item2}" + Environment.NewLine);

            int newTailXPosition = tailPosition.Item1;
            if (distanceBetweenXPositions != 0) {
                newTailXPosition = tailPosition.Item1 + (distanceBetweenXPositions / Math.Abs(distanceBetweenXPositions));
            }
            int newTailYPosition = tailPosition.Item2;
            if (distanceBetweenYPositions != 0) {
                newTailYPosition = tailPosition.Item2 + (distanceBetweenYPositions / Math.Abs(distanceBetweenYPositions));
            }
            
            tailPosition = (newTailXPosition, newTailYPosition);
            Console.WriteLine($"x: {tailPosition.Item1}, y: {tailPosition.Item2}");
            File.AppendAllText("./output.txt", $"x: {tailPosition.Item1}, y: {tailPosition.Item2}" + Environment.NewLine);

            tailPositions.Add(tailPosition);
        } else {
            File.AppendAllText("./output.txt", "No tail change" + Environment.NewLine);
        }
    }
}