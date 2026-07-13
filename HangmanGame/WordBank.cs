using System;
using System.Collections.Generic;
using System.Text;

namespace HangmanGame
{
    public class WordBank
    {
        private readonly string[] _words = { "программист", "ноутбук", "клавиатура", "монитор", "процессор", "интернет" };
        private readonly Random _random = new();

        public string GetRandomWord()
        {
            int index = _random.Next(_words.Length);
            return _words[index];
        }
    }
}
