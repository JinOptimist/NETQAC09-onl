using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;
using Moq;
using NUnit.Framework;

namespace MazeConsole.Tests.MazeModels.Cells;
public class WallTest
{
    [Test]
    public void PlayerStepInMe_ShouldReturnFalse()
    {
        // подготовка
        var wall = new Wall();
        var playerMock = new Mock<IPlayer>();  
        var player = playerMock.Object;

        // действие
        var result = wall.PlayerStepInMe(player);

        // проверка
        Assert.IsFalse(result, "We expect that player cannot step on Wall");
    }

    [Test]
public void MySymbol_ShouldBeHash()
    { //подготовка
    var wall = new Wall();
    // проверка
    Assert.AreEqual('#', wall.MySymbol);
    }

}