using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;
using MazeConsole.MazeModels.Intefaces;
using Moq;
using NUnit.Framework;

namespace MazeConsole.Tests.MazeModels.Cells;

public class MimicTest
{
    [Test]
    public void PlayerStepInMe_CanStepToMimic()
    {
        // Preparation
        var playerMock = new Mock<IPlayer>();   
        playerMock.SetupAllProperties();
        var player = playerMock.Object;
        var mazeMock = new Mock<IMaze>();      
        mazeMock.Setup(maze => maze.LogMessages)
            .Returns(new List<string>());
        var randomMock = new Mock<Random>();
        randomMock
            .Setup(x => x.Next(2))
            .Returns(1);
        var maze = mazeMock.Object;

        var mimic = new MimicChest(randomMock.Object);
        mimic.MazeWhereIWasCreated = maze;

        // Act
        var result = mimic.PlayerStepInMe(player);

        // Assert
        Assert.IsTrue(result, "We expect that player can step on Mimic");
    }
    [TestCase(3)]
    public void PlayerStepInMe_CoinIsIncreased(int coinBefore)
    {
        // Preparation
        var playerMock = new Mock<IPlayer>();
        playerMock.SetupAllProperties();
        var player = playerMock.Object;
        player.Coin = coinBefore;
        var mazeMock = new Mock<IMaze>();
        mazeMock.Setup(maze => maze.LogMessages)
            .Returns(new List<string>());
        var randomMock = new Mock<Random>();
        randomMock
            .Setup(x => x.Next(2))
            .Returns(0);
        var maze = mazeMock.Object;

        var mimic = new MimicChest(randomMock.Object);
        mimic.MazeWhereIWasCreated = maze;

        // Act
        var result = mimic.PlayerStepInMe(player);
        // Assert
        Assert.IsTrue(player.Coin == coinBefore + 1, "We expect that coin will be increased");
    }
    [TestCase(3)]
    public void PlayerStepInMe_HPIsIncreased(int hpBefore)
    {
        // Preparation
        var playerMock = new Mock<IPlayer>();
        playerMock.SetupAllProperties();
        var player = playerMock.Object;
        player.CurrentHealth = hpBefore;
        var mazeMock = new Mock<IMaze>();
        mazeMock.Setup(maze => maze.LogMessages)
            .Returns(new List<string>());
        var randomMock = new Mock<Random>();
        randomMock
            .Setup(x => x.Next(2))
            .Returns(1);
        var maze = mazeMock.Object;

        var mimic = new MimicChest(randomMock.Object);
        mimic.MazeWhereIWasCreated = maze;

        // Act
        var result = mimic.PlayerStepInMe(player);
        // Assert
        Assert.IsTrue(player.CurrentHealth == hpBefore - 1, "We expect that hp will be decreased");
    }
}
