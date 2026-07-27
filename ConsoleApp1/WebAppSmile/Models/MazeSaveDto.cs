namespace WebAppSmile.Models;

public class MazeSaveDto
{
    public int Width { get; set; }
    public int Height { get; set; }
    public int Seed { get; set; }
    public List<MazeSaveCellDto> Cells { get; set; } = new();
    public MazeSavePlayerDto Player { get; set; } = new();
    public List<string> Messages { get; set; } = new();
}

public class MazeSaveCellDto
{
    public int X { get; set; }
    public int Y { get; set; }
    public string Type { get; set; } = string.Empty;
}

public class MazeSavePlayerDto
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Coin { get; set; }
    public int CurrentHealth { get; set; }
    public int HealthPotion { get; set; }
    public int Sand { get; set; }
    public int Flowers { get; set; }
    public int SnakeMeets { get; set; }
}
