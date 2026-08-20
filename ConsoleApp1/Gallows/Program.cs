using System;
using System.Collections.Generic;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            // Список слов для игры
            string[] words = { "программирование", "виселица", "разработка", "компьютер", "алгоритм" };
            Random random = new Random();
            string secretWord = words[random.Next(words.Length)];

            // Скрытое слово (массив символов для отображения)
            char[] displayWord = new char[secretWord.Length];
            for (int i = 0; i < displayWord.Length; i++)
            {
                displayWord[i] = '_';
            }

            int attemptsLeft = 6;
            List<char> guessedLetters = new List<char>();

            Console.WriteLine("Добро пожаловать в игру 'Виселица'!");

            while (attemptsLeft > 0 && new string(displayWord) != secretWord)
            {
                Console.WriteLine($"\nСлово: {string.Join(" ", displayWord)}");
                Console.WriteLine($"Осталось попыток: {attemptsLeft}");
                Console.WriteLine($"Использованные буквы: {string.Join(", ", guessedLetters)}");

                Console.Write("Введите букву: ");
                string input = Console.ReadLine().ToLower();

                if (input.Length != 1 || !char.IsLetter(input[0]))
                {
                    Console.WriteLine("Пожалуйста, введите ровно одну букву.");
                    continue;
                }

                char letter = input[0];

                if (guessedLetters.Contains(letter))
                {
                    Console.WriteLine("Вы уже вводили эту букву.");
                    continue;
                }

                guessedLetters.Add(letter);

                if (secretWord.Contains(letter))
                {
                    for (int i = 0; i < secretWord.Length; i++)
                    {
                        if (secretWord[i] == letter)
                        {
                            displayWord[i] = letter;
                        }
                    }
                }
                else
                {
                    attemptsLeft--;
                    Console.WriteLine("Такой буквы нет в слове.");
                }
            }

            if (new string(displayWord) == secretWord)
            {
                Console.WriteLine($"\nПоздравляем! Вы угадали слово: {secretWord}");
            }
            else
            {
                Console.WriteLine($"\nВы проиграли. Загаданное слово было: {secretWord}");
            }
        }
    }
}