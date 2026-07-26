namespace MazeConsole;

public enum UserAction
{
    StepUp = 1,
    StepDown = 2,
    StepRight = 3,
    StepLeft = 4,
    Exit = 5,
    /// <summary>Сохранить игру (клавиша F5).</summary>
    Save = 6,
    /// <summary>Загрузить игру (клавиша F8).</summary>
    Load = 7,
}
