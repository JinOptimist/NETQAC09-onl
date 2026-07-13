using System;

namespace Questrunner;

public class Hallway : Room
{
    public override string Name => "Hallway";
    public bool isKeysAvailable = true;

    public override void RoomDescribe()
    {
        if (isKeysAvailable)
        {
            LogMessages.Add("You can see your keys, hanging on the hook");
        }
        base.RoomDescribe();
    }

    public override List<string> GetActions(Player player)
    {
        List<string> Actions = new List<string>
        {
            "1. Go to Living Room",
            "2. Go outside"

        };
        if (isKeysAvailable)
        {
            Actions.Add("3. Take your keys");
        }

        return Actions;
    }

    public void TakeTheKeys(Player player)
    {
        isKeysAvailable = false;
        player.Inventory.Add("Keys");
    }

    public override void HandleAction(int choice, Player player)
    {
        switch (choice)
        {
            case 1:
                GoToRoom(player, Exits[choice]);
                return;

            case 2:
                var victoryPoints = 0;
                if (!player.IsTeethClean)
                {
                    LogMessages.Add("You've forgot to brush your teeth!");
                }
                else
                {
                    victoryPoints++;
                }
                if (!player.Inventory.Contains("Cellphone"))
                {
                    LogMessages.Add("How will you watch memes without a phone?");
                    LogMessages.Add("Try looking for it in a bedroom");
                }
                else
                {
                    victoryPoints++;
                }
                if (!player.Inventory.Contains("Lunch"))
                {
                    LogMessages.Add("Do you prefer hungry death?");
                    LogMessages.Add("Get your lunch in a kitchen");
                }
                else
                {
                    victoryPoints++;
                }

                if (!player.Inventory.Contains("Keys"))
                {
                    LogMessages.Add("How do you open a door without a key?");
                    LogMessages.Add("Take them in a hallway");
                }
                else
                {
                    victoryPoints++;
                }

                if (victoryPoints == 4)
                {
                    Console.WriteLine("You've done it! The world is waiting for you!");
                    player.GameOver = true;
                }

                return;

            case 3:
                TakeTheKeys(player);
                return;


        }

    }

}
