using System.ComponentModel.DataAnnotations;
using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;
using MazeConsole.MazeModels.Intefaces;
using Moq;
using NUnit.Framework;

namespace MazeConsole.Tests.MazeModels.Cells;

public class PileOfSandTest
{
    [Test]
    public void PlayerStepInMe_CanStepToPile()
    {
        // Preparation
        var playerMock = new Mock<IPlayer>();
        playerMock.SetupAllProperties();
        var player = playerMock.Object;
        var mazeMock = new Mock<IMaze>();

        mazeMock.Setup(maze => maze.LogMessages)
            .Returns(new List<string>());

        var maze = mazeMock.Object;

        var pile = new PileOfSand();
        pile.MazeWhereIWasCreated = maze;

        // Act
        var result = pile.PlayerStepInMe(player);

        // Assert
        Assert.IsTrue(result, "We expect that player can step on Pile of sand");
    }

    [Test]
    [TestCase(0, 1)]
    [TestCase(3, 3)]
    public void PlayerStepInMe_AddSand(int sandBefore, int sandAfter)
    {
        // Preparation
        var pile = new PileOfSand();

        var playerMock = new Mock<IPlayer>();
        playerMock.SetupProperty(x => x.Sand);
        var player = playerMock.Object;
        player.Sand = sandBefore;

        var mazeMock = new Mock<IMaze>();
        mazeMock.Setup(maze => maze.LogMessages)
            .Returns(new List<string>());
        pile.MazeWhereIWasCreated = mazeMock.Object;

        // Act
        pile.PlayerStepInMe(player);

        // Assert
        Assert.IsTrue(player.Sand == sandAfter);
    }
}
