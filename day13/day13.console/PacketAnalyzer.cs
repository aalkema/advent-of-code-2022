namespace day13.console;

enum Result {
    Continue,
    Wrong,
    Correct
}

public class PacketAnalyzer {
    int indexSum = 0;

    public int GetDecoder(List<SinglePacket> packets) {
        packets.Sort(new PacketComparer());
        int twoLoc = packets.FindIndex(p => p.OriginalInput == "[[2]]") + 1;
        int sixLoc = packets.FindIndex(p => p.OriginalInput == "[[6]]") + 1;
        for (int i = 0; i < 10; i++) {
            Console.WriteLine(packets[i].PacketString());
        }
        return twoLoc * sixLoc;
    }

    public int SumOfCorrectPacketIndicies(List<Packet> packets) {
        foreach (Packet packet in packets) {
            Console.WriteLine($"Testing ({packet.Index}): left: {packet.LeftString()}, right: {packet.RightString()}");
            if (IsInRightOrder(packet)) {
                Console.WriteLine($"Right order({packet.Index}), left: {packet.LeftString()}, right: {packet.RightString()}");
                indexSum+= packet.Index;
            } else {
                Console.WriteLine($"Wrong order({packet.Index}), left: {packet.LeftString()}, right: {packet.RightString()}");
            }
        }
        Console.WriteLine();
        Console.WriteLine();
        return indexSum;
    }

    private bool IsInRightOrder(Packet packet) {
        while (packet.Left.Count > 0 && packet.Right.Count > 0) {
            Result result = CompareNextCharacters(packet);
            if (result == Result.Wrong) {
                return false;
            } else if (result == Result.Correct) {
                return true;
            }
        }
        return true;
    }

    private Result CompareNextCharacters(Packet packet) {
        char topOfRight = packet.Right.Pop();
        char topOfLeft = packet.Left.Pop();
        if (char.IsDigit(topOfRight) && char.IsDigit(packet.Right.Peek())) {
            // close enough to 10
            topOfRight = ':';
            packet.Right.Pop();
        }
        if (char.IsDigit(topOfLeft) && char.IsDigit(packet.Left.Peek())) {
            topOfLeft = ':';
            packet.Left.Pop();
        }
        Console.WriteLine("NEXT:");
        Console.WriteLine($"Right is {topOfRight}");
        Console.WriteLine($"Left is {topOfLeft}");
        Console.WriteLine($"left: {packet.LeftString()}");
        Console.WriteLine($"right: {packet.RightString()}");
        switch (topOfRight) {
            case ']':
                return TestWhenRightIsEndOfArray(packet, topOfLeft, topOfRight);
            case ',':
                return TestWhenRightIsComma(packet, topOfLeft, topOfRight);
            case '[':
                return TestWhenRightIsBeginOfArray(packet, topOfLeft, topOfRight);
            default:
                return TestWhenRightIsNumber(packet, topOfLeft, topOfRight);
        }
    }

    private Result TestWhenRightIsNumber(Packet packet, char topOfLeft, char topOfRight) {
        if (CheckIfCharIsDigit(topOfLeft)) {
            if (topOfLeft < topOfRight) {
                return Result.Correct;
            } else if (topOfLeft > topOfRight) {
                return Result.Wrong;
            } else {
                return Result.Continue;
            }
        } else if (topOfLeft == ']') {
            return Result.Correct;
        } else if (topOfLeft == '[') {
            packet.Left.Push(topOfLeft);
            packet.Right.Push(']');
            packet.Right.Push(topOfRight);
            packet.Right.Push('[');
            return Result.Continue;
        } else {
            Console.WriteLine($"Unexpected({packet.Index}): Right is digit, left is comma");
            return Result.Continue;
        }
    }

    private Result TestWhenRightIsBeginOfArray(Packet packet, char topOfLeft, char topOfRight) {
        if (topOfLeft == '[') {
            return Result.Continue;
        } else if (CheckIfCharIsDigit(topOfLeft)) {
            packet.Right.Push(topOfRight);
            packet.Left.Push(']');
            packet.Left.Push(topOfLeft);
            packet.Left.Push('[');
            return Result.Continue;
        } else if (topOfLeft == ']') {
            return Result.Correct;
        } else {
            Console.WriteLine($"Unexpected ({packet.Index}): Right is begin array, left is comma");
            return Result.Continue;
        }
    }

    private Result TestWhenRightIsComma(Packet packet, char topOfLeft, char topOfRight) {
        if (topOfLeft == ',') {
            return Result.Continue;
        } else if (topOfLeft == ']') {
            return Result.Correct;
        } else {
            Console.WriteLine($"Unexpected ({packet.Index}), right is comma, left is {topOfLeft}");
            return Result.Continue;
        }
    }

    private Result TestWhenRightIsEndOfArray(Packet packet, char topOfLeft, char topOfRight) {
        if (topOfLeft == ']') {
            return Result.Continue;
        } else {
            return Result.Wrong;
        }
    }

    private bool CheckIfCharIsDigit(char character) {
        if (character == ':') {
            return true;
        }
        return char.IsDigit(character);
    }
}