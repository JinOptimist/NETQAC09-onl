using System;
using Microsoft.VisualBasic;

namespace Questrunner;

public class Kitchen : Room
{
    public override string Name => "Kitchen";
    public bool IsLunchAvailable = true;

    public override void RoomDescribe()
    {
        LogMessages.Add("You see a bottle of whiskey on the table");
        if (IsLunchAvailable)
        {
            LogMessages.Add("Your lunch is packed and ready");
        }
        base.RoomDescribe();
    }

    public void DrinkSome(Player player)
    {
        Console.WriteLine("Why waste your time at the office if you can become alcoholic and be free?");
        player.GameOver = true;
    }


    public override List<string> GetActions(Player player)
    {
        List<string> Actions = new List<string>
        {
            "1. Go to Living Room",
            "2. Drink whiskey"

        };
        if (IsLunchAvailable)
        {
            Actions.Add("3. Take your lunch");
        }

        return Actions;
    }
    public void TakeLunch(Player player)
    {
        IsLunchAvailable = false;
        player.Inventory.Add("Lunch");
    }

    public override void HandleAction(int choice, Player player)
    {
        switch (choice)
        {
            case 1:
                GoToRoom(player, Exits[choice]);
                return;

            case 2:
                DrinkSome(player);
                return;

            case 3:
                TakeLunch(player);
                return;

        }

    }
}
