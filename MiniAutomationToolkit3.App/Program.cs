using System;
using MiniAutomationToolkit.Core.Models;

Console.WriteLine("=== Успешное создание и свойства Record ===");

// 1. Успешное создание пользователя
var user1 = new UserDto("Alex Smith", "alex@example.com");
Console.WriteLine($"Created user: Name = {user1.Name}, Email = {user1.Email}");

// 2. Проверка равенства по значению (структурное равенство записей)
var user2 = new UserDto("Alex Smith", "alex@example.com");
Console.WriteLine($"Are user1 and user2 equal? {user1 == user2}"); // Должно быть True

// 3. Невозможность изменения свойств (раскомментирование строки вызовет ошибку компиляции)
// user1.Name = "John"; // Ошибка CS8852: Свойство init-only или immutable можно назначить только при инициализации


Console.WriteLine("\n=== Демонстрация валидации (Ошибочные сценарии) ===");

// Сценарий 1: Пустое имя и корректный email
TryCreateUser("", "alex@example.com");

// Сценарий 2: Корректное имя и пустой email
TryCreateUser("Alex Smith", "   ");

// Сценарий 3: Корректное имя и email без символа @
TryCreateUser("Alex Smith", "alex_example.com");

// Сценарий 4: Корректное имя и email с пробелом
TryCreateUser("Alex Smith", "alex @example.com");


// Вспомогательный метод для безопасного тестирования исключений
static void TryCreateUser(string name, string email)
{
    try
    {
        var invalidUser = new UserDto(name, email);
        Console.WriteLine($"Success: Created user {invalidUser.Name}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Caught expected exception: {ex.Message}");
    }
}