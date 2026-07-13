namespace WheelOfFortune
{
    public class FinishSector : ISector
    {
        private string _name;

        public FinishSector()
        {
            _name = "Сектор finish 😫";
        }

        public string Name 
        { 
            get 
            { 
                return _name; 
            } 
        }
        /// <summary>
        /// при выпадении сектора Finish баланс игрока обнуляется
        /// </summary>
        /// <param name="player"></param>
        public void Apply(Player player)
        {
            int currentBalance = player.Balance;
            player.Balance = 0;
            
            Console.WriteLine("Выпал " + _name + "! Твой баланс обнулился, сорри, бро...");
        }
    }
}