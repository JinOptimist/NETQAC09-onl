using System;
using System.Collections.Generic;
using System.Text;

namespace HangmanGame
{
    public class GameRenderer
    {
        public void Do()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            WordBank wordRepository = new WordBank();

            // Создаем состояние игры, передав туда случайное слово
            HangmanGame game = new HangmanGame(wordRepository.GetRandomWord());

            Console.WriteLine("=== ДОБРО ПОЖАЛОВАТЬ В ИГРУ ВИСЕЛИЦА! ===");

            // Играем, пока не победа и не поражение
            while (!game.IsGameOver && !game.IsWordGuessed)
            {
                Console.WriteLine();
                // Выводим слово с маской (буквы и нижние подчеркивания)
                Console.WriteLine(game.GetDisplayWord());

                Console.WriteLine($"Осталось попыток: {game.MaxLives - game.CurrentErrors}");
                DrawGallows(game.CurrentErrors);

                Console.Write("Введите русскую букву: ");
                string input = Console.ReadLine()?.Trim().ToLower() ?? "";

                if (string.IsNullOrEmpty(input) || input.Length != 1 || !char.IsLetter(input[0]))
                {
                    ShowMessage("⚠️ Пожалуйста, введите ровно ОДНУ букву.", ConsoleColor.Yellow);
                    continue;
                }

                char inputLetter = input[0];

                if (game.IsLetterAlreadyGuessed(inputLetter))
                {
                    ShowMessage("⚠️ Вы уже вводили эту букву.", ConsoleColor.Yellow);
                    continue;
                }

                // Передаем букву в состояние игры и проверяем результат
                if (game.GuessLetter(inputLetter))
                {
                    ShowMessage("✅ Правильно!", ConsoleColor.Green);
                }
                else
                {
                    ShowMessage("❌ Такой буквы нет!", ConsoleColor.Red);
                }
            }

            // Финал игры
            Console.Clear();
            DrawGallows(game.CurrentErrors);

            if (game.IsWordGuessed)
            {
                ShowMessage($"\n🎉 Поздравляем! Вы выиграли! Было загадано слово: {game.GetTargetWord()}", ConsoleColor.Green);
            }
            else
            {
                ShowMessage($"\n💀 Вы проиграли! Вас повесили. Загаданное слово было: {game.GetTargetWord()}", ConsoleColor.Red);
            }
        }

        // Вспомогательный метод для цветного вывода текста
        static void ShowMessage(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        static void DrawGallows(int errors)
        {
            string[] stages = {
                "  +---+\n  |   |\n      |\n      |\n      |\n      |\n=========",
                "  +---+\n  |   |\n  O   |\n      |\n      |\n      |\n=========",
                "  +---+\n  |   |\n  O   |\n  |   |\n      |\n      |\n=========",
                "  +---+\n  |   |\n  O   |\n /|   |\n      |\n      |\n=========",
                "  +---+\n  |   |\n  O   |\n /|\\  |\n      |\n      |\n=========",
                "  +---+\n  |   |\n  O   |\n /|\\  |\n /    |\n      |\n=========",
                "  +---+\n  |   |\n  O   |\n /|\\  |\n / \\  |\n      |\n========="
            };
            Console.WriteLine(stages[errors]);
        }
    }
}
