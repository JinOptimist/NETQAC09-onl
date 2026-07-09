namespace MazeConsole.MazeModels.Cells
{
    public class Rainbow : BaseCell
    {
        public override char MySymbol => 'R';

        // Логика взаимодействия (ДЗ Занятие 5 и 6)
        public override bool PlayerStepInMe(Player player)
        {
            // 1) Условие для генерации ошибки (например, координаты сломались)
            if (X < 0 || Y < 0)
            {
                // 2) Подробное логирование информации для разработчика
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[ERROR LOG] [{DateTime.Now}] Критический сбой ячейки Rainbow!");
                Console.WriteLine($"Причина: Отрицательные координаты спавна объекта.");
                Console.WriteLine($"Текущие координаты в игре: X = {X}, Y = {Y}");
                Console.WriteLine($"Статы игрока на момент сбоя: Имя = {player.Name}, Монеты = {player.Coin}");
                Console.ResetColor();

                // Генерируем саму ошибку
                throw new ArgumentOutOfRangeException(nameof(X), "Координаты Радуги не могут быть меньше нуля!");
            }

            // Обычная логика ДЗ-5 (если всё работает нормально)
            var random = new Random();
            var bonusCoins = random.Next(5, 15);
            player.Coin += bonusCoins;

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n🌈 Вы наступили на Радугу! Получено бонусных монет: {bonusCoins}!");
            Console.ResetColor();

            return true;
        }
    }
}