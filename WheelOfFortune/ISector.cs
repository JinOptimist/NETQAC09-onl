namespace WheelOfFortune
{
    public interface ISector
    {
        string Name { get; }
        
        // метод, который принимает игрока и что-то делает с его балансом
        void Apply(Player player);
    }
}