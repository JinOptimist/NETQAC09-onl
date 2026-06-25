namespace ConsoleApp1;
//основной класс игры
public class GameKorolev
{
    OnlyNumbersBro helper = new OnlyNumbersBro(); // сразу проверки на число создаем чтоб все функции видели
    Random random = new Random(); // рандом для бота задаем
    GuessKorolev core = new GuessKorolev(); //вызов класса угадайки

    //основная игра (только сам выбор)
    public void Start()
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"
                    GUESS THE NUMBER OR SUFFER 🫵
                       === YOUR CHOOSE? ===

            1. Play with your stupid friend
            2. Play with beautiful and cool Bot
            3. Exit
            ================");
            Console.ResetColor();

            var choice = helper.ReadNumber("Enter your choice: ");

            if (choice == 1) PlayWithFriend();
            else if (choice == 2) PlayWithBot();
            else if (choice == 3)
            {
                Console.WriteLine("chao! see you next time 👊");
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("HEY! YOU CAN ENTER ONLY 1 OR 2 OR 3 !!!");
                Console.ResetColor();
                Console.WriteLine();
            }
        }
    }

    // игра с другом 
    public void PlayWithFriend()
    {
        Console.WriteLine(@" === So, you want to play with your stupid human friend ===
Understand 😆😆😆
Rules: Guess the number (1-100) with maximum 7 attempts.
================\n");

        var secretNumber = 0;
        do
        {
            secretNumber = helper.ReadNumber("Player 1. Enter your number (1-100): ");
        } while (secretNumber < 1 || secretNumber > 100);

        Console.Clear();
        
        // вызываем угадайку с передачей в нее параметров
        core.GuessLoop(7, 1, 100, secretNumber, isBotMode: false);
    }

    // игра с ботом
    public void PlayWithBot()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("ARE YOU READY TO PLAY WITH BOT? I WILL CRUSH YOU, LITTLE PIECE OF SHIT!!! \n");
        Console.ResetColor();

        var minBound = helper.ReadNumber("Enter LOWER bound (min): ");
        var maxBound = 0;

        while (true)
        {
            maxBound = helper.ReadNumber("Enter UPPER bound (max): ");
            if (maxBound > minBound) break;
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"COME ON! Enter a number GREATER than {minBound}!");
            Console.ResetColor();
        }

        var range = maxBound - minBound + 1;
        var maxAttempts = (int)Math.Ceiling(Math.Log(range, 2));

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n[BOT]: For a range of {range} numbers, math says you need maximum {maxAttempts} attempts.\n");
        Console.ResetColor();

        var botMagicNumber = random.Next(minBound, maxBound + 1);

        // вызываем угадайку для бота
        core.GuessLoop(maxAttempts, minBound, maxBound, botMagicNumber, isBotMode: true);
    }
}