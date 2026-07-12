namespace MazeConsole.MazeModels
{
    public interface IHuman
    {
        IPet MyPet { get; set; }

        int GetAge();
        string GetMyName();
    }
}