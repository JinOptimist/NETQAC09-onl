//class Kirilenko
//{
//    public void Do()
//    {
//        Console.WriteLine("The game Guess the number");

//        var MIN_NUMBER = 0;
//        Random random = new Random();
//        var userRange = 0;
//        bool isNumber;
//        Console.WriteLine("Write range for number from 0 to 2,147,483,647"); //Turns out you dont actualy need to check for int
//        do
//        {
//            var range = Console.ReadLine();
//            isNumber = int.TryParse(range, out userRange);

//            if (!isNumber)
//            {
//                Console.WriteLine("It's not an int number");
//            }
//            else if (userRange < MIN_NUMBER)
//            {
//                Console.WriteLine($"Too small number. Must be more then {MIN_NUMBER}");
//            }
//        } while (!isNumber
//    || userRange < MIN_NUMBER);
//        var user_type = 0;
//        Console.WriteLine("Write 1 to play with computer, write 2 to play with another player");
//        do
//        {
//            var input = Console.ReadLine();
//            isNumber = int.TryParse(input, out user_type);

//            if (!isNumber)
//            {
//                Console.WriteLine("It's not a number");
//            }
//            else if (user_type == 1 || user_type == 2)
//            {
//                Console.WriteLine("Thank you for your answer");
//            }
//            else if (user_type != 1 & user_type != 2)
//            {
//                Console.WriteLine($"Number is incorrect");
//            }
//        } while (!isNumber
//    || user_type != 1
//    & user_type != 2);
//        int magicNumber = random.Next(0, userRange);
//        if (user_type == 2)
//        {
//            Console.WriteLine($"Write number to guess from 0 to {userRange}"); //Turns out you dont actualy need to check for int
//            do
//            {
//                var range = Console.ReadLine();
//                isNumber = int.TryParse(range, out magicNumber);

//                if (!isNumber)
//                {
//                    Console.WriteLine("It's not a number");
//                }
//                else if (magicNumber < MIN_NUMBER)
//                {
//                    Console.WriteLine($"Too small number. Must be more or equal to {MIN_NUMBER}");
//                }
//                else if (magicNumber > userRange)
//                {
//                    Console.WriteLine($"Too big number. Must be less or equal to {userRange}");
//                }
//            } while (!isNumber
//    || magicNumber < MIN_NUMBER
//    || magicNumber > userRange);
//        }
//        Console.Clear();
//        var attempts = 0;
//        var NumberOfPower = 1;
//        do
//        {
//            if (Math.Pow(2, NumberOfPower) <= userRange)
//            {
//                NumberOfPower++;
//            }
//            else if (Math.Pow(2, NumberOfPower) > userRange)
//            {
//                attempts = NumberOfPower;
//            }
//        } while (attempts == 0);
//        var userLost = true;
//        var userGuess = -1;
//        if (Math.Pow(2, NumberOfPower) == userRange+1)
//        {
//            attempts++; 
//        }
//        var userMinimum = 0;
//        var userMaximum = userRange;
//        Console.WriteLine("The magic number is created. It is time, to guess!");
//        do
//        {
//            Console.WriteLine($"Number is between {userMinimum} to {userMaximum}");
//            Console.WriteLine($"Current attempts remain = {attempts}");
//            var input = Console.ReadLine();
//            isNumber = int.TryParse(input, out userGuess);
//            if (!isNumber)
//            {
//                Console.WriteLine("It's not an int number. Please enter int number");
//            }
//            else if (userGuess > userMaximum)
//            {
//                Console.WriteLine($"Number out of your range. Try again");
//            }
//            else if (userGuess < userMinimum)
//            {
//                Console.WriteLine($"Number out of your range. Try again");
//            }
//            else if (userGuess > magicNumber)
//            {
//                Console.WriteLine($"Number too big. Try again");
//                userMaximum = userGuess-1;
//                attempts--;
//            }
//            else if (userGuess < magicNumber)
//            {
//                Console.WriteLine($"Number too Small. Try again");
//                userMinimum = userGuess+1;
//                attempts--;
//            }
//            else if (userGuess == magicNumber)
//            {
//                userLost = false;
//            }
//        } while (attempts > 0 && userLost);
//        if (userLost == false)
//        {
//            Console.WriteLine($"That is correct! Winner winner chicken dinner! You had {attempts} attempts remaining when you guessed it!");
//        }
//        else if (attempts == 0)
//        {
//            Console.WriteLine($"You are out of attempts. You lost! The number was {magicNumber}");
//        }
//    }
//}



