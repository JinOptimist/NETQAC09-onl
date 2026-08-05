using System;
using MiniAutomationToolkit.Core.Models;
using MiniAutomationToolkit.Core.Services;

Console.WriteLine("MiniAutomationToolkit started.");

Console.WriteLine("=== Расчёт скидки заказа ===");

// 1. Ввод типа клиента
Console.Write("Введите тип клиента (Regular, Premium, Vip): ");
string? clientInput = Console.ReadLine();

if (!Enum.TryParse<ClientType>(clientInput, true, out ClientType clientType))
{
    Console.WriteLine("Ошибка: Неверный тип клиента. Допустимы только Regular, Premium, Vip.");
    return;
}

// 2. Ввод суммы заказа
Console.Write("Введите сумму заказа: ");
string? amountInput = Console.ReadLine();

if (!decimal.TryParse(amountInput, out decimal orderAmount))
{
    Console.WriteLine("Ошибка: Сумма заказа должна быть числом.");
    return;
}

// 3. Расчёт скидки и вывод результата
try
{
    decimal discount = DiscountCalculator.CalculateDiscount(orderAmount, clientType);

    // Вывод результата в строгом соответствии с шаблоном задания
    Console.WriteLine($"Client: {clientType}, amount: {orderAmount}, discount: {discount}");
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"Ошибка валидации: {ex.Message}");
}