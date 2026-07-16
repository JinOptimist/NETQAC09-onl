namespace BullAndCow;

public class BullAndCowGame
{
    public const int NUMBER_LENGTH = 4;
    private int _secretNumber; // 1243

    public void Play()
    {
        SetTheSecrecNumber();

        var bullAndCow = new BullAndCow();
        while (!IsUserWinner(bullAndCow))
        {
            var number = GetUserNumber(); // 2345

            bullAndCow = CalcBullAndCow(number); // B:0 C:2

            ShowToUserBullAndCow(bullAndCow);
        }
    }

    private bool IsUserWinner(BullAndCow bullAndCowCount)
    {
        return NUMBER_LENGTH == bullAndCowCount.Bull;
    }

    private void ShowToUserBullAndCow(BullAndCow bullAndCow)
    {
        Console.WriteLine($"Cow: {bullAndCow.Cow} Bull: {bullAndCow.Bull}");
    }

    private BullAndCow CalcBullAndCow(int userNumber)
    {
        var bull = 0;
        var cow = 0;
        var userNumberText = userNumber.ToString();
        var secretNumberText = _secretNumber.ToString();
        for (int i = 0; i < userNumberText.Length; i++)
        {
            var userSymbol = userNumberText[i];
            if (userSymbol == secretNumberText[i])
            {
                bull++;
                continue;
            }

            if (secretNumberText.Contains(userSymbol))
            {
                cow++;
            }
        }

        return new BullAndCow
        {
            Bull = bull,
            Cow = cow
        };
    }

    private int GetUserNumber()
    {
        var text = Console.ReadLine();
        return int.Parse(text);
    }

    private void SetTheSecrecNumber()
    {
        _secretNumber = 1243;
    }
}
