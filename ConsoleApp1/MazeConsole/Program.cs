using MazeConsole;

var controller = new MazeContoller();

try
{
    controller.Play();
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}