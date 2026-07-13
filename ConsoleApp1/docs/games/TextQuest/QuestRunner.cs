using System;
using Microsoft.VisualBasic;

namespace Questrunner;

public class QuestRunner
{


    public void Play()
    {
        // добавить List<string> LogMessages { get; set; }; в один из классов, чтобы нормально выводить сообщения
        Room bedroom = new Bedroom();
        Room livingRoom = new LivingRoom();
        Room bathRoom = new Bathroom();
        Room kitchen = new Kitchen();
        Room hallway = new Hallway();
        Player player = new Player(bedroom);
        bedroom.Exits.Add(1, livingRoom);
        kitchen.Exits.Add(1, livingRoom);
        bathRoom.Exits.Add(1, livingRoom);
        hallway.Exits.Add(1, livingRoom);
        livingRoom.Exits.Add(1, bedroom);
        livingRoom.Exits.Add(2, bathRoom);
        livingRoom.Exits.Add(3, kitchen);
        livingRoom.Exits.Add(4, hallway);
        Console.WriteLine("Welcome to the quest!");
        Console.WriteLine("You wake up in your bed and you feel great!");
        Console.WriteLine("Sadly, you've overslept and you need to get to work asap!");
        Console.WriteLine("Take your cellphone, keys and lunch and get going! And don't forget to brush your teeth.");
        Thread.Sleep(000);

        do
        {
            player.CurrentLocation.RoomDescribe();
            var action = player.CurrentLocation.GetActions(player);
            foreach (var act in action)
            {
                Console.WriteLine(act);
            }

            int choice = GetPlayerChoice(action);
            player.CurrentLocation.HandleAction(choice, player);

        } while (!player.GameOver);
    }


    static int GetPlayerChoice(List<string> actions)
    {
        while (true)
        {
            Console.Write($"What will you do?: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= actions.Count)
            {
                return choice;
            }

            Console.WriteLine("That's not an option! Pick one from above");
        }
    }
}

