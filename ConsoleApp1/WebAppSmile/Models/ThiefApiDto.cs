using System.Text.Json.Serialization;

namespace WebAppSmile.Models;

public class ThiefApiDto
{
    public string Name { get; set; } = string.Empty;

    public int Height { get; set; }

    public int Weight { get; set; }

    public PokemonSpritesDto? Sprites { get; set; }
}
public class PokemonSpritesDto
{
    [JsonPropertyName("front_default")]
    public string? FrontDefault { get; set; }
}
