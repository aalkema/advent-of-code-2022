namespace day12.test;

using day12.console;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        TerrainMap map = new TerrainMap();
        map.Map = new char[5,8] {
            {'S', 'a', 'b', 'q', 'p', 'o', 'n', 'm'},
            {'a', 'b', 'c', 'r', 'y', 'x', 'x', 'l'},
            {'a', 'c', 'c', 's', 'z', 'E', 'x', 'k'},
            {'a', 'c', 'c', 't', 'u', 'v', 'w', 'j'},
            {'a', 'b', 'd', 'e', 'f', 'g', 'h', 'i'}
        };
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}