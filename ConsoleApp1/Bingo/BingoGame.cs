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
                var number = _bag.Draw();
                if (number == null)
                {
                    Console.WriteLine("Bag is empty!");
                    break;
                }

                _card.Mark(number.Value);
                Console.WriteLine($"Drawn numbers: {string.Join(", ", _bag.Drawn)}");
                Redraw();

                if (_card.CheckBingo())
                {
                    Console.WriteLine("BINGO! You're victorious!");
                    break;
                }

                Console.WriteLine("Press Enter to draw next number or 'b' + Enter to check for BINGO.");
                var input = Console.ReadLine();
                if (input?.Trim().ToLower() == "b")
                {
                    Console.WriteLine(_card.CheckBingo() ? "BINGO confirmed!" : "No BINGO.");
                }
            }

        }
        private void Redraw(string status = "")
        {
            Console.Clear();
            Console.WriteLine(_card.Render());
            Console.WriteLine($"Выпало: {string.Join(", ", _bag.Drawn)}");
            if (!string.IsNullOrEmpty(status)) Console.WriteLine(status);
        }
    }
}