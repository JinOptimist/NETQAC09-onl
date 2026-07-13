using System;
using System.Collections.Generic;
using System.Text;

namespace Bingo
{
    public class BingoGame
    {
        private readonly BingoCard _card = new BingoCard();
        private readonly NumberBag _bag = new NumberBag();

        public void Run()
        {
            Redraw();

            while (true)
            {
                Console.WriteLine("Press Enter to draw next number or 'b' + Enter to check for BINGO ('q' to quit).");
                var input = Console.ReadLine()?.Trim().ToLower();

                if (input == "q") break;

                if (input == "b")
                {
                    Console.WriteLine(_card.CheckBingo() ? "BINGO confirmed!" : "No BINGO.");
                    continue;
                }

                var number = _bag.Draw();
                if (number == null)
                {
                    Console.WriteLine("Bag is empty!");
                    break;
                }

                _card.Mark(number.Value);
                Console.WriteLine($"Drawn numbers: {string.Join(", ", _bag.Drawn)}");
                Redraw();

                //Автопроверка победы, уберите комментарии если не хочется проверять BINGO вручную
                //if (_card.CheckBingo())
                //{
                //    Console.WriteLine("BINGO! You're victorious!");
                //    break;
                //}
            }

        }
        private void Redraw(string status = "")
        {
            Console.Clear();
            Console.WriteLine(_card.Render());
            Console.WriteLine($"Numbers drawn: {string.Join(", ", _bag.Drawn)}");
            if (!string.IsNullOrEmpty(status)) Console.WriteLine(status);
        }
    }
}