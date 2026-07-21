using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;
using MazeConsole.MazeModels.Intefaces;
using Moq;
using NUnit.Framework;

namespace MazeConsole.Tests.MazeModels.Cells;

public class PaidDoorTest
{
    // Проверяем, что если у игрока хватает монет, дверь открывается и метод возвращает true
    [Test]
    public void PlayerStepInMe_CanOpenDoor_WhenPlayerHasEnoughCoins()
    {
        // Preparation
        var playerMock = new Mock<IPlayer>();
        playerMock.SetupAllProperties();
        var player = playerMock.Object;
        player.Coin = 2; // монет ровно столько, сколько стоит дверь

        var mazeMock = new Mock<IMaze>();
        mazeMock.Setup(maze => maze.LogMessages)
            .Returns(new List<string>());

        var maze = mazeMock.Object;

        var paidDoor = new PaidDoor();
        paidDoor.MazeWhereIWasCreated = maze;

        // Act
        var result = paidDoor.PlayerStepInMe(player);

        // Assert
        Assert.IsTrue(result, "Player with enough coins should be able to open the door");
    }

    // Проверяем, что при нехватке монет в лог добавляется предупреждение для игрока
    [Test]
    public void PlayerStepInMe_AddsWarningLog_WhenPlayerDoesNotHaveEnoughCoins()
    {
        // Preparation
        var playerMock = new Mock<IPlayer>();
        playerMock.SetupAllProperties();
        var player = playerMock.Object;
        player.Coin = 0;

        var logMessages = new List<string>();
        // список создан отдельной переменной
        var mazeMock = new Mock<IMaze>();
        mazeMock.Setup(maze => maze.LogMessages)
            .Returns(logMessages);

        var maze = mazeMock.Object;

        var paidDoor = new PaidDoor();
        paidDoor.MazeWhereIWasCreated = maze;

        // Act
        Assert.Throws<Exception>(() => paidDoor.PlayerStepInMe(player));

        // Assert
        Assert.IsTrue(logMessages.Contains("You need 2 coins to open this door"),
            "We expect a warning message to be logged when player can't afford the door");
    }


    // Проверяем, что при успешном открытии дверь заменяется на землю
    [Test]
    public void PlayerStepInMe_ReplaceCellToGround_WhenDoorIsOpened()
    {
        // Preparation
        var playerMock = new Mock<IPlayer>();
        playerMock.SetupAllProperties();
        var player = playerMock.Object;
        player.Coin = 5; // монет с запасом, дверь точно должна открыться

        var mazeMock = new Mock<IMaze>();
        mazeMock.Setup(maze => maze.LogMessages)
            .Returns(new List<string>());

        var maze = mazeMock.Object;

        var paidDoor = new PaidDoor();
        paidDoor.MazeWhereIWasCreated = maze;

        // Act
        paidDoor.PlayerStepInMe(player);
        // тут исключения не будет, поэтому try-catch не нужен

        // Assert
        mazeMock.Verify(x => x.ReplaceCellToGround(paidDoor),
            Times.Once,
            "We think that door cell must be replaced with ground once opened");
    }

    
    // Проверяем, что монеты списываются ровно на цену двери, сразу на нескольких примерах
    [Test]
    [TestCase(2, 0)]     // было 2 монеты - осталось 0
    [TestCase(5, 3)]     // было 5 монет - осталось 3
    [TestCase(100, 98)]  // было 100 монет - осталось 98
    public void PlayerStepInMe_CoinIsDecreasingByDoorPrice_WhenDoorIsOpened(int coinBefore, int coinAfter)
    {
        // Preparation
        var paidDoor = new PaidDoor();

        var playerMock = new Mock<IPlayer>();
        playerMock.SetupProperty(x => x.Coin);
        // настраиваем "запоминание" только для свойства Coin, остальные свойства не нужны

        var player = playerMock.Object;
        player.Coin = coinBefore;

        var mazeMock = new Mock<IMaze>();
        mazeMock.Setup(maze => maze.LogMessages)
            .Returns(new List<string>());
        paidDoor.MazeWhereIWasCreated = mazeMock.Object;

        // Act
        paidDoor.PlayerStepInMe(player);

        // Assert
        Assert.IsTrue(player.Coin == coinAfter,
            $"Expected coin balance to be {coinAfter} after paying for the door, but was {player.Coin}");
    }
}