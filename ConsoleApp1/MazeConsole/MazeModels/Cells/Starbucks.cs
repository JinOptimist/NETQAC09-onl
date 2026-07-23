namespace MazeConsole.MazeModels.Cells;

public class Starbucks : BaseCell
{
    public override char MySymbol => 'O';

    public override bool PlayerStepInMe(IPlayer player)
    {
        int price = 5;  // цена кофе

        // (2) лог: что за клетка, где, состояние игрока
        Console.WriteLine($"[Starbucks] Позиция {GetMyPosition()}, монет у игрока: {player.Coin}, цена: {price}");

        // (1) условие -> ошибка
        if (player.Coin < price)
        {
            Console.WriteLine($"[Starbucks][ERROR] Недостаточно монет на {GetMyPosition()}. Нужно {price}, есть {player.Coin}");
            throw new InvalidOperationException(
                $"Недостаточно монет для Starbucks на {GetMyPosition()}. Нужно {price}, есть {player.Coin}");
        }

        player.Coin -= price;
        Console.WriteLine($"[Starbucks] Покупка успешна. Осталось монет: {player.Coin}");
        return true;
    }
}