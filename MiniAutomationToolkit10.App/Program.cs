using System.ComponentModel.DataAnnotations;
using MiniAutomationToolkit.Core.Validation;

Console.WriteLine("=== Тестирование защитного валидатора Guard ===");

// Сценарий 1: Положительное число (успешный проход)
try
{
    Console.WriteLine("\nПроверка значения: 5");
    Guard.EnsurePositive(5, "timeout");
    Console.WriteLine("Успешно: Число положительное, проверка пройдена.");
}
catch (MiniAutomationToolkit.Core.Validation.ValidationException ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
}

// Сценарий 2: Отрицательное число (ошибка)
try
{
    Console.WriteLine("\nПроверка значения: -5");
    Guard.EnsurePositive(-5, "retryCount");
    Console.WriteLine("Успешно: Проверка пройдена."); // Эта строка не должна выполниться
}
catch (MiniAutomationToolkit.Core.Validation.ValidationException ex)
{
    Console.WriteLine($"Перехвачено исключение:\n{ex.Message}");
}

// Сценарий 3: Ноль (ошибка)
try
{
    Console.WriteLine("\nПроверка значения: 0");
    Guard.EnsurePositive(0, "pageSize");
    Console.WriteLine("Успешно: Проверка пройдена."); // Эта строка не должна выполниться
}
catch (MiniAutomationToolkit.Core.Validation.ValidationException ex)
{
    Console.WriteLine($"Перехвачено исключение:\n{ex.Message}");
}