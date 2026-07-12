namespace WheelOfFortune;

public class Player
{
    public string Name { get; set; }
    private int _balance;   
    public int Balance
    {
        get => _balance;
        set 
        { 
            if (value < 0)
            {
                _balance = 0;
            }
            else
            {
                _balance = value;
            }
        }
    }
    public int InitialBalance { get; private set; }
    
    public Player(string name, int balance) 
    {
        Name = name;
        Balance = balance; 
        InitialBalance = balance; // запоминаем значение начального баланса
    }
/// <summary>
/// проверка на int
/// </summary>
/// <returns> возвращает баланс, если ввели норм, иначе требует ввести снова </returns>
    public static int GetIntBalance()
    {
        int balance;
        while (true)
        {
            Console.WriteLine("Сколько у тебя денег?");
            var input = Console.ReadLine();
            if (int.TryParse(input, out balance) && balance >= 0)
            {
                return balance;
            }
            else
            {
                Console.WriteLine("Ты какую-то фигню ввел, а не баланс");
            }
        }
    }
/// <summary>
/// Проверка, что игрок может крутить
/// </summary>
/// <returns>True если денег больше, чем стоимость прокрута</returns>
    public bool CanSpinWheel()
    {
        if (_balance>=GameConfig.SpinCost)
        {
            return true;
        }
        else
        {
            Console.WriteLine($"{Name}, твой баланс меньше стоимости прокрута {GameConfig.SpinCost}");
            return false;
        }
    }

}