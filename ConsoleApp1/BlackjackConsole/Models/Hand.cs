namespace BlackjackConsole.Models;

public class Hand // набор карт и подсчёт очков по ним
{
    public List<Card> Cards { get; set; } = new(); // список карт, на руках

    public void AddCard(Card card)
    {
        Cards.Add(card); // берем карту из колоды
    }
        
    public int GetScore() // метод для подсчета очков
    {
        var score = Cards.Sum(c => c.Value); // сумма всех карт (туз за 11)
        var acesCount = Cards.Count(c => c.Rank == Rank.Ace); // считаем, сколько тузов есть в руке
        while (score > 21 && acesCount > 0) // если перебор и есть тузы, пересчитываем
        {
            score -= 10; // туз считаем как 1 вместо 11
            acesCount--;
        }

        return score;
    }
    public bool IsBust => GetScore() > 21;
    // перебор - true, если очков больше 21
    public override string ToString()
    // как рука будет выглядеть при печати на экран
    {
        return string.Join(" ", Cards);
        // все карты подряд через пробел
    }
}
