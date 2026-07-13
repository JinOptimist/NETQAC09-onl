namespace BlackjackConsole.Models;

public class Hand
{
    public List<Card> Cards { get; set; } = new();

    public void AddCard(Card card)
    {
        Cards.Add(card);
    }

    /// <summary>
    /// Считаем очки руки. Туз сначала считается за 11,
    /// но если из-за этого перебор - "превращаем" тузы в 1 по одному,
    /// пока перебор не исчезнет (или пока тузы не закончатся)
    /// </summary>
    public int GetScore()
    {
        var score = Cards.Sum(c => c.Value);
        var acesCount = Cards.Count(c => c.Rank == Rank.Ace);

        while (score > 21 && acesCount > 0)
        {
            score -= 10; // туз считаем как 1 вместо 11
            acesCount--;
        }

        return score;
    }

    public bool IsBust => GetScore() > 21;

    public bool IsBlackjack => Cards.Count == 2 && GetScore() == 21;

    public override string ToString()
    {
        return string.Join(" ", Cards);
    }
}
