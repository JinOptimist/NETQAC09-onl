class MagicNumber
{
    public int MagicNumberGenerator(Player magicMaster, int maxRange)
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