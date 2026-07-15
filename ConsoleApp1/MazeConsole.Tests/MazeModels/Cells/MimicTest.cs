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

        var maze = mazeMock.Object;

        var mimic = new MimicChest();
        mimic.MazeWhereIWasCreated = maze;

        // Act
        var result = mimic.PlayerStepInMe(player);

        // Assert
        Assert.IsTrue(result, "We expect that player can step on Mimic");
    }
    [TestCase(3, 3)]
    [TestCase(2, 2)]
    public void PlayerStepInMe_HealthIsDrainingOrCoinIsIncreased(int coinBefore, int hpBefore)
    {
        // Preparation
        var playerMock = new Mock<IPlayer>();
        playerMock.SetupAllProperties();
        var player = playerMock.Object;
        player.Coin = coinBefore;
        player.CurrentHealth = hpBefore;
        var mazeMock = new Mock<IMaze>();
        mazeMock.Setup(maze => maze.LogMessages)
            .Returns(new List<string>());

        var maze = mazeMock.Object;

        var mimic = new MimicChest();
        mimic.MazeWhereIWasCreated = maze;

        // Act
        var result = mimic.PlayerStepInMe(player);
        // Assert
        Assert.IsTrue(player.Coin == coinBefore + 1 || player.CurrentHealth == hpBefore - 1, "We expect that either hp will be reduced, or coin will be increased");
    }
}
