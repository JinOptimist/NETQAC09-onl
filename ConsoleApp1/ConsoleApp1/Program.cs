using ConsoleApp1;

var lvou = new Lvou();
lvou.Do();


//var userName = "Ivan";
//var userAge = 20;
//var isMan = true;

//var userNameSecond = "Lera";
//var userAgeSecond = 18;
//var isManSecond = false;

var ivan = new User();
ivan.name = "Ivan";
ivan.age = 20;
ivan.isMan = true;

var lera = new User();
lera.name = "Lera";
lera.age = 20;
lera.isMan = false;


Console.WriteLine($"{ivan.name} {ivan.age}");