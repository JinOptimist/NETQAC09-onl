using System;
using System.Collections.Generic;
using System.IO;

namespace MiniAutomationToolkit.Core.Configuration
{
    public class AppConfig
    {
        private readonly Dictionary<string, string> _settings = new(StringComparer.OrdinalIgnoreCase);

        public AppConfig(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл конфигурации не найден: {filePath}");
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (var rawLine in lines)
            {
                string line = rawLine.Trim();

                // Игнорируем пустые строки и комментарии
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                {
                    continue;
                }

                // Разделяем строго по первому символу '=' (максимум на 2 части)
                string[] parts = line.Split(new[] { '=' }, 2);

                // Ошибка: в строке нет знака '=' или ключ пустой
                if (parts.Length < 2 || string.IsNullOrWhiteSpace(parts[0]))
                {
                    throw new InvalidDataException($"Некорректный формат строки конфигурации: '{rawLine}'");
                }

                string key = parts[0].Trim();
                string value = parts[1].Trim();

                // Повторяющийся ключ — ошибка конфигурации
                if (_settings.ContainsKey(key))
                {
                    throw new InvalidDataException($"Обнаружен дублирующийся ключ конфигурации: '{key}'");
                }

                _settings.Add(key, value);
            }
        }

        public T GetSetting<T>(string key)
        {
            if (!_settings.TryGetValue(key, out string? rawValue))
            {
                throw new KeyNotFoundException($"Ключ конфигурации '{key}' не найден.");
            }

            try
            {
                // Преобразование строки к целевому обобщенному типу T
                return (T)Convert.ChangeType(rawValue, typeof(T));
            }
            catch (Exception ex) when (ex is FormatException || ex is InvalidCastException || ex is OverflowException)
            {
                throw new InvalidDataException($"Ошибка приведения ключа '{key}' к типу {typeof(T).Name}.", ex);
            }
        }
    }
}