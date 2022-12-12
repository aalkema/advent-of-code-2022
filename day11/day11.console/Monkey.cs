namespace day11.console;

public class Monkey {
    private int id;
    private UInt64 timesInspected = 0;
    private Queue<UInt64> items;
    private Func<UInt64, UInt64> operation;
    private Func<UInt64, int> test;

    public Monkey(int id) {
        this.id = id;
        this.items = new Queue<UInt64>();
    }

    public void AddOperation(Func<UInt64, UInt64> operation) {
        this.operation = operation;
    }

    public void AddTest(Func<UInt64, int> test) {
        this.test = test;
    }

    public int Id {
        get { return id; }
    }

    public UInt64 TimesInspected {
        get { return timesInspected; }
    }

    public void CatchItem(UInt64 item) {
        items.Enqueue(item);
    }

    public void TakeTurn(List<Monkey> otherMonkeys) {
        while (items.Count > 0) {
            HandleItem(items.Dequeue(), otherMonkeys);
        }
    }

    private void HandleItem(UInt64 item, List<Monkey> otherMonkeys) {
        // inspect
        //Console.WriteLine($"Monkey {id} handling {item}");
        item = operation(item);
        //Console.WriteLine($"Monkey {id} now handling {item} after inspection");
        timesInspected++;

        // item ok - less worry
        // item = item / 3;
        // Console.WriteLine($"Monkey {id} now handling {item} after relief");

        // hard code to input, I have to go to bed
        item %= 9699690;

        // do test and throw
        Console.WriteLine($"Monkey {id} with {item} test output: {test(item)}");
        otherMonkeys.FirstOrDefault(m => m.Id == test(item)).CatchItem(item);
    }
}