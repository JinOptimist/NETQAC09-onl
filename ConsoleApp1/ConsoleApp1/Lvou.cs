class Lvou
{
    public void Do()
    {
        Console.WriteLine("The game Guess the number");

       
        var MAX_NUMBER = 0;
        var MIN_NUMBER = 0;

        var userMagicNumber = 0;
        bool isNumber;


         do
        {
            Console.WriteLine("Enter Max number");

            var MaxNumberInput  = Console.ReadLine();
            isNumber = int.TryParse(MaxNumberInput, out MAX_NUMBER);

            if (!isNumber)
            {
                Console.WriteLine("It's not a number");
            }

        } while (!isNumber);

         do
        {
            Console.WriteLine("Enter Min number");

            var MinNumberInput  = Console.ReadLine();
            isNumber = int.TryParse(MinNumberInput, out MIN_NUMBER);

            if (!isNumber)
            {




                Console.WriteLine("It's not a number");
            }

        
            else if (MIN_NUMBER > MAX_NUMBER)
                {
                    Console.WriteLine("Min number must be less then Max number");
                }
        

        } while (!isNumber || MIN_NUMBER > MAX_NUMBER);


        do
        {
            Console.WriteLine("Enter Magic number");

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
        } while (!isNumber || userMagicNumber < MIN_NUMBER || userMagicNumber > MAX_NUMBER);

        Console.Clear();
        var attempt = 0;
        int guess;
        var isWin = false;
        int MAX_ATTEMPT =  (int)Math.Ceiling(Math.Log(MAX_NUMBER - MIN_NUMBER + 1, 2));


        do
        {
            attempt++;
            //Console.WriteLine("User 2. Enter your guess. Attemmpt [" + attempt + " / " + MAX_ATTEMPT + "]");
            Console.WriteLine($"User 2. Enter your guess. Attemmpt [{attempt} / {MAX_ATTEMPT}]");
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
            Console.WriteLine("Right! Your are Win");
        }
        else
        {
            Console.WriteLine("Loooose");
        } 

    }
}
