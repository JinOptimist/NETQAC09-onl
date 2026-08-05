using System;
using System.Collections.Generic;
using System.Text;

namespace MiniAutomationToolkit.Core.Helpers
{
    public static class FileSearcher
    {
        public static string FindFirstScreenshot(List<string> fileNames)
        {
            // Метод расширения EndWith с параметром StringComparison.OrdinalIgnoreCase игнорирует регистр (.PNG / .png)
            var hasScreenshots = fileNames.Any(f => f.EndsWith(".png", StringComparison.OrdinalIgnoreCase));

            if (!hasScreenshots)
            {
                throw new FileNotFoundException("No screenshots found in the provided list.");
            }

            return fileNames
                .Where(f => f.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault()!;
        }
    }
}
