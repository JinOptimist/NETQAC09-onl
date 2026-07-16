namespace ConsoleApp1;

public class User
{
    //public int MagicNumber => 42;
    //public int MagicNumberV2
    //{
    //    get
    //    {
    //        return 42;
    //    }
    //}

    // private field
    //private int _age;
    //// public mehtod
    //public int GetAge()
    //{
    //    return _age;
    //}
    //public void SetAge (int age)
    //{
    //    if (age > 0)
    //    {
    //        _age = age;
    //    }
    //}
    public string Name { get; set; }
    public bool IsMan {  get; set; }
    public int Age { get; set; }

    private DateTime _birthday;


    // Constructor
    public User()
    {
        _birthday = DateTime.Now;
    }

    public User(string name)
    {
        Name = name;
    }

    public User(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public User(int age)
    {
        Age = age;
    }


    public void SayHi()
    {
        Console.WriteLine($"Hi my name is {Name}");
    }

    public bool IsAdult()
    {
        return DateTime.Now.Year - _birthday.Year > 18;
    }

    public int Sum(int numberA, int numberB)
    {
        var answer = numberA + numberB; 
        return answer;
    }

    public int DoMagic(int number)
    {
        number = 50;
        return number;
    }

    public User DoMagic(User friend)
    {
        friend.Age = 50;
        return friend;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not User)
        {
            return false;
        }
        var user2 = (User)obj;

        return Name == user2.Name && Age == user2.Age;
    }

    public static bool operator == (User a, User b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(User a, User b)
    {
        return !a.Equals(b);
    }
}
