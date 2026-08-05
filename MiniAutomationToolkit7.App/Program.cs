using System.Diagnostics;
using MiniAutomationToolkit.Core.Simulations;

Console.WriteLine("=== Запуск асинхронной операции ===");

var simulator = new LongOperationSimulator();
var stopwatch = new Stopwatch();

// Запускаем секундомер
stopwatch.Start();

// Вызываем асинхронный метод через await (без использования .Result или .Wait())
string result = await simulator.LongOperationAsync();

// Останавливаем секундомер
stopwatch.Stop();

Console.WriteLine($"Результат выполнения: {result}");
Console.WriteLine($"Длительность выполнения: {stopwatch.ElapsedMilliseconds} мс");