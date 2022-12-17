namespace day13.console;

using System.Text;

public class Packet {
    private int index;
    private Stack<char> left;
    private Stack<char> right;
    
    public Packet(string left, string right, int index) {
        this.index = index;
        this.left = new Stack<char>();
        this.right = new Stack<char>();
        PopulateStack(left, this.left);
        PopulateStack(right, this.right);
    }

    private void PopulateStack(string input, Stack<char> stack) {
        char[] stringChars = input.ToArray();
        for (int i = stringChars.Count() - 1; i >= 0; i--) {
            stack.Push(stringChars[i]);
        }
    }

    public int Index {
        get { return index; }
    }

    public Stack<char> Left {
        get { return left; }
    }

    public string LeftString() {
        return StackToString(left);
    }

    public Stack<char> Right {
        get { return right; }
    }

    public string RightString() {
        return StackToString(right);
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