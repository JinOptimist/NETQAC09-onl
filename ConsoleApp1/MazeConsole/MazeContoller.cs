using System.Reflection.PortableExecutable;
using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;

namespace MazeConsole;

public class MazeContoller
{
    private Maze _maze;

    public void Play()
    {
        var builder = new MazeBuilder();
        var drawer = new MazeDrawer();

        _maze = builder.BuildTestMaze();

        while (true)
        {
            drawer.Draw(_maze);

            var userAction = GetUserAction();

            if (userAction == UserAction.Exit)
            {
                break;
            }

            PerfomAction(userAction);
        }
    }

    private void PerfomAction(UserAction actionWhichUserTryToDo)
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
                break;
        }

        var destinationCell = _maze
            .Cells
            .SingleOrDefault(cell => cell.X == destinationX && cell.Y == destinationY);

        //if (destinationCell != null && destinationCell.PlayerStepInMe(_maze.Player))
        var tree = new Tree();
        if (destinationCell?.PlayerStepInMe(_maze.Player) ?? false)
        {
            _maze.Player.X = destinationX;
            _maze.Player.Y = destinationY;
        }
        
      //  if (destinationCell == tree)
      //  {
      //      var toGos = _maze
      //          .Cells
     //           .Where(x => x is Ground)
     //           .ToList;
      //      var toGo = GetRandomFromList(toGos);
      //          _maze.Player.X = toGo.X;
     //           _maze.Player.Y = toGo.Y;
     //   }
    }

    private object GetRandomFromList(object ground)
    {
        throw new NotImplementedException();
    }

    private object GetRandomFromList(Ground ground)
    {
        throw new NotImplementedException();
    }

    private UserAction GetUserAction()
    {
        var key = Console.ReadKey();

        while (true)
        {
            switch (key.Key)
            {
                case ConsoleKey.Escape:
                    return UserAction.Exit;
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
                default:
                    break;
            }
        }
    }
}
