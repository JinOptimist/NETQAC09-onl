namespace SeaBattleConsole.SeaBattleModels.Cells;

public enum CellType
{
    neutral = 0,
    player1 = 1,
    player2 = 2
}

public enum HitType
{
    NonValid,
    Hit,
    Miss
}
public enum Direction
{
    Up,
    Down,
    Left,
    Right,
    None
}