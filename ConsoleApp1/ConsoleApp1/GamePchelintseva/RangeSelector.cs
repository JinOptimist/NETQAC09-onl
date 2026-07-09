// Выбирает диапазон и считает maxAttempt; результат возвращает в HM_3.Play().
public class RangeSelector
{ 
    public GameRangeSettings SelectRange()
    {
        var maxNumber = 0;
        var minNumber = 0;
        var rangeChoice = 0;
        bool isRangeChoiceValid;

        do
        {
            Console.WriteLine("Choose the range of numbers. 1. 1 - 100, 2. 101 - 500, 3. 501 - 1000");
            var rangeChoiceText = Console.ReadLine();
            isRangeChoiceValid = int.TryParse(rangeChoiceText, out rangeChoice);

            if (!isRangeChoiceValid)
            {
                Console.WriteLine("It's not a number");
            }
            else if (rangeChoice < 1 || rangeChoice > 3)
            {
                Console.WriteLine("Wrong choice. Must be 1, 2 or 3");
            }
            else if (rangeChoice == 1)
            {
                minNumber = 1;
                maxNumber = 100;
            }
            else if (rangeChoice == 2)
            {
                minNumber = 101;
                maxNumber = 500;
            }
            else if (rangeChoice == 3)
            {
                minNumber = 501;
                maxNumber = 1000;
            }

        } while (!isRangeChoiceValid || rangeChoice < 1 || rangeChoice > 3);

        var numbersCount = maxNumber - minNumber + 1;
        var maxAttempt = (int)Math.Ceiling(Math.Log2(numbersCount));

        return new GameRangeSettings
        {
            minNumber = minNumber,
            maxNumber = maxNumber,
            maxAttempt = maxAttempt
        };
    }
}