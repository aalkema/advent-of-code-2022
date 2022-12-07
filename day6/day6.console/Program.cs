namespace day6.console;

public class Program
{
    private static void Main(string[] args)
    {
        int position = 0;
        var packetAnalyzer = new PacketAnalyzer(14);
        using (var sr = new StreamReader("input.txt")) {
            while (sr.Peek() >= 0) {
                position++;
                if ( packetAnalyzer.StartOfPacketComplete((char)sr.Read()) ) {
                    Console.WriteLine(position);
                    Console.WriteLine(packetAnalyzer.GetCurrentQueueValue());
                    break;
                }
            }
        }
    }
}