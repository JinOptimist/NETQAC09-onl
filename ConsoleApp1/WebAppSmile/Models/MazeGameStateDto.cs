namespace WebAppSmile.Models;

public class MazeGameStateDto
{
    public int Width { get; set; }
    public int Height { get; set; }
    public int Seed { get; set; }
    public bool IsAlive { get; set; }
    public bool IsFailed { get; set; }
    public string? ErrorMessage { get; set; }
    public PlayerStatusDto Player { get; set; } = new();
    public List<MazeCellDto> Cells { get; set; } = new();
    public List<string> Messages { get; set; } = new();
}

public class PlayerStatusDto
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Coin { get; set; }
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }
    public int HealthPotion { get; set; }
    public int Sand { get; set; }
    public int Flowers { get; set; }
}

public class MazeCellDto
{
    public int X { get; set; }
    public int Y { get; set; }
    public string Type { get; set; } = string.Empty;
    public bool IsPlayer { get; set; }
}

public class MazeMoveRequest
{
    public string Action { get; set; } = string.Empty;
}
