namespace day5.console;

using System.Collections.Generic;
using System.Text;

public class CrateManager {
    Dictionary<int, Stack<char>> stacks;

    public CrateManager(Dictionary<int, Stack<char>> stacks) {
        this.stacks = stacks;
    }

    public void PushToStack(int stack, char value) {
        if (!stacks.ContainsKey(stack)) {
            stacks[stack] = new Stack<char>();
        }
        stacks[stack].Push(value);
    }

    public void MoveCrates(int source, int destination, int count) {
        for (int i = 0; i < count; i++) {
            MoveCrate(source, destination);
        }
    }

    public void FlipAllStacks() {
        foreach ( int stackId in stacks.Keys ) {
            Stack<char> stack = stacks[stackId];
            Stack<char> temp = new Stack<char>();
            while (stack.Count > 0) {
                temp.Push(stack.Pop());
            }
            stacks[stackId] = temp;
        }
    }

    public string GetTopsOfStacks() {
        var topBuilder = new StringBuilder();
        for ( int i = 1; i <= stacks.Count; i++ ) {
            char top = stacks[i].Peek();
            Console.WriteLine($"{top} is on top of {i}");
            topBuilder.Append(top);
        }
        return topBuilder.ToString();
    }

    private void MoveCrate(int source, int destination) {
        char crate = stacks[source].Pop();
        stacks[destination].Push(crate);
    }
}