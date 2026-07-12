namespace WheelOfFortune
{
    public class PrizeSector : ISector
    {
        private string _name;
        private int _amount;

        public PrizeSector(string name, int amount)
        {
            _name = name;
            _amount = amount;
        }
        
        public string Name 
        { 
            get 
            { 
                return _name; 
            } 
        }
/// <summary>
/// При выпадении призового сектора приз прибавляется к балансу игрока
/// </summary>
/// <param name="player"></param>
        public void Apply(Player player)
        {
            int currentBalance = player.Balance;
            player.Balance = currentBalance + _amount;
            
            Console.WriteLine("Выпал " + _name + "! Ты получил " + _amount + " EUR.");
        }
    }
}