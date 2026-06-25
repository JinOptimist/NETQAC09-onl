class GameCycle(GameData userInputs)
{
    public void GameLoop()
    {
        Console.Clear();
        var attempts = 0;
        var NumberOfPower = 1;
        var userRange = userInputs.maxRange;
        var magicNumber = userInputs.magicNumber;
        do
        {
            if (Math.Pow(2, NumberOfPower) <= userRange)
            {
                NumberOfPower++;
            }
            else if (Math.Pow(2, NumberOfPower) > userRange)
            {
                attempts = NumberOfPower;
            }
        } while (attempts == 0);
        var userLost = true;
        var userGuess = -1;
        if (Math.Pow(2, NumberOfPower) == userRange + 1)
        {
            attempts++;
        }
        var userMinimum = 0;
        var userMaximum = userRange;
        var isNumber = false;
        Console.WriteLine("The magic number is created. It is time, to guess!");
        do
        {
            Console.WriteLine($"Number is between {userMinimum} to {userMaximum}");
            Console.WriteLine($"Current attempts remain = {attempts}");
            var input = Console.ReadLine();
            isNumber = int.TryParse(input, out userGuess);
            if (!isNumber)
            {
                Console.WriteLine("It's not an int number. Please enter int number");
            }
            else if (userGuess > userMaximum)
            {
                Console.WriteLine($"Number out of your range. Try again");
            }
            else if (userGuess < userMinimum)
            {
                Console.WriteLine($"Number out of your range. Try again");
            }
            else if (userGuess > magicNumber)
            {
                Console.WriteLine($"Number too big. Try again");
                userMaximum = userGuess - 1;
                attempts--;
            }
            else if (userGuess < magicNumber)
            {
                Console.WriteLine($"Number too Small. Try again");
                userMinimum = userGuess + 1;
                attempts--;
            }
            else if (userGuess == magicNumber)
            {
                userLost = false;
            }
        } while (attempts > 0 && userLost);
        if (userLost == false)
        {
            Console.WriteLine($"That is correct! Winner winner chicken dinner! You had {attempts} attempts remaining when you guessed it!");
        }
        else if (attempts == 0)
        {
            Console.WriteLine($"You are out of attempts. You lost! The number was {magicNumber}");
        }
    }
}