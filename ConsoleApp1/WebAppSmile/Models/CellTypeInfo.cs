namespace WebAppSmile.Models;

public class CellTypeInfo
{
    public required string TypeKey { get; init; }
    public required string TitleRu { get; init; }
    public required string Teaser { get; init; }
    public required string Category { get; init; }
    public string? Link { get; init; } // чтобы можно было положить ссылку на страницу в кодекс
}
