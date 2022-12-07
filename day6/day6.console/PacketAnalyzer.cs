namespace day6.console;

using System.Collections.Generic;
using System.Text;

public class PacketAnalyzer {
    Queue<char> _lastFour;
    private int _uniqueCharsRequired;

    public PacketAnalyzer(int uniqueCharsRequired) {
        _lastFour = new Queue<char>();
        _uniqueCharsRequired = uniqueCharsRequired;
    }

    public bool StartOfPacketComplete(char nextChar) {
        if (_lastFour.Count >= _uniqueCharsRequired) {
            _lastFour.Dequeue();
            _lastFour.Enqueue(nextChar);
            return IsStartOfPacket();
        } else {
            _lastFour.Enqueue(nextChar);
            return false;
        }
    }

    public string GetCurrentQueueValue() {
        StringBuilder sb = new StringBuilder();
        foreach ( char item in _lastFour ) {
            sb.Append(item);
        }
        return sb.ToString();
    }

    private bool IsStartOfPacket() {
        HashSet<char> chars = new HashSet<char>();
        foreach ( char item in _lastFour ) {
            chars.Add(item);
        }
        if (chars.Count == _uniqueCharsRequired) {
            return true;
        }
        return false;
    }
}