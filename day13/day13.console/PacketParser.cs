namespace day13.console;

public class PacketParser {
    private List<Packet> packets;
    private List<SinglePacket> singlePackets;
    private string currentFirstPacket = "";
    int index = 1;
    
    public PacketParser() {
        packets = new List<Packet>();
        singlePackets = new List<SinglePacket>() {
            new SinglePacket("[[2]]"),
            new SinglePacket("[[6]]")
        };
    }

    public void ParseLine(string line) {
        if (string.IsNullOrEmpty(line)) {
            return;
        }

        if (string.IsNullOrEmpty(currentFirstPacket)) {
            currentFirstPacket = line;
        } else {
            packets.Add(new Packet(currentFirstPacket, line, index));
            currentFirstPacket = "";
            index++;
        }
    }

    public void ParseSinglePacketLine(string line) {
        if (string.IsNullOrEmpty(line)) {
            return;
        }

        singlePackets.Add(new SinglePacket(line));
    }

    public List<Packet> Packets {
        get { return packets; }
    }

    public List<SinglePacket> SinglePackets {
        get { return singlePackets; }
    }
}