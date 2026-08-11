using System.Text.Json.Serialization;

namespace WebAppSmile.Models;

public class CoffeeDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;

    public List<string> Ingredients { get; set; } = new();
}