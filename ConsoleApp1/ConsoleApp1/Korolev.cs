public class Korolev
{
    public void Do()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(@"
                    GUESS THE NUMBER OR SUFFER 🫵");
        var playWith = 0;

        // защита от дурака на шаге выбора
        while (true)
        {
            Console.WriteLine(@"
                       === YOUR CHOOSE? ===

            1. Play with your stupid friend
            2. Play with beautiful and cool Bot
            3. Exit
            ================");
            var playWithDecisionString = Console.ReadLine();

            if (int.TryParse(playWithDecisionString, out playWith))
            {

                if (playWith == 1 || playWith == 2 || playWith == 3)
                {
                    break;
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("HEY! YOU CAN ENTER ONLY 1 OR 2 OR 3 !!!");
            Console.ResetColor();
            Console.WriteLine();
        }

        // ИГРА С ДРУГОМ (по старым правилам - загадывает от 1 до 100, 7 попыток у нас

        if (playWith == 1)
        {
            Console.WriteLine(@" === So, you want to play with your stupid human friend ===
Understand 😆😆😆

Here are some rules:
1. Your friend (player 1) type some number from 1 to 100 into console
2. You (player 2) try to guess it with maximum 7 attempts. 
3. If you can't - you are loser. If you guess - you are winner!

Are you ready???

================");
            //хардкодим правила
            var MAX_ATTEMPT = 7;
            var MAX_NUMBER = 100;
            var MIN_NUMBER = 1;

            var userMagicNumber = 0;
            bool isNumber;
            do
            {
                Console.WriteLine("Player 1. Enter your number! ");

                var userMagicNumberText = Console.ReadLine();
                isNumber = int.TryParse(userMagicNumberText, out userMagicNumber);

                if (!isNumber)
                {
                    Console.WriteLine("It's not a number");
                }
                else if (userMagicNumber > MAX_NUMBER)
                {
                    Console.WriteLine($"Too big number. Must be less then {MAX_NUMBER}");
                }
                else if (userMagicNumber < MIN_NUMBER)
                {
                    Console.WriteLine($"Too small number. Must be more then {MIN_NUMBER}");
                }
            } while (!isNumber
                     || userMagicNumber < MIN_NUMBER
                     || userMagicNumber > MAX_NUMBER);

            Console.Clear();
            var attempt = 0;
            int guess;
            var isWin = false;
            do
            {
                attempt++;
                //Console.WriteLine("User 2. Enter your guess. Attemmpt [" + attempt + " / " + MAX_ATTEMPT + "]");
                Console.WriteLine($"Player 2. Enter your guess. Attemmpt [{attempt} / {MAX_ATTEMPT}]");
                var guessText = Console.ReadLine();
                guess = int.Parse(guessText);

                if (guess < userMagicNumber)
                {
                    Console.WriteLine("Our number is bigger");
                }
                else if (guess > userMagicNumber)
                {
                    Console.WriteLine("Our number is less");
                }
                else if (guess == userMagicNumber)
                {
                    isWin = true;
                }
            } while (!isWin && attempt < MAX_ATTEMPT);


            if (isWin)
            {
                Console.WriteLine("Right! PLAYER 2 WIN");
            }
            else
            {
                Console.WriteLine("Ohhh, PLAYER 2 LOOSE");
            }

        }

        // ИГРА С БОТОМ. БОТ ПОПРОСИТ ВВЕСТИ ГРАНИЦЫ, ДАСТ ОПТИМАЛЬНОЕ КОЛИЧЕСТВО ПОПЫТОК И БУДЕТ ПОКАЗЫВАТЬ КУДА УГАДЫВАТЬ
        if (playWith == 2)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ARE YOU READY TO PLAY WITH BOT? I WILL CRUSH YOU, LITTLE PIECE OF SHIT!!! ");
            Console.WriteLine(@"=== HERE ARE THE RULES BRO:
1. You set the min and max numbers.
2. I will calculate the PERFECT number of attempts using math.
3. I will create random number
4. You try to guess, and I will track your to help
================
");
// задание границ (нижней и верхней)
            var minBound = 0;
            var maxBound = 0;

            
            while (true)
            {
                Console.Write("Enter LOWER bound (min): ");
                
                if (int.TryParse(Console.ReadLine(), out minBound))
                {
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ARE YOU BLIND??? Enter valid number!");
                Console.ResetColor();
            }
            
            while (true)
            {
                Console.Write("Enter UPPER bound (max): ");
                
                if (int.TryParse(Console.ReadLine(), out maxBound) && maxBound > minBound)
                {
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"COME OM! Enter a number GREATER than {minBound}!");
                Console.ResetColor();
            }
            
// РАССЧЕТ ПОПЫТОК

            var range = maxBound - minBound + 1;
            var maxAttempts = (int)Math.Ceiling(Math.Log(range, 2));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n[BOT]: For a range of {range} numbers, math says you need maximum {maxAttempts} attempts.");
            Console.WriteLine("Let's see if your meat brain can handle it!\n");
            Console.ResetColor();

            var currentMin = minBound;
            var currentMax = maxBound;
            var botAttempt = 0;
            var botMagicNumber = new Random().Next(minBound, maxBound + 1);
            var isBotWin = false;

            // погнали играть
            
            while (botAttempt < maxAttempts)
            {
                botAttempt++; 
                
                // показываем куда гадать
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"-> Hint: The number is somewhere between [{currentMin} and {currentMax}]");
                Console.ResetColor();
                
                Console.Write($"Attempt [{botAttempt} / {maxAttempts}]. Your guess: ");
                
                int playerGuess;
                if (!int.TryParse(Console.ReadLine(), out playerGuess))
                {
                    Console.WriteLine("That's not even a number, stupid little human 😤");
                    continue; 
                }

                if (playerGuess < botMagicNumber)
                {
                    Console.WriteLine("HA-HA-HA! My number is BIGGER.");
                    // Сужаем диапазон снизу (но не выходим за рамки текущего макса)
                    if (playerGuess >= currentMin) currentMin = playerGuess + 1;
                }
                else if (playerGuess > botMagicNumber)
                {
                    Console.WriteLine("NOPE, BRO! My number is LESS.");
                    // Сужаем диапазон сверху
                    if (playerGuess <= currentMax) currentMax = playerGuess - 1;
                }
                else
                {
                    isBotWin = true;
                    break;
                }
                Console.WriteLine();
            }

            // Итоги игры
            if (isBotWin)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nLucky bastard... You guessed it in {botAttempt} attempts! 😤");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nLOSER! You wasted all {maxAttempts} attempts. My number was {botMagicNumber}! 🖕");
            }
            Console.ResetColor();
        }

        if (playWith == 3)
        {
            Console.WriteLine("chao! see you next time 👊");
            Environment.Exit(0);
        }
    }
}