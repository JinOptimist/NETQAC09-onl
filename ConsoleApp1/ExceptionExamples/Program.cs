using ExceptionExamples;
using System.Security.AccessControl;

Console.WriteLine("Good 1");

var user = new User();

Console.WriteLine("Good 2");


try
{
    // Опасный код. Может упасть. Я знаю это
    user.SetAge(10);

    Console.WriteLine("Good 3");
    
    // user.SetAge(-5);
    // user.DoMagic(42, null);
}
catch (BadUserDataException ex)
{
    Console.WriteLine("User do something unexpected");

    throw;
    throw ex;
    throw new Exception("Bad");
}
catch (Exception ex)
{
    Console.WriteLine("Very bad. I don't know what to do");
    
    //throw new Exception("New exception");
}
finally
{
    // do it a any case
    Console.WriteLine("Do maintances");
}

Console.WriteLine("Good 4");

Console.WriteLine("Good 5");