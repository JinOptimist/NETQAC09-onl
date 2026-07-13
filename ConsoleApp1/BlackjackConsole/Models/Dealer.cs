using System.Numerics;

namespace BlackjackConsole.Models;

public class Dealer : Player
{
    private const int MustHitBelowScore = 17;

    public Dealer() : base("Дилер", 0)
    {
    }

    // Пока игрок ходит, первая карта дилера скрыта от игрока
    public string ShowHandHidden()
    {
        var openCards = Hand.Cards.Skip(1);

        return $"[?] {string.Join(" ", openCards)}";//отопражаем карты дилера в консоль
    }

    // Правило: дилер обязан брать карты, пока сумма меньше 17
    public bool ShouldHit()
    {
        return Hand.GetScore() < MustHitBelowScore;
    }
}
