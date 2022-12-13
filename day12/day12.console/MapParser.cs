namespace day12.console;

public class MapParser {

    char[,] terrainmap;
    int currentRow = 0;
    TerrainMap map;
    public MapParser(int rows, int columns) {
        terrainmap = new char[rows, columns];
        map = new TerrainMap();
    }

    public void InputRow(string line) {
        char[] chars = line.ToArray();
        for (int i = 0; i < line.Length; i++) {
            if (line[i] == 'S') {
                map.StartingPoint = (currentRow, i);
                terrainmap[currentRow, i] = 'a';
            } else if (line[i] == 'E') {
                map.DestinationPoint = (currentRow, i);
                terrainmap[currentRow, i] = 'z';
            } else {
                terrainmap[currentRow, i] = chars[i];
            }
        }
        currentRow++;
    }

    public TerrainMap GetMap() {
        map.Map = terrainmap;
        return map;
    }
}