using System;
using System.Collections.Generic;
using System.Text;

namespace Bingo
{
    public class NumberBag
    {
        private readonly List<int> _available;
        private readonly Random _rnd = new Random();
        public List<int> Drawn { get; } = new List<int>();

        public NumberBag(int max = 75)
        {
            _available = Enumerable.Range(1, max).ToList();
        }

        public int? Draw()
        {
            if (_available.Count == 0) return null;
            int idx = _rnd.Next(_available.Count);
            int number = _available[idx];
            _available.RemoveAt(idx);
            Drawn.Add(number);
            return number;
        }
    }
}
