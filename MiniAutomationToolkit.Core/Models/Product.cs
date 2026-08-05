using System;
using System.Collections.Generic;
using System.Text;

namespace MiniAutomationToolkit.Core.Models
{
    public record Product(string Name, decimal Price, ProductCategory Category);
}
