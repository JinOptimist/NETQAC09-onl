namespace WheelOfFortune
{
    public class ZeroSector : ISector
    {
        private string _name;

        public ZeroSector()
        {
            _name = "Сектор zero";
        }

        public string Name 
        { 
            get 
            { 
                return _name; 
            } 
        }
/// <summary>
/// при выпадении сектора Zero ничего не делаем
/// </summary>
/// <param name="player"></param>
        public void Apply(Player player)
        {
            // Здесь ничего не меняем в балансе, просто выводим сообщение
            Console.WriteLine(_name + "! Ничего не произошло.");
        }
    }
}