namespace day11.console;

public class MonkeyParser {
    private List<Monkey> monkeys;
    private Monkey currentMonkey;
    private UInt64 currentTestDivisible;
    private int currentTrueMonkeyId;
    public MonkeyParser() {
        monkeys = new List<Monkey>();
    }

    public void ParseLine(string line) {
        if (line.StartsWith("Monkey")) {
            currentMonkey = new Monkey(int.Parse(line.Split(" ")[1].Replace(":", "")));
            monkeys.Add(currentMonkey);
        } else if (line.StartsWith("  Starting items:")) {
            string ints = line.Split(":")[1].Trim();
            string[] nums = ints.Split(",");
            foreach (string num in nums) {
                currentMonkey.CatchItem(UInt64.Parse(num.Trim()));
            }
        } else if (line.StartsWith("  Operation")) {
            currentMonkey.AddOperation(GetOperationFromLine(line));
        } else if (line.StartsWith("  Test:")) {
            SetCurrentTest(line);
        } else if (line.Trim().StartsWith("If true: ")) {
            SetCurrentTrue(line);
        } else if (line.Trim().StartsWith("If false: ")) {
            currentMonkey.AddTest(GetTestFromLine(line));
        }
    }

    private Func<UInt64, UInt64> GetOperationFromLine(string line) {
        line = line.Replace("Operation: new = old ", "").Trim();
        string[] operation = line.Split(" ");
        if (operation[1] == "old") {
            return (x) => x * x;
        }
        UInt64 val = UInt64.Parse(operation[1]);
        if (operation[0] == "*") {
            return (x) => x * val;
        } else {
            return (x) => x + val;
        }
    }

    private Func<UInt64, int> GetTestFromLine(string line) {
        line = line.Replace("If false: throw to monkey ", "").Trim();
        UInt64 testDivisible = currentTestDivisible;
        int falseId = int.Parse(line);
        int trueId = currentTrueMonkeyId;
        //Console.WriteLine($"Monkey {currentMonkey.Id} test: If input % {testDivisible} == 0 ? {trueId} : {falseId}");
        return new Func<UInt64, int>(x => 
            {
                if ( x % testDivisible == 0 ) {
                    return trueId;
                }
                return falseId;
            }
        );
    }

    private void SetCurrentTest(string line) {
        line = line.Replace("Test: divisible by ", "").Trim();
        currentTestDivisible = UInt64.Parse(line);
    }

    private void SetCurrentTrue(string line) {
        line = line.Replace("If true: throw to monkey ","").Trim();
        currentTrueMonkeyId = int.Parse(line);
    }

    public List<Monkey> GetMonkeys() {
        return monkeys;
    }
}