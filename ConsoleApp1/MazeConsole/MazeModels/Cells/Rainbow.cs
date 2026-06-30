namespace MazeConsole.MazeModels.Cells
{
    public class Rainbow : BaseCell
    {
        // Буква, которой радуга будет обозначаться в лабиринте
        public override char MySymbol => 'R';

        // Логика взаимодействия с игроком (ДЗ Занятие 5)
        public override bool PlayerStepInMe(Player player)
        {
            // 1. Создаем рандом для определения размера бонуса
            var random = new Random();
            var bonusCoins = random.Next(5, 15); // от 5 до 14 монеток

            // 2. Начисляем монеты игроку
            player.Coin += bonusCoins;

            // 3. Выводим красивое цветное сообщение в консоль
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n🌈 Вы наступили на Радугу! Получено бонусных монет: {bonusCoins}!");
            Console.ResetColor();

            return true;
        }
    }
}