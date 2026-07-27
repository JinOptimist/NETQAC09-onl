using System.Collections.Concurrent;
using MazeConsole;

namespace WebAppSmile.Services;

public class MazeGameSessionStore
{
    private readonly ConcurrentDictionary<string, MazeContoller> _games = new();

    public MazeContoller GetOrCreate(string sessionId)
    {
        return _games.GetOrAdd(sessionId, _ =>
        {
            var controller = new MazeContoller();
            controller.StartNewGame();
            return controller;
        });
    }

    public MazeContoller Restart(string sessionId)
    {
        var controller = new MazeContoller();
        controller.StartNewGame();
        _games[sessionId] = controller;
        return controller;
    }

    public MazeContoller Set(string sessionId, MazeContoller controller)
    {
        _games[sessionId] = controller;
        return controller;
    }
}
