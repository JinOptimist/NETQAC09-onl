using MiniAutomationToolkit.Core.Models;
using MiniAutomationToolkit.Core.Repositories;

string dataFolder = Path.Combine(AppContext.BaseDirectory, "data");
if (!Directory.Exists(dataFolder))
{
    dataFolder = "data";
}

string csvPath = Path.Combine(dataFolder, "products.csv");

try
{
    Console.WriteLine("=== Загрузка каталога товаров ===");
    List<Product> products = ProductRepository.LoadFromCsv(csvPath);
    Console.WriteLine($"Успешно загружено товаров: {products.Count}\n");

    // Тест 1: Категория Food, бюджет 10 (Ожидаются: Milk, Bread, Cheese)
    Console.WriteLine("=== Тест 1: Категория Food, бюджет 10 ===");
    List<string> foodUnder10 = ProductRepository.GetAffordableProducts(products, ProductCategory.Food, 10m);
    PrintResults(foodUnder10);

    // Тест 2: Категория Food, бюджет 1 (Ожидается: No products found, так как Milk равен 1, а нужно строго меньше)
    Console.WriteLine("=== Тест 2: Категория Food, бюджет 1 ===");
    List<string> foodUnder1 = ProductRepository.GetAffordableProducts(products, ProductCategory.Food, 1m);
    PrintResults(foodUnder1);
}
catch (Exception ex)
{
    Console.WriteLine($"Произошла ошибка: {ex.Message}");
}

static void PrintResults(List<string> productNames)
{
    if (productNames.Count == 0)
    {
        Console.WriteLine("No products found.");
    }
    else
    {
        Console.WriteLine("Доступные товары:");
        foreach (var name in productNames)
        {
            Console.WriteLine($"- {name}");
        }
    }
    Console.WriteLine();
}