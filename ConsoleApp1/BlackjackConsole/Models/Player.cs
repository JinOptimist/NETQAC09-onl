namespace BlackjackConsole.Models;

public class Player
{
    public string Name { get; set; }
    public Hand Hand { get; set; } = new();
    public int Balance { get; set; }

    public Player(string name, int balance)
    {
        Name = name;
        Balance = balance;
    }

    public virtual string ShowHand()
    {
        return $"{Hand} = {Hand.GetScore()}";
    }
}

