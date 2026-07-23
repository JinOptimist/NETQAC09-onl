namespace WebAppSmile.Models;

public class CurrencyRateDto
{
    public string? Date { get; set; }

    public string? Base { get; set; }

    public string? Quote { get; set; }

    public decimal Rate { get; set; }
}