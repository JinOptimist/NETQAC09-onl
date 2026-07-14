namespace BykiKorovy;

public class BullsCowsGame
{
    private const int MaxAttempts = 10;
    private const int NumberLength = 4;

    private readonly SecretNumber _secretNumber = new();
    private readonly GuessEvaluator _evaluator = new();

    public void Play()
    {
        string secret = _secretNumber.Generate();

        Console.Clear();
        Console.WriteLine("The game Bulls and Cows");
        Console.WriteLine($"Guess the {NumberLength}-digit number.");
        Console.WriteLine($"You have {MaxAttempts} attempts.");

        for (int attempt = 1; attempt <= MaxAttempts; attempt++)
        {
            Console.Write($"\nAttempt {attempt}. ");
            string guess = GetUserInput();

            var result = _evaluator.Evaluate(secret, guess);

            Console.WriteLine($"Bulls: {result.Bulls}");
            Console.WriteLine($"Cows: {result.Cows}");

            if (result.Bulls == NumberLength)
            {
                Console.WriteLine("Congratulations! You guessed the number!");
                return;
            }
        }

        Console.WriteLine($"You lost. Secret number was {secret}");
    }

    private string GetUserInput()
    {
        int userInputInt;
        bool isNumber;
        bool isValidInput;
        do
        {
            isValidInput = false;
            Console.Write($"Your number: ");
            var userInput = Console.ReadLine();
            isNumber = int.TryParse(userInput, out userInputInt);

            if (userInputInt.ToString().Length != NumberLength || !isNumber)
            {
                Console.WriteLine("Invalid input. Please enter a 4-digit number.");
                isValidInput = true;
            }
        } while (isValidInput || !isNumber);

        return userInputInt.ToString();
    }
}