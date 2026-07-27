namespace MazeConsole.Save;

// Снимок игры — то, что пишется в JSON.
// Сами классы Maze/Cell в файл не сериализуем (циклы ссылок, абстракции).
public class GameSaveDto
{
    public MazeSaveDto Maze { get; set; } = new();
    public PlayerSaveDto Player { get; set; } = new();
    public List<CellSaveDto> Cells { get; set; } = new(); // все клетки карты
}

// Размер лабиринта и seed (по seed при загрузке создаём новый Random)
public class MazeSaveDto
{
    public int Width { get; set; }
    public int Height { get; set; }
    public int Seed { get; set; }
}

// Позиция игрока и его инвентарь/статы
public class PlayerSaveDto
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

// Одна клетка в сейве.
// Type — имя класса ("Coin", "Wall"...), по нему при загрузке создаём нужный тип.
// CoinCount / VisitCount / Durability — доп. состояние; для обычных клеток могут быть null.
public class CellSaveDto
{
    public string Type { get; set; } = string.Empty;
    public int X { get; set; }
    public int Y { get; set; }
    public int? CoinCount { get; set; }   // сколько монет ещё на клетке Coin
    public int? VisitCount { get; set; }  // сколько раз заходили в MimicChest
    public int? Durability { get; set; }  // прочность Wall / Tree
}
