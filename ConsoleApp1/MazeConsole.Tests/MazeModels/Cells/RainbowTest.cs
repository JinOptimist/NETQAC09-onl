using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;
using Moq;
using NUnit.Framework;

namespace MazeConsole.Tests.MazeModels.Cells;

public class RainbowTest
{
    [Test]
    public void PlayerStepInMe_CanStepToRainbow()
    {
        // Preparation
        var playerMock = new Mock<IPlayer>();
        var player = playerMock.Object;

        var rainbow = new Rainbow();

        // Act
        var result = rainbow.PlayerStepInMe(player);

        // Assert
        Assert.IsTrue(result, "We expect that player can step on Rainbow");
    }

    [Test]
    public void MySymbol_RainbowSymbolIsR()
    {
        // Preparation
        var rainbow = new Rainbow();

        // Act
        var result = rainbow.MySymbol;

        // Assert
        Assert.IsTrue(result == 'R', "We expect that Rainbow symbol is R");
    }
}