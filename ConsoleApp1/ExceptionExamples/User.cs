namespace ExceptionExamples;

public class User
{
    public string Name { get; set; } = "Ivan";
    private int _age;

    public void SetAge(int age)
    {
        if (age <= 0)
        {
            // так не бывает. Генерим ошибку (стопкран)
            var ex = new BadUserDataException(Name, "Stop. Age must be positive");
            throw ex;
        }

        _age = age;
    }

    public void DoMagic(int number, string name)
    {
        if (number == 42)
        {
            throw new Exception("Bad number");
        }

        if (name is null)
        {
            throw new BadUserDataException(Name, "Bad name");
        }
    }
}
