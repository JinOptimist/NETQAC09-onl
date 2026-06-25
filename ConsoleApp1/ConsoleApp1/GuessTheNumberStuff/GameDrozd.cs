public class GameDrozd
{
    private int minNumber;
    private int maxNumber;
    private int maxAttempts;
    private int targetNumber;
    private int currentAttempt;
    private bool isWin;

   
    public void Start()
    {
        Console.WriteLine("The game Guess the number");

        SetupRange();
        CalculateMaxAttempts();
        SelectOpponentAndSetTarget();
        PlayGameLoop();
        ShowFinalResult();
    }

    private int ReadInt(string prompt) //Метод для валидации на ввод именно целого числа
    {
        int result;
        while (true)
        {
            Console.WriteLine(prompt);
            if (int.TryParse(Console.ReadLine(), out result))
            {
                return result;
            }
            Console.WriteLine("It's not a number");
        }
    }
    private void SetupRange() //Метод задающий интервал для угадывания
    {
        minNumber = ReadInt("Enter guessing interval start value:");

        while (true)
        {
            maxNumber = ReadInt("Enter guessing interval end value:");
            if (maxNumber > minNumber) break;
            Console.WriteLine("End value should be more than start value");
        }
    }
    private void CalculateMaxAttempts() //Подсчет максимального количества попыток
    {
        decimal middleIntervalValue = maxNumber - minNumber + 1;
        maxAttempts = 0;
        do
        {
            middleIntervalValue = (int)Math.Ceiling(middleIntervalValue / 2); //BUG подсчета, исправить с деления на логарифмический метод
            maxAttempts++;
        } while (middleIntervalValue > 1);
    }
    private void SelectOpponentAndSetTarget()  //Выбираем кто оппонент и загадываем число
    {
        int opponent;
        while (true)
        {
            Console.WriteLine("Select who chooses magic number");
            opponent = ReadInt("Enter '1' to select human opponent, enter '2' to select computer opponent");
            if (opponent == 1 || opponent == 2) break;
            Console.WriteLine("Please make valid selection: 1 or 2");
        }

        if (opponent == 1)
        {
            while (true)
            {
                targetNumber = ReadInt($"User 1. Enter Magic number between [{minNumber} and {maxNumber}]");
                if (targetNumber >= minNumber && targetNumber <= maxNumber) break;
                Console.WriteLine($"Number must be between {minNumber} and {maxNumber}");
            }
        }
        else
        {
            targetNumber = Random.Shared.Next(minNumber, maxNumber + 1);
        }

        Console.Clear();
    }
    private void PlayGameLoop() //Метод основной игровой логики (догадка в интервале, подсказка текущего интервала, какая попытка)
    {
        currentAttempt = 0;
        isWin = false;

        while (!isWin && currentAttempt < maxAttempts)
        {
            Console.WriteLine($"Current guessing interval between [{minNumber} and {maxNumber}]");
            int guess = ReadInt($"User 2. Enter your guess. Attempt [{currentAttempt} / {maxAttempts}]");

            if (guess > maxNumber)
            {
                Console.WriteLine("Entered value is bigger then end of interval");
            }
            else if (guess < minNumber)
            {
                Console.WriteLine("Entered value is smaller then start of interval");
            }
            else if (guess < targetNumber)
            {
                Console.WriteLine("Our number is bigger");
                minNumber = guess + 1;
                currentAttempt++;
            }
            else if (guess > targetNumber)
            {
                Console.WriteLine("Our number is less");
                maxNumber = guess - 1;
                currentAttempt++;
            }
            else if (guess == targetNumber)
            {
                isWin = true;
                currentAttempt++;
            }
        }
    }
    private void ShowFinalResult() //Вывод результата
    {
        if (isWin)
        {
            Console.WriteLine("Right! You Win!");
        }
        else
        {
            Console.WriteLine($"Loose! The number was {targetNumber}");
        }
    }
}