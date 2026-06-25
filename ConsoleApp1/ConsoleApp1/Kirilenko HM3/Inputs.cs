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
