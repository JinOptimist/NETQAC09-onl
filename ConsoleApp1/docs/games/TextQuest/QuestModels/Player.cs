using System;
using System.ComponentModel.Design;

namespace Questrunner;

public class Player
{
    public bool IsTeethClean;

    public bool GameOver { get; set; } = false;

    public List<string> Inventory { get; set; } = new List<string>();

    public Room CurrentLocation { get; set; }

    public Player(Room StartingLocation)
    {
        IsTeethClean = false;
        Inventory = new List<string>();
        CurrentLocation = StartingLocation;
    }

    public void CheckStatus()
    {
        if (IsTeethClean)
        {
            CurrentLocation.LogMessages.Add("You are fresh and ready to go");
        }
        else
        {
            CurrentLocation.LogMessages.Add("Your breath stinks and you need to brush your teeth before you go.");
        }
        CurrentLocation.LogMessages.Add("You need to get cellphone, keys and lunch to go.");
        CurrentLocation.LogMessages.Add("All you have now:");
        if (Inventory.Count == 0)
        {
            CurrentLocation.LogMessages.Add("Regrets for your past.");
        }
        else
        {
            foreach (var item in Inventory)
            {
                CurrentLocation.LogMessages.Add($"- {item}");
            }
        }
    }
}
