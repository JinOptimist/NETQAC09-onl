namespace WheelOfFortune
{
    public class PenaltySector : ISector
    {
        private string _name;
        private int _penalty;

        public PenaltySector(int penalty)
        {
            _name = "Штрафной сектор";
            _penalty = penalty;
        }
        public string Name 
        { 
            get 
            { 
                return _name; 
            } 
        }

        /// <summary>
        /// при выпадении Штрафного сектора вычитаем сумму сектора из баланса игрока
        /// </summary>
        /// <param name="player"></param>
        public void Apply(Player player)
        {
            int currentBalance = player.Balance;
            player.Balance = currentBalance - _penalty;
            
            Console.WriteLine(_name + "! Списано " + _penalty + " EUR.");
        }
    }
}