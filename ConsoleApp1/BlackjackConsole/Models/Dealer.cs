namespace BlackjackConsole.Models;

public class Dealer : Player
{
    private const int MUST_HIT_BELOW_SCORE = 17;
    // число очков, ниже которого дилер обязан брать ещё карты

    public Dealer() : base("Дилер", 0)
    {
    }

    // Пока игрок ходит, первая карта дилера скрыта от игрока
    public string ShowHandHidden()
    {
        var openCards = Hand.Cards.Skip(1);

        return $"[?] {string.Join(" ", openCards)}";//отопражаем карты дилера в консоль
    }
        
    public bool ShouldHit() //должен ли дилер сейчас взять ещё одну карту
    {
        return Hand.GetScore() < MUST_HIT_BELOW_SCORE;
    }
}
