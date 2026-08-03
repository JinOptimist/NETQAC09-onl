using System;

namespace Questrunner;

public class LivingRoom : Room
{
    public override string Name => "LivingRoom";
    public bool isTVOn = true;

    public override void RoomDescribe()
    {
        LogMessages.Add("Your bed looks nice and comfortable. You feel the urge to get back to sleep.");
        if (isTVOn)
        {
            LogMessages.Add("TV is on. Looks like something interesting is happening.");
        }
        else
        {
            LogMessages.Add("TV is off.");
        }

        base.RoomDescribe();
    }

    public override List<string> GetActions(Player player)
    {
        List<string> Actions = new List<string>
        {
            "1. Go to Bedroom",
            "2. Go to Bathroom",
            "3. Go to Kitchen",
            "4. Go to Hallway",
            "5. Check your readiness",

        };
        if (isTVOn)
        {
            Actions.Add("6. Watch TV");
            Actions.Add("7. Turn off the TV");
        }
        else
        {
            Actions.Add("6. Turn on the TV");
        }

        return Actions;
    }

    public void ClickTheTV()
    {
        isTVOn = !isTVOn;
    }

    public override void HandleAction(int choice, Player player)
    {
        switch (choice)
        {
            case >= 1 and <= 4:
                GoToRoom(player, Exits[choice]);
                return;

            case 5:
                player.CheckStatus();
                return;

            case 6:
                Console.WriteLine("You watch TV for a day. It's nice and relaxing.");
                Console.WriteLine("But you forgot to go to work!");
                player.GameOver = true;
                return;

            case 7:
                ClickTheTV();
                return;


        }

    }

}
