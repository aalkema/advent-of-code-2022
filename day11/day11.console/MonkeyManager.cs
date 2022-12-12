namespace day11.console;

public class MonkeyManager {
    List<Monkey> monkeys;
    public MonkeyManager(List<Monkey> monkeys) {
        this.monkeys = monkeys;
    }

    public void PerformRounds(int numberOfRounds) {
        monkeys = monkeys.OrderBy(m => m.Id).ToList();
        for (int i = 0; i < numberOfRounds; i++) {
            PerformRound();
        }
    }

    public UInt64 GetMonkeyBusiness() {
        monkeys = monkeys.OrderByDescending(m => m.TimesInspected).ToList();
        return monkeys[0].TimesInspected * monkeys[1].TimesInspected;
    }

    private void PerformRound() {
        foreach (Monkey monkey in monkeys) {
            monkey.TakeTurn(monkeys);
        }
    }
}