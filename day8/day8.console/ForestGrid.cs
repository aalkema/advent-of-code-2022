namespace day8.console;

using System.Linq;

public class ForestGrid {
    int[,] _forest;

    public ForestGrid(int rows, int cols) {
        _forest = new int[rows, cols];
    }

    public void AddRow(string line, int row) {
        int[] treeHeights = line.Select(l => int.Parse(l.ToString())).ToArray();
        for (int i = 0; i < treeHeights.Length; i++) {
            _forest[row, i] = treeHeights[i];
        }
    }

    public int GetNumberOfVisibleTrees() {
        int numVisibleTrees = 0;
        for (int i = 0; i < _forest.GetLength(0); i++) {
            for (int j = 0; j < _forest.GetLength(1); j++) {
                if (IsThereATallerTreeAboveAndBelow(i, j)) {
                    if (IsThereATallerTreeLeftAndRight(i, j)) {
                        continue;
                    }
                }
                numVisibleTrees++;
            }
        }

        return numVisibleTrees;
    }

    public int GetHighestScenicScore() {
        int bestScenicScore = 0;
        for (int i = 0; i < _forest.GetLength(0); i++) {
            for (int j = 0; j < _forest.GetLength(1); j++) {
                int score = GetTreeScenicScore(i, j);
                if (score > bestScenicScore) {
                    bestScenicScore = score;
                }
            }
        }

        return bestScenicScore;
    }

    public int GetTreeScenicScore(int row, int column) {
        return GetViewDown(row, column) *
            GetViewUp(row, column) *
            GetViewLeft(row, column) *
            GetViewRight(row, column);
    }

    private int GetViewUp(int row, int column) {
        int height = _forest[row, column];
        int numTrees = 0;
        for (int i = row-1; i>=0; i--) {
            numTrees++;
            if (_forest[i, column] >= height ) {
                break;
            }
        }
        return numTrees;
    }

    private int GetViewDown(int row, int column) {
        int height = _forest[row, column];
        int numTrees = 0;
        for (int i = row+1; i<_forest.GetLength(1); i++) {
            numTrees++;
            if (_forest[i, column] >= height ) {
                break;
            }
        }
        return numTrees;
    }

    private int GetViewLeft(int row, int column) {
        int height = _forest[row, column];
        int numTrees = 0;
        for (int i = column-1; i>=0; i--) {
            numTrees++;
            if (_forest[row, i] >= height ) {
                break;
            }
        }
        return numTrees;
    }

    private int GetViewRight(int row, int column) {
        int height = _forest[row, column];
        int numTrees = 0;
        for (int i = column+1; i<_forest.GetLength(0); i++) {
            numTrees++;
            if (_forest[row, i] >= height ) {
                break;
            }
        }
        return numTrees;
    }

    private bool IsThereATallerTreeAboveAndBelow(int row, int column) {
        int height = _forest[row, column];
        bool atCurrentTree = false;
        bool tallerTreeAbove = false;
        bool tallerTreeBelow = false;
        for (int i = 0; i < _forest.GetLength(1); i++) {
            if (i == row) { 
                atCurrentTree = true;
                continue; 
            }
            if (_forest[i, column] >= height) {
                if (atCurrentTree) {
                    tallerTreeBelow = true;
                } else {
                    tallerTreeAbove = true;
                }
            }
        }
        return tallerTreeAbove && tallerTreeBelow;
    }

    private bool IsThereATallerTreeLeftAndRight(int row, int column) {
        int height = _forest[row, column];
        bool atCurrentTree = false;
        bool tallerTreeLeft = false;
        bool tallerTreeRight = false;
        for (int i = 0; i < _forest.GetLength(0); i++) {
            if (i == column) { 
                atCurrentTree = true;
                continue; 
            }
            if (_forest[row, i] >= height) {
                if (atCurrentTree) {
                    tallerTreeRight = true;
                } else {
                    tallerTreeLeft = true;
                }
            }
        }
        return tallerTreeLeft && tallerTreeRight;
    }
}