using MiniAutomationToolkit.Core.Services;

// Настройка путей к файлам в папке data
string dataFolder = Path.Combine(AppContext.BaseDirectory, "data");

// Локальная заглушка на случай запуска из папки проекта в IDE
if (!Directory.Exists(dataFolder))
{
    dataFolder = "data";
}

// Гарантируем, что папка data существует
Directory.CreateDirectory(dataFolder);

string inputPath = Path.Combine(dataFolder, "input.txt");
string missingPath = Path.Combine(dataFolder, "missing.txt");
string logPath = Path.Combine(dataFolder, "errors.log");

var logger = new ErrorLogger();

// --- Сценарий 1: Чтение существующего файла ---
Console.WriteLine("=== Сценарий 1: Чтение существующего файла ===");
if (!File.Exists(inputPath))
{
    File.WriteAllText(inputPath, "Привет от MiniAutomationToolkit!\nЭтот файл успешно прочитан.");
}

string? successContent = logger.TryReadFile(inputPath, logPath);
if (successContent != null)
{
    Console.WriteLine("Содержимое файла:");
    Console.WriteLine(successContent);
}
Console.WriteLine();

// --- Сценарий 2: Попытка чтения отсутствующего файла ---
Console.WriteLine("=== Сценарий 2: Чтение отсутствующего файла ===");
// Гарантируем, что файла точно нет
if (File.Exists(missingPath))
{
    File.Delete(missingPath);
}

string? failContent = logger.TryReadFile(missingPath, logPath);
if (failContent == null)
{
    Console.WriteLine("Файл не найден. Ошибка записана в лог.");
}
Console.WriteLine();

// --- Вывод содержимого созданного лог-файла ---
Console.WriteLine("=== Содержимое файла errors.log ===");
if (File.Exists(logPath))
{
    string logContent = File.ReadAllText(logPath);
    Console.WriteLine(logContent);
}
else
{
    Console.WriteLine("Файл лога не был создан.");
}