using ConsoleApp1.IntefaceExample;

var elemtToDraw = new List<ICanDraMySelfInConsole>();

elemtToDraw.Add(new UserWithInterface());
elemtToDraw.Add(new Cirle());

foreach (var elemt in elemtToDraw)
{
    elemt.DrawMySelfInConsole();
}




//using ConsoleApp1;

//var user1 = new User()
//{
//    Name = "Test",
//    Age = 50
//};

//var user2 = new User()
//{
//    Name = "Test",
//    Age = 50
//};

//if (user1 == user2)
//{
//    Console.WriteLine("+");
//}
//else
//{
//    Console.WriteLine("-");
//}


//var userValue1 = new UserButValue();
//userValue1.Year = 2020;
//var userValue2 = new UserButValue();
//userValue2.Year = 2020;

//if (userValue1.Equals(userValue2))
//{
//    Console.WriteLine("+");
//}


//var a = new UserRecord("Ivan", 50, true);
//var b = new UserRecord("Ivan", 50, true);

//if (a == b)
//{
//    Console.WriteLine("+");
//}
