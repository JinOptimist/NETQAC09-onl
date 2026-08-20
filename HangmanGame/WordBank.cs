using System;
using System.Collections.Generic;
using System.Text;

namespace HangmanGame
{
    public class WordBank
    {
        private readonly string[] _words = { "пипидастр", "шуфлядка", "катавасия", "драндулет", "выхухоль", "даздраперма" };
        private readonly Random _random = new();

        public string GetRandomWord()
        {
            int index = _random.Next(_words.Length);
            return _words[index];
        }
    }
}
