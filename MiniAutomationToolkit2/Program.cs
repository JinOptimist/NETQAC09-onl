using System;
using System.Collections.Generic;
using MiniAutomationToolkit.Core.Helpers;

// 1. Список с 20 перемешанными файлами (включает скриншоты в разном регистре)
List<string> fileNames = new List<string>
{
    "error_2024.log", "debug.txt", "screen_001.png", "notes.txt",
    "SCREEN_002.PNG", "app.config", "report.xlsx", "trace.log",
    "screen_003.png", "dump.dmp", "setup.exe", "readme.md",
    "image.jpg", "screen_004.png", "output.txt", "data.json",
    "build.log", "script.sh", "screen_005.png", "temp.tmp"
};

// 2. Список без скриншотов
List<string> fileNamesWithoutScreenshots = new List<string>
{
    "error_2024.log", "debug.txt", "notes.txt", "app.config", "trace.log"
};

try
{
    Console.WriteLine("=== Тест 1: Поиск скриншота ===");
    string firstScreenshot = FileSearcher.FindFirstScreenshot(fileNames);
    Console.WriteLine($"Found screenshot: {firstScreenshot}");
}
catch (FileNotFoundException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

Console.WriteLine();

try
{
    Console.WriteLine("=== Тест 2: Поиск в списке без скриншотов ===");
    string firstScreenshot = FileSearcher.FindFirstScreenshot(fileNamesWithoutScreenshots);
    Console.WriteLine($"Found screenshot: {firstScreenshot}");
}
catch (FileNotFoundException ex)
{
    Console.WriteLine($"Caught expected exception: {ex.Message}");
}
