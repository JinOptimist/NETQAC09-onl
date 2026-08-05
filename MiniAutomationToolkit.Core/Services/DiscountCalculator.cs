using System;
using System.Collections.Generic;
using System.Text;
using MiniAutomationToolkit.Core.Models;

namespace MiniAutomationToolkit.Core.Services
{
    public static class DiscountCalculator
    {
        public static decimal CalculateDiscount(decimal orderAmount, ClientType clientType)
        {
            if (orderAmount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(orderAmount), "Сумма заказа не может быть отрицательной.");
            }

            return (clientType, orderAmount) switch
            {
                (ClientType.Vip, _) => orderAmount * 0.15m,

                (ClientType.Premium, > 1000) => orderAmount * 0.10m,
                (ClientType.Premium, _) => orderAmount * 0.05m,

                (ClientType.Regular, > 1000) => orderAmount * 0.05m,
                (ClientType.Regular, _) => 0m,

                _ => 0m
            };
        }
    }
}
