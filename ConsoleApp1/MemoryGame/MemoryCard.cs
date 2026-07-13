namespace MemoryGame;

public class MemoryCard
{
    public char Symbol { get; set; }
    public CardState State { get; set; }

    public MemoryCard(char symbol)
    {
        Symbol = symbol;
        State = CardState.Hidden;
    }

    public bool IsMatched => State == CardState.Matched;
    public bool IsRevealed => State == CardState.Revealed;
    public bool IsHidden => State == CardState.Hidden;
}
