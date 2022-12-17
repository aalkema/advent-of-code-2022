namespace day13.console;

public class PacketComparer : IComparer<SinglePacket> {
    public int Compare(SinglePacket left, SinglePacket right) {
        while (left.Packet.Count > 0 && right.Packet.Count > 0) {
            Result result = CompareNextCharacters(left, right);
            if (result == Result.Wrong) {
                left.Reset();
                right.Reset();
                return 1;
            } else if (result == Result.Correct) {
                left.Reset();
                right.Reset();
                return -1;
            }
        }
        left.Reset();
        right.Reset();
        return 0;
    }

    private Result CompareNextCharacters(SinglePacket left, SinglePacket right) {
        char topOfRight = right.Packet.Pop();
        char topOfLeft = left.Packet.Pop();
        if (char.IsDigit(topOfRight) && char.IsDigit(right.Packet.Peek())) {
            // close enough to 10
            topOfRight = ':';
            right.Packet.Pop();
        }
        if (char.IsDigit(topOfLeft) && char.IsDigit(left.Packet.Peek())) {
            topOfLeft = ':';
            left.Packet.Pop();
        }
        /*Console.WriteLine("NEXT:");
        Console.WriteLine($"Right is {topOfRight}");
        Console.WriteLine($"Left is {topOfLeft}");
        Console.WriteLine($"left: {packet.LeftString()}");
        Console.WriteLine($"right: {packet.RightString()}");*/
        switch (topOfRight) {
            case ']':
                return TestWhenRightIsEndOfArray(left, right, topOfLeft, topOfRight);
            case ',':
                return TestWhenRightIsComma(left, right, topOfLeft, topOfRight);
            case '[':
                return TestWhenRightIsBeginOfArray(left, right, topOfLeft, topOfRight);
            default:
                return TestWhenRightIsNumber(left, right, topOfLeft, topOfRight);
        }
    }

    private Result TestWhenRightIsNumber(SinglePacket left, SinglePacket right, char topOfLeft, char topOfRight) {
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
            left.Packet.Push(topOfLeft);
            right.Packet.Push(']');
            right.Packet.Push(topOfRight);
            right.Packet.Push('[');
            return Result.Continue;
        } else {
            Console.WriteLine($"Unexpected: Right is digit, left is comma");
            return Result.Continue;
        }
    }

    private Result TestWhenRightIsBeginOfArray(SinglePacket left, SinglePacket right, char topOfLeft, char topOfRight) {
        if (topOfLeft == '[') {
            return Result.Continue;
        } else if (CheckIfCharIsDigit(topOfLeft)) {
            right.Packet.Push(topOfRight);
            left.Packet.Push(']');
            left.Packet.Push(topOfLeft);
            left.Packet.Push('[');
            return Result.Continue;
        } else if (topOfLeft == ']') {
            return Result.Correct;
        } else {
            Console.WriteLine($"Unexpected: Right is begin array, left is comma");
            return Result.Continue;
        }
    }

    private Result TestWhenRightIsComma(SinglePacket left, SinglePacket right, char topOfLeft, char topOfRight) {
        if (topOfLeft == ',') {
            return Result.Continue;
        } else if (topOfLeft == ']') {
            return Result.Correct;
        } else {
            Console.WriteLine($"Unexpected, right is comma, left is {topOfLeft}");
            return Result.Continue;
        }
    }

    private Result TestWhenRightIsEndOfArray(SinglePacket left, SinglePacket right, char topOfLeft, char topOfRight) {
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