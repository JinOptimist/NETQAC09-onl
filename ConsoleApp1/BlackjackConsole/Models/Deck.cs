namespace BlackjackConsole.Models;

public class Deck
{
    private List<Card> _cards = new();
    private readonly Random _random;

    // seed позволяет получить одинаковую (воспроизводимую) раздачу при одном и том же числе.
    // Если seed не передан - колода каждый раз будет перемешиваться по-разному
    public Deck(int? seed = null)
    {
        _random = seed.HasValue ? new Random(seed.Value) : new Random();
        BuildFullDeck();
    }

    public int CardsLeft => _cards.Count;

    private void BuildFullDeck()
    {
        foreach (Suit suit in Enum.GetValues<Suit>())
        {
            foreach (Rank rank in Enum.GetValues<Rank>())
            {
                _cards.Add(new Card(suit, rank));
            }
        }
    }

    public void Shuffle()
    {
        _cards = _cards
            .OrderBy(card => _random.Next())
            .ToList();
    }

    public Card Draw()
    {
        if (_cards.Any() == false)
        {
            BuildFullDeck();
            Shuffle();
        }

        var card = _cards.First();
        _cards.Remove(card);

        return card;
    }
}
