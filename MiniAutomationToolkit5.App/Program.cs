using System;
using System.Collections.Generic;
using System.IO;
using MiniAutomationToolkit.Core.Configuration;

// Формируем относительный путь к файлу конфигурации в папке приложения
string configPath = Path.Combine(AppContext.BaseDirectory, "data", "appsettings.txt");

// На случай, если вы запускаете через IDE и папка data лежит рядом с файлом проекта,
// сделаем проверку и скорректируем путь для локальной отладки:
if (!File.Exists(configPath))
{
    configPath = Path.Combine("data", "appsettings.txt");
}

try
{
    Console.WriteLine("=== Загрузка конфигурации ===");
    AppConfig config = new AppConfig(configPath);
    Console.WriteLine("Конфигурация успешно загружена.\n");

    // 1. Успешное получение параметров в соответствующих типах
    string baseUrl = config.GetSetting<string>("baseUrl");
    int timeout = config.GetSetting<int>("timeout");
    bool headless = config.GetSetting<bool>("headless");
    int retryCount = config.GetSetting<int>("retryCount");

    Console.WriteLine("=== Прочитанные параметры ===");
    Console.WriteLine($"baseUrl (string): {baseUrl}");
    Console.WriteLine($"timeout (int): {timeout}");
    Console.WriteLine($"headless (bool): {headless}");
    Console.WriteLine($"retryCount (int): {retryCount}\n");

    // 2. Демонстрация обработки отсутствующего ключа
    Console.WriteLine("=== Тест: Отсутствующий ключ ===");
    config.GetSetting<string>("nonExistentKey");
}
catch (KeyNotFoundException ex)
{
    Console.WriteLine($"Перехвачено KeyNotFoundException: {ex.Message}\n");
}
catch (InvalidDataException ex)
{
    Console.WriteLine($"Перехвачена ошибка данных: {ex.Message}\n");
}
catch (Exception ex)
{
    Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
}

// 3. Демонстрация ошибки приведения неверного типа данных
try
{
    Console.WriteLine("=== Тест: Ошибка приведения типов ===");
    AppConfig config = new AppConfig(configPath);
    // Пытаемся прочитать текстовый URL как целое число int
    config.GetSetting<int>("baseUrl");
}
catch (InvalidDataException ex)
{
    Console.WriteLine($"Перехвачено InvalidDataException: {ex.Message}");
}