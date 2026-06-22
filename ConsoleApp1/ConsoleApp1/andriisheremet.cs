using System;

class Andriisheremet
{
    public void Do()
    {
        Console.WriteLine("=== The upgraded game: Guess the number ===");

        // 1) Выбор диапазона
        int minNumber = 0;
        int maxNumber = 0;
        while (minNumber >= maxNumber)
        {
            Console.Write("Enter MIN boundary of the range: ");
            int.TryParse(Console.ReadLine(), out minNumber);

            Console.Write("Enter MAX boundary of the range: ");
            int.TryParse(Console.ReadLine(), out maxNumber);

            if (minNumber >= maxNumber)
            {
                Console.WriteLine("Error: MAX must be strictly greater than MIN. Try again.\n");
            }
        }

        // 2) Вычисление максимального количества попыток ( Бинарный поиск )
        // Формула: Log2(диапазон) + 1. Округляем в большую сторону.
        int range = maxNumber - minNumber + 1;
        int maxAttempts = (int)Math.Ceiling(Math.Log2(range));
        Console.WriteLine($"\nBased on your range, the maximum number of attempts is: {maxAttempts}");

        // 3) Выбор: кто загадывает число (1 - Человек, 2 - Компьютер)
        int gameMode = 0;
        while (gameMode != 1 && gameMode != 2)
        {
            Console.WriteLine("\nWho will guess the number?");
            Console.WriteLine("1. User 1 (Man)");
            Console.WriteLine("2. Computer");
            Console.Write("Select mode (1 or 2): ");
            int.TryParse(Console.ReadLine(), out gameMode);
        }

        int magicNumber = 0;

        if (gameMode == 1)
        {
            // Загадывает человек (с вашей старой проверкой)
            bool isNumber;
            do
            {
                Console.WriteLine($"\nUser 1. Enter Magic number ({minNumber} to {maxNumber}):");
                var text = Console.ReadLine();
                isNumber = int.TryParse(text, out magicNumber);

                if (!isNumber || magicNumber < minNumber || magicNumber > maxNumber)
                {
                    Console.WriteLine($"Invalid entry. Number must be between {minNumber} and {maxNumber}.");
                    isNumber = false;
                }
            } while (!isNumber);

            Console.Clear(); // Прячем число от Угадывающего
        }
        else
        {
            // Загадывает компьютер
            Random random = new Random();
            magicNumber = random.Next(minNumber, maxNumber + 1); // maxNumber включительно
            Console.WriteLine("\n[Computer has generated a hidden number! Let's play.]");
        }

        // Переменные для динамического изменения диапазона (Подсказки)
        int currentMin = minNumber;
        int currentMax = maxNumber;

        int attempt = 0;
        bool isWin = false;

        // Основной цикл угадывания
        while (!isWin && attempt < maxAttempts)
        {
            // 4) Вывод подсказки о текущем актуальном диапазоне для игрока
            Console.WriteLine($"\nHint: The number is somewhere between [{currentMin} and {currentMax}]");
            Console.WriteLine($"Attempt [{attempt + 1} / {maxAttempts}]");
            Console.Write("Enter your guess: ");

            var guessText = Console.ReadLine();
            if (!int.TryParse(guessText, out int guess))
            {
                Console.WriteLine("It's not a valid number! Try again (attempt not counted).");
                continue; // 5) Не считаем за попытку
            }

            // 5) Не считать за попытку число вне разрешённого изначально диапазона
            if (guess < minNumber || guess > maxNumber)
            {
                Console.WriteLine($"Out of global range! Must be between {minNumber} and {maxNumber} (attempt not counted).");
                continue;
            }

            // Если ввод корректный — только теперь засчитываем попытку
            attempt++;

            if (guess < magicNumber)
            {
                Console.WriteLine("Our number is BIGGER");
                // Сужаем динамический диапазон для подсказки
                if (guess >= currentMin)
                    currentMin = guess + 1;
            }
            else if (guess > magicNumber)
            {
                Console.WriteLine("Our number is LESS");
                // Сужаем динамический диапазон для подсказки
                if (guess <= currentMax)
                    currentMax = guess - 1;
            }
            else
            {
                isWin = true;
            }
        }

        // Финал игры
        Console.WriteLine("\n=============================");
        if (isWin)
        {
            Console.WriteLine($"Right! You Win in {attempt} attempts!");
        }
        else
        {
            Console.WriteLine($"Loooose! The secret number was: {magicNumber}");
        }
        Console.WriteLine("=============================");
    }
}