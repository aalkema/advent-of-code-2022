namespace day13.console;

public class Program
{
    private static void Main(string[] args)
    {
        using FileStream fs = File.OpenRead("input.txt");
        using var sr = new StreamReader(fs);

        var packetParser = new PacketParser();
        string line;
        while ((line = sr.ReadLine()) != null) {
            packetParser.ParseSinglePacketLine(line);
        }

        var packetAnalyzer = new PacketAnalyzer();

        // 6005 is too low
        // 6245 is too high
        Console.WriteLine($"Decoder: {packetAnalyzer.GetDecoder(packetParser.SinglePackets)}");
    }
}