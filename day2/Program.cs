using System.Collections;

using FileStream fs = File.OpenRead("input.txt");
using var sr = new StreamReader(fs);

string line;
int score = 0;
while ((line = sr.ReadLine()) != null)
{
    var game = line.Split(" ");
    string opponentMove = game[0];
    string ourGoal = game[1];

    string ourMove = GetOurMove(opponentMove, ourGoal);

    score += GetPointsForMove(ourMove);
    score += GetPointsForResult(opponentMove, ourMove);
}

Console.WriteLine(score);

static string GetOurMove(string opponentMove, string ourGoal) {
    // Opponent moves - A:Rock, B:Paper, C:Scissors
    // Our goal - X:Lose, Y:Draw, Z:Win
    // output - X:Rock, Y:Paper, Z:Scissors
    var moveFind = new Dictionary<string, string>() {
        { "AX", "Z" },
        { "AY", "X" },
        { "AZ", "Y" },
        { "BX", "X" },
        { "BY", "Y" },
        { "BZ", "Z" },
        { "CX", "Y" },
        { "CY", "Z" },
        { "CZ", "X" }
    };

    return moveFind[opponentMove + ourGoal];
}

static int GetPointsForMove(string move) {
    var movePoints = new Dictionary<string, int>() {
        { "X", 1 },
        { "Y", 2 },
        { "Z", 3 }
    };

    return movePoints[move];
}

static int GetPointsForResult(string opponentMove, string ourMove) {
    var gameResults = new Dictionary<string, int>() {
        { "AX", 3 },
        { "AY", 6 },
        { "AZ", 0 },
        { "BX", 0 },
        { "BY", 3 },
        { "BZ", 6 },
        { "CX", 6 },
        { "CY", 0 },
        { "CZ", 3 }
    };

    return gameResults[$"{opponentMove}{ourMove}"];
}