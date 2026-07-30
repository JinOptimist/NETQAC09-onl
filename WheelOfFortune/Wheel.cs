namespace WheelOfFortune
{
    public class Wheel
    {
        private Random _rnd = new Random();
        private List<ISector> _sectors;
        
        /// <summary>
        /// метод сборки колеса
        /// </summary>
        public Wheel()
        {
            _sectors = new List<ISector>();
            
            // сборка
            _sectors.Add(new PrizeSector("Призовой сектор", 10));
            _sectors.Add(new PrizeSector("Призовой сектор", 20));
            _sectors.Add(new PenaltySector(20));
            _sectors.Add(new ZeroSector());
            _sectors.Add(new PenaltySector(10));
            _sectors.Add(new ZeroSector());
            _sectors.Add(new FinishSector());
            _sectors.Add(new FinishSector());
            _sectors.Add(new FinishSector());
        }

        /// <summary>
        /// кручение колеса
        /// </summary>
        /// <returns>возвращает id сектора из списка</returns>
        public ISector Spin()
        {
            int index = _rnd.Next(0, _sectors.Count);
            return _sectors[index];
        }
    }
}

/*namespace WheelOfFortune;

public class Wheel
{
    private List<(string Name, int Win)> _sectors = new List<(string, int)>
    {
        ("Призовой сектор", 10),
        ("Призовой сектор", 20),
        ("Штрафной сектор", -20),
        ("Сектор zero", 0)
    };

    public (string Name, int Win) Spin ()
    {
        Random rnd = new Random();
        int spinResult = rnd.Next(0, _sectors.Count);
        return _sectors[spinResult];
    }
}
*/