// See https://aka.ms/new-console-template for more information

string playerName;
int menuOption;
int score = 0;
List<Tuple<string,int>> previousScores = new List<Tuple<string, int>>();
Random random = new Random();
Console.WriteLine("Enter your name: ");
playerName = Console.ReadLine();
Console.WriteLine($"Hello, {playerName} Welcome to the Math Game!");
Console.WriteLine("Press any key to start the game...");
Console.ReadKey();
Console.Clear();
bool wantToPlay = true;
while (wantToPlay)
{
    menuOption = ShowMainMenu(playerName);
    if (menuOption == 6)
    {
        Console.WriteLine("Thank you for playing the Math Game!");
        return;
    }

    Console.Clear();
    if (menuOption == 0)
    {
        ShowPreviousScores();
        continue;
    }
    Console.WriteLine("Enter the number of rounds: ");
    int rounds = int.Parse(Console.ReadLine());
    Console.Clear();
    switch (menuOption)
    {
        case 1:
            ShowAddition(rounds);
            previousScores.Add(new Tuple<string, int>("Addition Game",score));
            break;
        case 2:
            ShowSubtraction(rounds);
            previousScores.Add(new Tuple<string, int>("Subtraction Game",score));
            break;
        case 3:
            ShowMultiplication(rounds);
            previousScores.Add(new Tuple<string, int>("Mulitplication Game",score));
            break;
        case 4:
            ShowDivision(rounds);
            previousScores.Add(new Tuple<string, int>("Division Game",score));
            break;
        case 5:
            ShowRandom(rounds);
            previousScores.Add(new Tuple<string, int>("Random Game",score));
            break;
    }
    Console.Clear();
    Console.WriteLine($"Your score was: {score}...");
    Console.ReadKey();
    score = 0;
}

void ShowAddition(int rounds)
{
    for (int i = 0; i < rounds; i++)
    {
        Console.WriteLine("Addition Game");
        var first = random.Next(1, 10);
        var second = random.Next(1, 10);
        int result = first + second;
        Console.WriteLine($"What is {first} + {second}?");
        var playerAnswer = int.Parse(Console.ReadLine());
        Console.WriteLine(playerAnswer == result ? "Correct!" : "Incorrect!");
        score += playerAnswer == result ? 1 : 0;
        Console.WriteLine($"Your score is {score}");
        Console.WriteLine("Press any key to return to continue...");
        Console.ReadKey();
        Console.Clear();
    }
}

void ShowSubtraction(int rounds)
{
    for (int i = 0; i < rounds; i++)
    {
        Console.WriteLine("Subtraction Game");
        var first = random.Next(1, 10);
        var second = random.Next(1, 10);
        int result = first - second;
        Console.WriteLine($"What is {first} - {second}?");
        var playerAnswer = int.Parse(Console.ReadLine());
        Console.WriteLine(playerAnswer == result ? "Correct!" : "Incorrect!");
        score += playerAnswer == result ? 1 : 0;
        Console.WriteLine($"Your score is {score}");
        Console.WriteLine("Press any key to return to continue...");
        Console.ReadKey();
        Console.Clear();
    }
}

void ShowMultiplication(int rounds)
{
    for (int i = 0; i < rounds; i++)
    {
        Console.WriteLine("Multiplication Game");
        var first = random.Next(1, 10);
        var second = random.Next(1, 10);
        int result = first * second;
        Console.WriteLine($"What is {first} * {second}?");
        var playerAnswer = int.Parse(Console.ReadLine());
        Console.WriteLine(playerAnswer == result ? "Correct!" : "Incorrect!");
        score += playerAnswer == result ? 1 : 0;
        Console.WriteLine($"Your score is {score}");
        Console.WriteLine("Press any key to return to continue...");
        Console.ReadKey();
        Console.Clear();
    }
}

void ShowDivision(int rounds)
{
    for (int i = 0; i < rounds; i++)
    {
        Console.WriteLine("Division Game");
        var first = random.Next(1, 10);
        var second = random.Next(1, 10);
        if (first % second != 0)
        {
            i--;
            Console.Clear();
            continue;
        }

        int result = first / second;
        Console.WriteLine($"What is {first} / {second}?");
        var playerAnswer = int.Parse(Console.ReadLine());
        Console.WriteLine(playerAnswer == result ? "Correct!" : "Incorrect!");
        score += playerAnswer == result ? 1 : 0;
        Console.WriteLine($"Your score is {score}");
        Console.WriteLine("Press any key to return to continue...");
        Console.ReadKey();
        Console.Clear();
    }
}

void ShowRandom(int rounds)
{
    for (int i = 0; i < rounds; i++)
    {
        var mode = random.Next(1, 5);
        switch (mode)
        {
            case 1:
                ShowAddition(1);
                break;
            case 2:
                ShowSubtraction(1);
                break;
            case 3:
                ShowMultiplication(1);
                break;
            case 4:
                ShowDivision(1);
                break;
        }
    }
}

void ShowPreviousScores()
{
    Console.WriteLine("Previous Scores");
    int i = 0;
    foreach (var score in previousScores)
    {
        i++;
        Console.WriteLine($"{i}. {score.Item1} - {score.Item2}");
    }
    Console.WriteLine("Press any key to return to continue...");
    Console.ReadKey();
}

int ShowMainMenu(string? s)
{
    Console.WriteLine($"{s}, Choose the Game Mode: ");
    Console.WriteLine("0. Previous Scores");
    Console.WriteLine("1. Addition");
    Console.WriteLine("2. Subtraction");
    Console.WriteLine("3. Multiplication");
    Console.WriteLine("4. Division");
    Console.WriteLine("5. Random");
    Console.WriteLine("6. Exit");
    Console.WriteLine("Enter your choice: ");
    return int.Parse(Console.ReadLine());
}