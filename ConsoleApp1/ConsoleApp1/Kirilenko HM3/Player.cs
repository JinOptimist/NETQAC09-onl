class Player
{
    public string name { get; set; }
    public bool realPlayer { get; }
    public Player(string nameInput, bool playerType)
    {
        name = nameInput;
        realPlayer = playerType;
    }
}