using ConsoleApp1.IntefaceExample;

var elemtToDraw = new List<ICanDraMySelfInConsole>();

elemtToDraw.Add(new User());
elemtToDraw.Add(new Cirle());

foreach (var elemt in elemtToDraw)
{
    elemt.DrawMySelfInConsole();
}