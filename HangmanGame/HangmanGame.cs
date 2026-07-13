using System;
using System.Collections.Generic;
using System.Text;

namespace HangmanGame
{
    public class HangmanGame
    {
        private readonly string _targetWord;
        private readonly HashSet<char> _guessedLetters;

        public int MaxLives { get; }
        public int CurrentErrors { get; private set; }

        // Конструктор принимает загаданное слово и задает количество жизней
        public HangmanGame(string targetWord, int maxLives = 6)
        {
            _targetWord = targetWord.ToLower();
            _guessedLetters = new HashSet<char>();
            MaxLives = maxLives;
            CurrentErrors = 0;
        }

        // Проверка: исчерпаны ли попытки
        public bool IsGameOver => CurrentErrors >= MaxLives;

        // Проверка: угадано ли всё слово целиком
        public bool IsWordGuessed => _targetWord.All(letter => _guessedLetters.Contains(letter));

        // Свойство для получения текущей маски слова (например, "п _ о _ _ _ _")
        public string GetDisplayWord()
        {
            List<string> displayedCharacters = new();
            foreach (char letter in _targetWord)
            {
                displayedCharacters.Add(_guessedLetters.Contains(letter) ? letter.ToString() : "_");
            }
            return string.Join(" ", displayedCharacters);
        }

        // Проверка: вводилась ли буква ранее
        public bool IsLetterAlreadyGuessed(char letter) => _guessedLetters.Contains(letter);

        // Основной метод обработки хода. Возвращает true, если буква угадана.
        public bool GuessLetter(char letter)
        {
            letter = char.ToLower(letter);
            _guessedLetters.Add(letter);

            if (_targetWord.Contains(letter))
            {
                return true;
            }

            CurrentErrors++;
            return false;
        }

        // Метод, чтобы узнать загаданное слово в конце игры
        public string GetTargetWord() => _targetWord;
    }
}
