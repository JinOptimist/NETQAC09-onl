using System;
using System.Collections.Generic;
using System.Text;
using MiniAutomationToolkit.Core.Models;

namespace MiniAutomationToolkit.Core.Repositories
{
    public static class ProductRepository
    {
        public static List<Product> LoadFromCsv(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл каталога не найден: {filePath}");
            }

            var products = new List<Product>();
            string[] lines = File.ReadAllLines(filePath);

            // Начинаем со 2-й строки (индекс 1), так как индекс 0 — это заголовок
            for (int i = 1; i < lines.Length; i++)
            {
                int lineNumber = i + 1; // Физический номер строки (1-based)
                string line = lines[i];

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] parts = line.Split(';');

                if (parts.Length != 3 ||
                    string.IsNullOrWhiteSpace(parts[0]) ||
                    string.IsNullOrWhiteSpace(parts[1]) ||
                    string.IsNullOrWhiteSpace(parts[2]))
                {
                    throw new InvalidDataException($"Ошибка в строке {lineNumber}: некорректная структура данных.");
                }

                string name = parts[0].Trim();

                if (!decimal.TryParse(parts[1].Trim(), out decimal price) || price < 0)
                {
                    throw new InvalidDataException($"Ошибка в строке {lineNumber}: недопустимое значение цены.");
                }

                if (!Enum.TryParse<ProductCategory>(parts[2].Trim(), true, out ProductCategory category))
                {
                    throw new InvalidDataException($"Ошибка в строке {lineNumber}: неизвестная категория товара.");
                }

                products.Add(new Product(name, price, category));
            }

            return products;
        }

        public static List<string> GetAffordableProducts(IEnumerable<Product> products, ProductCategory category, decimal maxPrice)
        {
            return products
                .Where(p => p.Category == category && p.Price < maxPrice)
                .OrderBy(p => p.Price)
                .ThenBy(p => p.Name)
                .Select(p => p.Name)
                .ToList();
        }
    }
}
