public class Program
{
    private static void Main(string[] args)
    {
        using FileStream fs = File.OpenRead("input.txt");
        using var sr = new StreamReader(fs);
        while ((line = sr.ReadLine()) != null) {
            HandlePairs(line);
        }

    }

    public static void DoesPairFullyContainOther(string pair) {

    }
}