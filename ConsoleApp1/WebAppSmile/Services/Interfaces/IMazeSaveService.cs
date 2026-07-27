using MazeConsole;

namespace WebAppSmile.Services.Interfaces
{
    public interface IMazeSaveService
    {
        void Save(MazeContoller controller, string sessionId);
        MazeContoller? Load(string sessionId);
    }
}
