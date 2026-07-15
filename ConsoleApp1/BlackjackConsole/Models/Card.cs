namespace BlackjackConsole.Models;

public class Card
{
    public Suit Suit { get; set; } // масть
    public Rank Rank { get; set; } //достоинство карты

    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }

    // сколько очков стоит эта карта в игре
    public int Value
    {
        get
        {
            switch (Rank) //проверяем достоинство карты
            {
                case Rank.Jack:
                case Rank.Queen:
                case Rank.King:
                    return 10; // валет, дама и король всегда стоят 10 очков
                case Rank.Ace:
                    return 11; // туз 11 (Hand поправит на 1, если будет перебор)
                default:
                    return (int)Rank; // другая карта стоит столько, сколько написано на ней
            }
        }
    }

    public override string ToString() //метод для отображения карты
    {
        return $"[{GetRankSymbol()}{GetSuitSymbol()}]";
    }

    private string GetRankSymbol()
    {
        switch (Rank)
        {
            case Rank.Jack:
                return "J";
            case Rank.Queen:
                return "Q";
            case Rank.King:
                return "K";
            case Rank.Ace:
                return "A";
            default:
                return ((int)Rank).ToString();
        }
    }
    private string GetSuitSymbol()
    {
        return Suit.ToString()[0].ToString().ToLower();
        // берём  первую букву названия масти ("H"),
        // делаем её строчной ("h")
    }
}
