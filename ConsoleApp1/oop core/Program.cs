using oop_core;

Console.WriteLine("НАЧИНАЕМ МАХАЧЬ!");
GameCharacter warrior_one = new GameCharacter();

//warrior_one.Name = "Арагорн";
Console.WriteLine("Введите имя воина");
var inputName = Console.ReadLine();
warrior_one.Name = inputName;

warrior_one.Health = 100;
warrior_one.Damage = 10;

GameCharacter mage_one = new GameCharacter();
mage_one.Name = "Гендальф Серый";
mage_one.Health = 200;
mage_one.Damage = 5;

Console.WriteLine($"Первого воина зовут {warrior_one.Name}, а первого мага - {mage_one.Name}");



mage_one.Mana = 12;

warrior_one.GameCharacterPrintInfo();
mage_one.GameCharacterPrintInfo();
GameCharacter.GameCharacterAttack(warrior_one,mage_one);
warrior_one.GameCharacterPrintInfo();
mage_one.GameCharacterPrintInfo();

Boss bigBoss = new Boss();
bigBoss.Name = "Саурон";
bigBoss.Health = 500;
bigBoss.Damage = 50;
bigBoss.Armor = 20;

Console.WriteLine($"Босса зовут {bigBoss.Name}");
bigBoss.GameCharacterPrintInfo();
