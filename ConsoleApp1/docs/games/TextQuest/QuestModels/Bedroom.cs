using System;
using Microsoft.VisualBasic;

namespace Questrunner;

public class Bedroom : Room
{
    public override string Name => "Bedroom";
    public bool HasPhone = true;

    public override void RoomDescribe()
    {
        LogMessages.Add("Your bed looks nice and comfortable. You feel the urge to get back to sleep.");
        if (HasPhone)
        {
            LogMessages.Add("You can see your phone on the table.");
        }
        base.RoomDescribe();

    }

    public void TakePhone(Player player)
    {
        HasPhone = false;
        player.Inventory.Add("Cellphone");
    }


    public override List<string> GetActions(Player player)
    {
        List<string> Actions = new List<string>
        {
            "1. Go to Living Room",
            "2. Get back to sleep",

        };
        if (HasPhone)
        {
            Actions.Add("3. Take your phone");
        }

        return Actions;
    }

    public override void HandleAction(int choice, Player player)
    {
        switch (choice)
        {
            case 1:
                GoToRoom(player, Exits[choice]);
                return;

            case 2:
                Console.WriteLine("You go back to sleep.");
                Console.WriteLine("But you forgot to go to work!");
                player.GameOver = true;
                return;

            case 3:
                TakePhone(player);
                return;

        }

    }
}
