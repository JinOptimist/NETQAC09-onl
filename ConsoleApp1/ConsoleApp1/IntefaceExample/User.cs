namespace ConsoleApp1.IntefaceExample;

internal class User : ICanDraMySelfInConsole
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }

    public void DrawMySelfInConsole()
    {
        throw new NotImplementedException();
    }
}
