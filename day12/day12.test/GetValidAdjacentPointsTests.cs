namespace day12.test;

using day12.console;

public class Tests
{
    TerrainMap map;
    PathFinder pathFinder;
    [SetUp]
    public void Setup()
    {
        map = new TerrainMap();
        map.Map = new char[5,8] {
            {'a', 'a', 'b', 'q', 'p', 'o', 'n', 'm'},
            {'a', 'b', 'c', 'r', 'y', 'x', 'x', 'l'},
            {'a', 'c', 'c', 's', 'z', 'z', 'x', 'k'},
            {'a', 'c', 'c', 't', 'u', 'v', 'w', 'j'},
            {'a', 'b', 'd', 'e', 'f', 'g', 'h', 'i'}
        };
        map.StartingPoint = (0, 0);
        map.DestinationPoint = (2, 5);

        pathFinder = new PathFinder(map);
    }

    [Test]
    public void GetValidAdjacentPoints_Start_GetBoth()
    {
        var points = pathFinder.GetValidAdjacentPoints((0, 0), new List<(int, int)>());
        Assert.IsTrue(points.Count == 2);
        Assert.IsTrue(points.Contains((1, 0)));
        Assert.IsTrue(points.Contains((0, 1)));
    }

    [Test]
    public void GetValidAdjacentPoints_SameButWithPrevIncluded_GetOne()
    {
        var points = pathFinder.GetValidAdjacentPoints((0, 0), new List<(int, int)>() {(1, 0)});
        Assert.IsTrue(points.Count == 1);
        Assert.IsTrue(points.Contains((0, 1)));
    }

    [Test]
    public void GetValidAdjacentPoints_ThreeOptions_GetAll() {
        var points = pathFinder.GetValidAdjacentPoints((3, 2), new List<(int, int)>());
        Assert.IsTrue(points.Count == 3);
        Assert.IsTrue(points.Contains((2, 2)));
        Assert.IsTrue(points.Contains((3, 1)));
        Assert.IsTrue(points.Contains((4, 2)));
    }

    [Test]
    public void GetValidAdjacentPoints_ThreeOptions_GetOne() {
        var points = pathFinder.GetValidAdjacentPoints((3, 2), new List<(int, int)>() {(2, 2), (3, 1)});
        Assert.IsTrue(points.Count == 1);
        Assert.IsTrue(points.Contains((4, 2)));
    }

    [Test]
    public void GetValidAdjacentPoints_ThreeOptionsOneWasPrev_GetTwo() {
        var points = pathFinder.GetValidAdjacentPoints((3, 2), new List<(int, int)>(){(2, 2)});
        Assert.IsTrue(points.Count == 2);
        Assert.IsTrue(points.Contains((3, 1)));
        Assert.IsTrue(points.Contains((4, 2)));
    }

    [Test]
    public void GetValidAdjacentPoints_Dest_GetThree() {
        var points = pathFinder.GetValidAdjacentPoints((2, 4), new List<(int, int)>());
        //Assert.AreEqual(4, points.Count);
        Assert.IsTrue(points.Contains((2, 5)));
    }

    [Test]
    public void GetShortestPath_BesideDestination_One() {
        map.StartingPoint = (2, 4);
        Assert.IsTrue(pathFinder.GetShortestPath() == 1);
    }

    [Test]
    public void GetShortestPath_TwoSteps_Two() {
        map.StartingPoint = (1, 4);
        Assert.AreEqual(2, pathFinder.GetShortestPath());
    }

    [Test]
    public void GetShortestPath_FullPath_ThirtyOne() {
        Assert.AreEqual(31, pathFinder.GetShortestPath());
    }
}