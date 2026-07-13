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
            _card.Print();

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
                _card.Print();

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
    }
}