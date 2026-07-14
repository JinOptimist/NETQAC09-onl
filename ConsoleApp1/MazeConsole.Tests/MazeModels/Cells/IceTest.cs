using MazeConsole.MazeExceptions;
using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;
using MazeConsole.MazeModels.Intefaces;
using Moq;
using NUnit.Framework;

namespace MazeConsole.Tests.MazeModels.Cells;

public class IceTest
{
    // Хелпер: создаёт лёд с замоканным лабиринтом и списком ячеек
    private static (Ice ice, Mock<IMaze> mazeMock, List<ICell> cells) CreateIce(int x, int y)
    {
        var cells = new List<ICell>();

        var mazeMock = new Mock<IMaze>();
        mazeMock.Setup(m => m.Cells).Returns(cells);
        mazeMock.Setup(m => m.LogMessages).Returns(new List<string>());
        mazeMock.Setup(m => m.Seed).Returns(12345);

        var ice = new Ice { X = x, Y = y };
        ice.MazeWhereIWasCreated = mazeMock.Object;
        cells.Add(ice);

        return (ice, mazeMock, cells);
    }

    private static Mock<IPlayer> CreatePlayer(int x, int y, int sand = 0)
    {
        var playerMock = new Mock<IPlayer>();
        playerMock.SetupAllProperties();
        var player = playerMock.Object;
        player.X = x;
        player.Y = y;
        player.Sand = sand;
        return playerMock;
    }

    [Test]
    public void PlayerStepInMe_WithSand_ReplacesIceWithDirtAndReturnsTrue()
    {
        // Preparation
        var (ice, _, cells) = CreateIce(5, 5);
        var playerMock = CreatePlayer(4, 5, sand: 3);
        var player = playerMock.Object;

        // Act
        var result = ice.PlayerStepInMe(player);

        // Assert
        Assert.IsTrue(result, "Со песком игрок может встать на лёд (лёд превращается в грязь)");
        Assert.AreEqual(2, player.Sand, "Количество песка должно уменьшиться на 1");
        Assert.IsInstanceOf<Dirt>(cells[0], "Лёд должен быть заменён на Dirt");
    }

    [Test]
    public void PlayerStepInMe_SlidesToSteppableCell_ReturnsFalseAndMovesPlayer()
    {
        // Preparation
        var (ice, _, cells) = CreateIce(5, 5);
        // игрок идёт слева (4,5) -> направление (+1,0) -> скользит на (6,5)
        var playerMock = CreatePlayer(4, 5);
        var player = playerMock.Object;

        // ячейка за льдом - наступабельная
        var nextCellMock = new Mock<ICell>();
        nextCellMock.SetupAllProperties();
        nextCellMock.Object.X = 6;
        nextCellMock.Object.Y = 5;
        nextCellMock.Setup(c => c.PlayerStepInMe(player)).Returns(true);
        cells.Add(nextCellMock.Object);

        // Act
        var result = ice.PlayerStepInMe(player);

        // Assert
        Assert.IsFalse(result, "Ice сам двигает игрока, поэтому контроллеру возвращает false");
        Assert.AreEqual(6, player.X, "Игрок должен проскользить на клетку за льдом");
        Assert.AreEqual(5, player.Y);
    }

    [Test]
    public void PlayerStepInMe_NextCellNotSteppable_PlayerStaysOnIce()
    {
        // Preparation
        var (ice, _, cells) = CreateIce(5, 5);
        var playerMock = CreatePlayer(4, 5);
        var player = playerMock.Object;

        // ячейка за льдом существует, но НЕ наступабельная
        var nextCellMock = new Mock<ICell>();
        nextCellMock.SetupAllProperties();
        nextCellMock.Object.X = 6;
        nextCellMock.Object.Y = 5;
        nextCellMock.Setup(c => c.PlayerStepInMe(player)).Returns(false);
        cells.Add(nextCellMock.Object);

        // Act
        var result = ice.PlayerStepInMe(player);

        // Assert
        Assert.IsFalse(result);
        Assert.AreEqual(5, player.X, "Игрок остаётся на клетке льда");
        Assert.AreEqual(5, player.Y);
    }

    [Test]
    public void PlayerStepInMe_NextCellNull_DoesNotThrowAndPlayerStays()
    {
        // Preparation
        var (ice, _, _) = CreateIce(5, 5);
        // ячейки за льдом (6,5) в списке нет -> nextCell == null
        var playerMock = CreatePlayer(4, 5);
        var player = playerMock.Object;

        // Act
        var result = ice.PlayerStepInMe(player);

        // Assert
        // корректная логика оставляет игрока на льду, исключение не бросается
        Assert.IsFalse(result);
        Assert.AreEqual(5, player.X);
        Assert.AreEqual(5, player.Y);
    }

    [Test]
    public void PlayerStepInMe_WithSand_DoesNotSlide()
    {
        // Preparation
        var (ice, _, _) = CreateIce(5, 5);
        var playerMock = CreatePlayer(4, 5, sand: 1);
        var player = playerMock.Object;

        // Act
        ice.PlayerStepInMe(player);

        // Assert - игрок не скользит, координаты не меняются логикой льда
        Assert.AreEqual(4, player.X);
        Assert.AreEqual(5, player.Y);
    }

    [Test]
    public void PlayerStepInMe_WithSand_WritesDirtLog_NoException()
    {
        // Preparation
        var (ice, mazeMock, _) = CreateIce(5, 5);
        var playerMock = CreatePlayer(4, 5, sand: 1);
        var player = playerMock.Object;

        // Act & Assert
        Assert.DoesNotThrow(() => ice.PlayerStepInMe(player));
    }
}