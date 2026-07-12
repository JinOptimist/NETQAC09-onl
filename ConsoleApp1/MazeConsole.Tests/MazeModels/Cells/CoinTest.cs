using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;
using Moq;
using NUnit.Framework;

namespace MazeConsole.Tests.MazeModels.Cells;

public class CoinTest
{
    [Test]
    public void PlayerStepInMe_CanStepToCoin()
    {
        // Preparation
        var playerMock = new Mock<IPlayer>();  // moq, stub
        playerMock.SetupAllProperties();
        var player = playerMock.Object;
        var mazeMock = new Mock<IMaze>();      // moq, stub

        mazeMock.Setup(maze => maze.LogMessages)
            .Returns(new List<string>());

        var maze = mazeMock.Object;

        var coin = new Coin();
        coin.MazeWhereIWasCreated = maze;

        // Act
        var result = coin.PlayerStepInMe(player);

        // Assert
        Assert.IsTrue(result, "We expect that player can step on Coin");
    }


    [Test]
    public void PlayerStepInMe_ThrowExceptionOnPoorPlayer()
    {
        // Preparation
        var playerMock = new Mock<IPlayer>();  // moq, stub
        playerMock.SetupAllProperties();
        playerMock.Setup(player => player.Coin)
            .Returns(-5);
        var player = playerMock.Object;
        var mazeMock = new Mock<IMaze>();      // moq, stub

        mazeMock.Setup(maze => maze.LogMessages)
            .Returns(new List<string>());

        var maze = mazeMock.Object;

        var coin = new Coin();
        coin.MazeWhereIWasCreated = maze;

        // Act
        // Assert
        Assert.Throws<Exception>(() => coin.PlayerStepInMe(player),
            "we have to down if player has negative coin");
    }

    [Test]
    public void PlayerStepInMe_WeReplaceCellWhenCoinHasToDisapeare()
    {
        // Preparation
        var playerMock = new Mock<IPlayer>();  // moq, stub
        playerMock.SetupAllProperties();
        var player = playerMock.Object;
        var mazeMock = new Mock<IMaze>();      // moq, stub

        mazeMock.Setup(maze => maze.LogMessages)
            .Returns(new List<string>());

        var maze = mazeMock.Object;

        var coin = new Coin();
        coin.MazeWhereIWasCreated = maze;

        // Act
        for (int i = 0; i < Coin.COINT_COUNT_INITIAL; i++)
        {
            coin.PlayerStepInMe(player);
        }

        // Assert
        mazeMock.Verify(x => x.ReplaceCellToGround(coin), Times.Once, "We think that cell must be replaced");
    }
}
