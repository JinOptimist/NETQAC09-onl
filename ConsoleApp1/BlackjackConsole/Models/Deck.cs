namespace BlackjackConsole.Models;

public class Deck
{
    private List<Card> _cards = new(); //массив карт в колоде
    private readonly Random _random;

    
    public Deck(int? seed = null) //конструктор для создания колоды
    {
        _random = seed.HasValue ? new Random(seed.Value) : new Random();
        BuildFullDeck();
    }

    public int CardsLeft => _cards.Count;

    private void BuildFullDeck() //генерируем колоду
    {
        foreach (Suit suit in Enum.GetValues<Suit>())
        {
            foreach (Rank rank in Enum.GetValues<Rank>())
            {
                _cards.Add(new Card(suit, rank)); //добавляем карту в колоду масть+ранг
            }
        }
    }

    public void Shuffle() //метод для перемешивания колоды
    {
        _cards = _cards
            .OrderBy(card => _random.Next())  // сортируем карты по рандомному порядоку
            .ToList();
    }

    public Card Draw() //берем первую карту
    {
        if (_cards.Any() == false) //если карты кончились создаем новую колоду
        {
            BuildFullDeck();
            Shuffle();
        }

        var card = _cards.First();// берем первую карту из списка
        _cards.Remove(card); // убираем её из колоды, чтобы не выдать повторно

        return card;
    }
}
