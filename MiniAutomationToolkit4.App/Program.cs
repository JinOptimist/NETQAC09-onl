using System;
using System.Collections.Generic;
using System.Linq;
using MiniAutomationToolkit.Core.Pages;

// 1. Создание списка страниц базового типа
List<BasePage> pages = new List<BasePage>
{
    new LoginPage(),
    new HomePage()
    // Сюда можно добавить еще один объект с дублирующимся Url для проверки исключения
};

// 2. Демонстрация полиморфного вызова метода Load
Console.WriteLine("=== Загрузка страниц ===");
foreach (var page in pages)
{
    page.Load();
}

Console.WriteLine("\n=== Проверка уникальности URL ===");
try
{
    // Считаем общее количество страниц и количество уникальных URL
    int totalCount = pages.Count;
    int uniqueUrlCount = pages.Select(p => p.Url).Distinct().Count();

    if (totalCount != uniqueUrlCount)
    {
        throw new InvalidOperationException("Обнаружены дублирующиеся URL страниц.");
    }

    Console.WriteLine("All page URLs are unique.");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
}