using MazeConsole;
using MazeConsole.MazeExceptions;

var controller = new MazeContoller();

try
{
    controller.Play();
}

catch (IceCellExceptions ex) //можно протестировать на public Maze BuildTestMaze(int width = 12, int height = 9, int? seed = 932), предварительно раскомментив "сломанную" логику скольжения в Ice.cs
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Oops smth went wrong");
    Console.WriteLine(ex.Message);
    Console.ResetColor();
}

catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}