namespace day13.console;

public class SinglePacket {
    private Stack<char> packet;
    private readonly Stack<char> _packet;

    public SinglePacket(string packet) {
        this.packet = new Stack<char>();
        this._packet = new Stack<char>();
        PopulateStack(packet, this.packet);
        PopulateStack(packet, this._packet);
    }

    public void Reset() {
        this.packet = new Stack<char>(_packet.Reverse());
    }

    private void PopulateStack(string input, Stack<char> stack) {
        char[] stringChars = input.ToArray();
        for (int i = stringChars.Count() - 1; i >= 0; i--) {
            stack.Push(stringChars[i]);
        }
    }

    public string OriginalInput {
        get { return StackToString(_packet); }
    }

    public Stack<char> Packet {
        get { return packet; }
    }

    public string PacketString() {
        return StackToString(packet);
    }

    private string StackToString(Stack<char> stack) {
        char[] stackElements = new char[stack.Count];
        int index = 0;
        foreach (char item in stack) {
            stackElements[index] = item;
            index++;
        }
        return new string(stackElements);
    }
}