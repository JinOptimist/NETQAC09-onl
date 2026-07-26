using MazeConsole.MazeModels;
using MazeConsole.Save;

namespace MazeConsole;

public class MazeContoller
{
    private Maze _maze = null!;
    private readonly FileLogger _logger;

    private readonly GameSaveService _saveService;

    public MazeContoller()
    {
        _logger = new FileLogger();
        _saveService = new GameSaveService();
    }

    public Maze Maze => _maze;

    public bool IsAlive => _maze?.Player.CurrentHealth > 0;

    public void StartNewGame()
    {
        MazeGeneration();
    }

    public void Play()
    {
        var drawer = new MazeDrawer();

        StartNewGame();

        while (true)
        {
            drawer.Draw(_maze);

            if (!IsAlive)
            {
                Console.WriteLine();
                Console.WriteLine("You died! Game over.");
                break;
            }

            var userAction = GetUserAction();

            if (userAction == UserAction.Exit)
            {
                break;
            }

            try
            {
                // SaveGame/LoadGame — не ходы по карте, поэтому не вызываем PerformAction
                if (userAction == UserAction.Save)
                {
                    SaveGame();
                    continue;
                }

                if (userAction == UserAction.Load)
                {
                    LoadGame();
                    continue;
                }

                PerformAction(userAction);
            }
            catch (Exception ex)
            {
                _logger.AddLog(ex.Message);
                throw;
            }
        }
    }

    public void PerformAction(UserAction actionWhichUserTryToDo)
    {
        var destinationX = _maze.Player.X;
        var destinationY = _maze.Player.Y;

        switch (actionWhichUserTryToDo)
        {
            case UserAction.StepUp:
                destinationY--;
                break;
            case UserAction.StepDown:
                destinationY++;
                break;
            case UserAction.StepRight:
                destinationX++;
                break;
            case UserAction.StepLeft:
                destinationX--;
                break;
            default:
                throw new Exception($"Unkown UserAction {actionWhichUserTryToDo}");
        }

        var destinationCell = _maze
            .Cells
            .SingleOrDefault(cell => cell.X == destinationX && cell.Y == destinationY);

        if (destinationCell?.PlayerStepInMe(_maze.Player) ?? false)
        {
            _maze.Player.X = destinationX;
            _maze.Player.Y = destinationY;
        }
    }

    // сохранить игру
    public void SaveGame()
    {
        _saveService.SaveGame(_maze);
        _maze.LogMessages.Add("Game saved.");
    }

    // загрузить игру
    public void LoadGame()
    {
        if (!_saveService.isSaveFileExists())
        {
            _maze.LogMessages.Add("SaveGame file not found");
            return;
        }

        _maze = _saveService.LoadGame();
        _maze.LogMessages.Add("Game loaded.");
    }

    private void MazeGeneration()
    {
        var builder = new MazeBuilder();

        var attempt = 0;
        const int maxAttempts = 25;
        while (attempt < maxAttempts)
        {
            try
            {
                _maze = builder.BuildTestMaze();
                return;
            }
            catch (Exception ex)
            {
                _logger.AddLog(ex.Message);
            }

            attempt++;
        }

        throw new Exception($"We try build maze {attempt}. All fail. Read logs");
    }

    private UserAction GetUserAction()
    {
        while (true)
        {
            var key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.Escape:
                    return UserAction.Exit;
                case ConsoleKey.F5: // сохранить
                    return UserAction.Save;
                case ConsoleKey.F8: // загрузить
                    return UserAction.Load;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    return UserAction.StepRight;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    return UserAction.StepLeft;
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    return UserAction.StepUp;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    return UserAction.StepDown;
            }
        }
    }
}
