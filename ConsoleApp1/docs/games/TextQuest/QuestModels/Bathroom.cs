using System;
using Microsoft.VisualBasic;

namespace Questrunner;

public class Bathroom : Room
{
    public override string Name => "Bathroom";
    public bool IsBrushAvailable = true;

    public override void RoomDescribe()
    {

        LogMessages.Add("Room looks clean");
        if (IsBrushAvailable)
        {
            LogMessages.Add("You can see your toothbrush.");
        }
        base.RoomDescribe();
    }

    public void CleanTheTeeth(Player player)
    {
        IsBrushAvailable = false;
        player.IsTeethClean = true;
    }


    public override List<string> GetActions(Player player)
    {
        List<string> Actions = new List<string>
        {
            "1. Go to Living Room"

        };
        if (IsBrushAvailable)
        {
            Actions.Add("2. Brush your teeth");
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
                CleanTheTeeth(player);
                return;

        }

    }
}
