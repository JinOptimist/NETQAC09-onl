using MiniAutomationToolkit.Core.Extensions;

Console.WriteLine("=== Тестирование метода расширения HasHttpScheme ===");

// Список тестовых значений, включая null
var testUrls = new List<string?>
{
    "https://google.com",
    "http://example.org",
    "ftp://files.example.com",
    null,
    "HTTPS://SITE.EXAMPLE.COM",
    "   ", // Дополнительный тест на строку из пробелов
    "httpnew.com" // Дополнительный тест на ложное срабатывание (без ://)
};

foreach (var url in testUrls)
{
    // Вызываем метод расширения как метод экземпляра строки
    bool result = url.HasHttpScheme();

    // Форматируем null для красивого вывода на экран
    string displayUrl = url ?? "<null>";

    Console.WriteLine($"URL: {displayUrl,-30} -> {result}");
}