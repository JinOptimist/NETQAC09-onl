namespace oop_core;

public class GameCharacter
{

    //public string Name { get; set; }
    
    private string _name;
    
    public string Name
    {
        get {
            return _name;
        }
        set 
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                _name = "Безымянный воин"; 
                Console.WriteLine($"Бро, ты ввел пустое имя, так что воина - {this._name}");
            }
            else
            {
                _name = value;
                Console.WriteLine($"Бро, персонажу присвоено имя {this._name}");
            }
        }
    }
    
    
   // public int Health { get; set; }
   
   private int _health;

   public int Health
   {
       get
       {
           return _health;
       }
       set
       {
           if (value < 0)
           {
               _health = 0;
           }
           else
               {
               _health = value;
               }
       }
       
   }    
    public int Damage { get; set; }

    public bool IsAlive
    {
        get
        {
            if (Health > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private int _mana;
    
    public int Mana
    {
        get
        {
            return _mana;
        }
        set
        {
            _mana = new Random().Next(2, 100);
        }
    }
    
    public virtual void GameCharacterPrintInfo ()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Health: {Health}");
        Console.WriteLine($"Damage: {Damage}");
        Console.WriteLine($"Mana: {Mana}");
        Console.WriteLine($"IsAlive: {IsAlive}");
    }
    
    public static void GameCharacterAttack(GameCharacter attacker, GameCharacter target)
    {
        target.Health = target.Health - attacker.Damage;
        Console.WriteLine($"{attacker.Name} атаковал {target.Name} и теперь здоровье  {target.Name} стало {target.Health}");
        if (target.Mana > 50)
        {
            attacker.Health = attacker.Health - target.Damage * 100;
            Console.WriteLine(
                $"В ответ {target.Name} двинул {attacker.Name} и нанес ему урон, здоровье {attacker.Name} теперь {attacker.Health}");

        }

        else if (target.Mana > 20 && target.Mana <=50)
        {
            attacker.Health = attacker.Health - target.Damage;
            Console.WriteLine(
                $"В ответ {target.Name} двинул {attacker.Name} и нанес ему урон, здоровье {attacker.Name} теперь {attacker.Health}");
        }
        else
        {
            Console.WriteLine($"Манны у {target.Name} не достаточно, чтобы ответить");
        }

        Console.WriteLine("Бой завершен");
    }
}