using System;

namespace ConsoleApp1
{
    public class GameLvou
    {
        // Поля класса (состояние игры, к которому есть доступ из любого метода внутри класса)
        private int _minNumber;
        private int _maxNumber;
        private int _maxAttempts;
        private int _magicNumber;

        // Главный публичный метод, который запускает всю игру
        public void Start()
        {
            Console.WriteLine("=== Welcome to GameLvou: Guess the number ===");

            SetupRange();
            CalculateAttempts();
            SelectGameMode();
            PlayGuessingLoop();
        }

        // 1. Метод для настройки диапазона
        private void SetupRange()
        {
            while (_minNumber >= _maxNumber)
            {
                Console.Write("Enter MIN boundary of the range: ");
                int.TryParse(Console.ReadLine(), out _minNumber);

                Console.Write("Enter MAX boundary of the range: ");
                int.TryParse(Console.ReadLine(), out _maxNumber);

                if (_minNumber >= _maxNumber)
                {
                    Console.WriteLine("Error: MAX must be greater than MIN. Try again.\n");
                }
            }
        }

        // 2. Метод для математического расчета попыток
        private void CalculateAttempts()
        {
            int range = _maxNumber - _minNumber + 1;
            _maxAttempts = (int)Math.Ceiling(Math.Log2(range));
            Console.WriteLine($"\nBased on your range, maximum attempts: {_maxAttempts}");
        }

        // 3. Метод выбора режима игры и загадывания числа
        private void SelectGameMode()
        {
            int gameMode = 0;
            while (gameMode != 1 && gameMode != 2)
            {
                Console.WriteLine("\nWho will guess the number?");
                Console.WriteLine("1. User 1 (Man)\n2. Computer");
                Console.Write("Select mode (1 or 2): ");
                int.TryParse(Console.ReadLine(), out gameMode);
            }

            if (gameMode == 1)
            {
                bool isNumber;
                do
                {
                    Console.WriteLine($"\nUser 1. Enter Magic number ({_minNumber} to {_maxNumber}):");
                    isNumber = int.TryParse(Console.ReadLine(), out _magicNumber);

                    if (!isNumber || _magicNumber < _minNumber || _magicNumber > _maxNumber)
                    {
                        Console.WriteLine("Invalid entry. Try again.");
                        isNumber = false;
                    }
                } while (!isNumber);

                Console.Clear();
            }
            else
            {
                Random random = new Random();
                _magicNumber = random.Next(_minNumber, _maxNumber + 1);
                Console.WriteLine("\n[Computer has generated a hidden number!]");
            }
        }

        // 4. Основной игровой цикл угадывания
        private void PlayGuessingLoop()
        {
            int currentMin = _minNumber;
            int currentMax = _maxNumber;
            int attempt = 0;
            bool isWin = false;

            while (!isWin && attempt < _maxAttempts)
            {
                Console.WriteLine($"\nHint: The number is between [{currentMin} and {currentMax}]");
                Console.WriteLine($"Attempt [{attempt + 1} / {_maxAttempts}]");
                Console.Write("Enter your guess: ");

                if (!int.TryParse(Console.ReadLine(), out int guess) || guess < _minNumber || guess > _maxNumber)
                {
                    Console.WriteLine("Invalid input or out of global range! (attempt not counted).");
                    continue;
                }

                attempt++;

                if (guess < _magicNumber)
                {
                    Console.WriteLine("Our number is BIGGER");
                    if (guess >= currentMin) currentMin = guess + 1;
                }
                else if (guess > _magicNumber)
                {
                    Console.WriteLine("Our number is LESS");
                    if (guess <= currentMax) currentMax = guess - 1;
                }
                else
                {
                    isWin = true;
                }
            }

            // Финал
            Console.WriteLine("\n=============================");
            if (isWin)
            {
                Console.WriteLine($"Right! You Win in {attempt} attempts!");
            }
            else
            {
                Console.WriteLine($"Lose! The secret number was: {_magicNumber}");
            }
            Console.WriteLine("=============================");
        }
    }
}