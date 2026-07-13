using System;

namespace Questrunner;

public abstract class Room
{
    public abstract string Name { get; }

    public Dictionary<int, Room> Exits { get; set; } = new Dictionary<int, Room>();

    public List<string> LogMessages { get; set; } = new List<string>();

    public virtual void RoomDescribe()
    {
        Console.Clear();
        Console.WriteLine(); //Нужно чтобы Console.Clear с 14 строки не стирал Console.WriteLine с 16 строки при выводе
        Console.WriteLine($"You are in the {Name}");

        foreach (var message in LogMessages)
        {
            Console.WriteLine(message);
        }
        LogMessages.Clear();
    }

    public abstract List<string> GetActions(Player player);
    public abstract void HandleAction(int choice, Player player);

    public void GoToRoom(Player player, Room targetRoom)
    {
        if (targetRoom != null)
        {
            player.CurrentLocation = targetRoom;
        }
    }



}
