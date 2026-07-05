using MazeConsole;

var controller = new MazeContoller();

try
{
    controller.Play();
}
catch(Exception ex)
{
    Console.WriteLine("Sorry. We are fail");
}