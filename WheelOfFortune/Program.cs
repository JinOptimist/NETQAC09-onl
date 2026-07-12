using WheelOfFortune;
using System.Threading;

Console.WriteLine(@$" 
==================================
Добро пожаловать! 
это игра {GameConfig.GameName}
версия приложения {GameConfig.GameVersion}

стоимость кручения колеса тут фиксированная - {GameConfig.SpinCost} EUR
==================================
");


Wheel wheel = new Wheel();
SpinAnimation animation = new SpinAnimation();

Console.WriteLine("Введи свое имя");
var inputName = Console.ReadLine();


int startMoney = Player.GetIntBalance();
Player player = new Player(inputName, startMoney);

Console.WriteLine($"Тебя зовут {player.Name}, твой баланс {player.Balance} EUR");

PlayerInputManager myInput = new PlayerInputManager();

while (true)
{
    // проверяем, что у игрока есть бабки
    if (player.CanSpinWheel() == false)
    {
        break;
    }
    
    // проверяем, что игрок хочет крутить
    if (myInput.IWantToSpin() == false)
    {
        Console.WriteLine("Ты решил закончить игру. Пока! 🤝");
        break;
    }
    else
    {
        animation.CycleDivider();
        
        player.Balance = player.Balance - GameConfig.SpinCost;
        Console.WriteLine("За прокрут списалось " + GameConfig.SpinCost + " EUR");
        
        animation.PlayAnimation();
        Thread.Sleep(1000);
        
        ISector currentSector = wheel.Spin();
        currentSector.Apply(player);
        
        Console.WriteLine("Ваш баланс стал " + player.Balance + " EUR");

        int gameProfit = player.Balance - player.InitialBalance;
        Console.WriteLine("Ваш профит за игру = " + gameProfit);
    }
}


/*
while (true)
{
    if (!player.CanSpinWheel())
    {
        break;
    }
    
    if (!myInput.IWantToSpin())
    {
        Console.WriteLine("Ты решил закончить игру. Пока!  🤝");
        break;
    }
    else
    {
        animation.CycleDivider();
        player.Balance = player.Balance - GameConfig.SpinCost;
        Console.WriteLine($"За прокрут списалось {GameConfig.SpinCost} EUR");
        animation.PlayAnimation();
        Thread.Sleep(1000);
        
        var spinResult = wheel.Spin();
        player.Balance = player.Balance + spinResult.Win;
    
        Console.WriteLine($"Выпал {spinResult.Name} на сумму {spinResult.Win} EUR");
        Console.WriteLine($"Ваш баланс стал {player.Balance} EUR");

        int gameProfit = player.Balance - player.InitialBalance;
        Console.WriteLine($"/n Ваш профит за игру = {gameProfit}");
    }
}
*/