class Player
{
    public string name { get; set; }
    public bool realPlayer { get; }
    public Player(string nameInput, bool playerType)
    {
        name = nameInput;
        realPlayer = playerType;
    }
}
class GameData
{
    public int magicNumber {get;}
    public int maxRange {get;}
    public GameData(int magicNumberInput, int maxRangeInput)
    {
        magicNumber = magicNumberInput;
        maxRange = maxRangeInput;
    }
}

class MagicNumber
{
    public int MagicNumberGenerator(Player magicMaster,int maxRange)
    {
        if (magicMaster.realPlayer == false)
        {
            var rand = new Random();
            int randNum = rand.Next(0, maxRange);
            return (randNum);
        }
        else
        {
            Console.WriteLine($"Write number to guess from 0 to {maxRange}");  
            var isNumber = false;
            var magicNumber = 0;
            var minNumber = 0;
            do
            {
                var range = Console.ReadLine();

                isNumber = int.TryParse(range, out magicNumber);

                if (!isNumber)
                {
                    Console.WriteLine("It's not a number");
                }
                else if (magicNumber < minNumber)
                {
                    Console.WriteLine($"Too small number. Must be more or equal to {minNumber}");
                }
                else if (magicNumber > maxRange)
                {
                    Console.WriteLine($"Too big number. Must be less or equal to {maxRange}");
                }
            } while (!isNumber
    || magicNumber < minNumber
    || magicNumber > maxRange);
            return (magicNumber);
        }
    }

    }
class Inputs
{
    public Player PlayerTypeInput()
    {
        var isNumber = false;
        var pcType = 0;
        Console.WriteLine("Write 1 to play with computer, write 2 to play with another player");
        do
        {
            var input = Console.ReadLine();
            isNumber = int.TryParse(input, out pcType);
            if (!isNumber)
            {
                Console.WriteLine("It's not a number");
            }
            else if (pcType == 1 || pcType == 2)
            {
                Console.WriteLine("Thank you for your answer");
            }
            else if (pcType != 1 & pcType != 2)
            {
                Console.WriteLine($"Number is incorrect");
            }
        } while (!isNumber
    || pcType != 1
    & pcType != 2);
        if (pcType == 1)
        {
            return (new Player("magicMaster", false));
        }
        else
        {
            return (new Player("magicMaster", true));
        }

    }

        public int RangeInput()
        {
            Console.WriteLine("Write range for number from 0 to 2,147,483,647"); //Turns out you dont actualy need to check for int
            var isNumber = false;
            var maxRange = 2147483647;
            var minRange = 0;
            do
            {
                var rangeInput = Console.ReadLine();
                isNumber = int.TryParse(rangeInput, out maxRange);
                if (!isNumber)
                {
                    Console.WriteLine("It's not an int number");
                }
                else if (maxRange < minRange)
                {
                    Console.WriteLine($"Too small number. Must be more then {minRange}");
                }
            } while (!isNumber
        || maxRange < minRange);
            return (maxRange);
        }
    }

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
class GameKirilenko
    {
        public void MainKirilenkoProgram()
        {
            Console.WriteLine("The game Guess the number");
            var magicMaster = new Inputs().PlayerTypeInput();
            var maxRange = new Inputs().RangeInput();
            var magicNumber = new MagicNumber().MagicNumberGenerator(magicMaster,maxRange);
            var gameInput = new GameData(magicNumber, maxRange);
            var gameLoop = new GameCycle(gameInput);
            gameLoop.GameLoop();
    }
}
