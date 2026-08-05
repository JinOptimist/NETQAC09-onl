using System;
using System.Collections.Generic;
using System.Text;

namespace MiniAutomationToolkit.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool HasHttpScheme(this string? input)
        {
            // 1. Безопасная проверка на null, пустую строку или пробелы
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            // 2. Проверка схемы без учета регистра с помощью StringComparison.OrdinalIgnoreCase
            return input.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                   input.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
        }
    }
}
