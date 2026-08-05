using System;
using System.Collections.Generic;
using System.Text;

namespace MiniAutomationToolkit.Core.Simulations
{
    public class LongOperationSimulator
    {
        // Синхронный метод: жестко блокирует текущий поток
        public string LongOperation()
        {
            Thread.Sleep(2000);
            return "Done";
        }

        // Асинхронный метод: освобождает поток на время ожидания
        public async Task<string> LongOperationAsync()
        {
            await Task.Delay(2000);
            return "Done";
        }
    }
}
