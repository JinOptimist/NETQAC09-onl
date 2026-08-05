using System;
using System.Collections.Generic;
using System.Text;

namespace MiniAutomationToolkit.Core.Services
{
    public class ErrorLogger
    {
        public string? TryReadFile(string sourceFilePath, string logFilePath)
        {
            try
            {
                // Попытка прочитать файл целиком
                return File.ReadAllText(sourceFilePath);
            }
            catch (Exception ex) when (ex is FileNotFoundException || ex is UnauthorizedAccessException)
            {
                // Формируем строку лога: <дата и время> | <тип исключения> | <сообщение>
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {ex.GetType().Name} | {ex.Message}{Environment.NewLine}";

                // Дозаписываем в файл (создается автоматически, если его не было)
                File.AppendAllText(logFilePath, logEntry);

                return null;
            }
        }
    }
}
