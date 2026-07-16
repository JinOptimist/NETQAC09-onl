using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;
using MazeConsole.MazeModels.Cells.Interaces;
using MazeConsole.MazeModels.Intefaces;
using Moq;
using NUnit.Framework;

namespace MazeConsole.Tests.MazeModels.Cells;

public class TreeTest
{
    [Test]
    public void Durability_HasInitialValueFive()
    {
        // Preparation
        var tree = new Tree();

        // Act
        var currentDurability = tree.Durability;

        // Assert
        Assert.IsTrue(currentDurability == 5, "We expect that initial durability is 5");
    }

    [Test]
    public void MySymbol_ReturnsSymbolW()
    {
        // Preparation
        var tree = new Tree();

        // Act
        var symbol = tree.MySymbol;

        // Assert
        Assert.IsTrue(symbol == 'W', "We expect that tree symbol equals W");

    }

    [Test]
    public void PlayerStepInMe_ThrowsNotImplementedExceptionIfGroundIsNotAvailable()
    {
        // Preparation
        var mazeMock = new Mock<IMaze>();

        var playerMock = new Mock<IPlayer>();
        var player = playerMock.Object;

        mazeMock.Setup(maze => maze.Cells)
           .Returns(new List<IBaseCell>());

        var maze = mazeMock.Object;

        var tree = new Tree();
        tree.MazeWhereIWasCreated = maze;

        // Act


        // Assert
        Assert.Throws<NotImplementedException>(() => tree.PlayerStepInMe(player),
            "We expect NotImplementedException when there are no Ground cells ");

    }
}