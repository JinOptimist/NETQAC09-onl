namespace BlackjackConsole.Models;

public class Card
{
    public Suit Suit { get; set; }
    public Rank Rank { get; set; }

    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }

    // Очки карты: 2-10 по номиналу, J/Q/K = 10, Туз = 11.
    // Если из-за туза случится перебор, Hand сам "превратит" его в 1 (см. Hand.GetScore)
    public int Value
    {
        get
        {
            switch (Rank)
            {
                case Rank.Jack:
                case Rank.Queen:
                case Rank.King:
                    return 10;
                case Rank.Ace:
                    return 11;
                default:
                    return (int)Rank;
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
    // переводим в стринг, берем первую букву-char опять пепеводим в стринг, делаем нижний регистр
    private string GetSuitSymbol()
    {
        return Suit.ToString()[0].ToString().ToLower();
    }
}
