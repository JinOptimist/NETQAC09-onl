using MazeConsole.MazeExceptions;
using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;
using MazeConsole.MazeModels.Cells.Interaces;
using MazeConsole.MazeModels.Intefaces;
using Moq;
using NUnit.Framework;

namespace MazeConsole.Tests.MazeModels.Cells;

public class IceTest
{
    private List<IBaseCell> _cells;
    private Mock<IMaze> _mazeMock;
    private Mock<IPlayer> _playerMock;
    private IPlayer _player;
    private Ice _ice;

    [SetUp]
    public void SetUp()
    {
        _cells = new List<IBaseCell>();

        _mazeMock = new Mock<IMaze>();
        _mazeMock.Setup(m => m.Cells).Returns(_cells);
        _mazeMock.Setup(m => m.LogMessages).Returns(new List<string>());
        _mazeMock.Setup(m => m.Seed).Returns(12345);

        _ice = new Ice { X = 5, Y = 5 };
        _ice.MazeWhereIWasCreated = _mazeMock.Object;
        _cells.Add(_ice);

        _playerMock = new Mock<IPlayer>();
        _playerMock.SetupAllProperties();
        _player = _playerMock.Object;
        // игрок идёт слева: направление (+1,0) -> скользит на (6,5)
        _player.X = 4;
        _player.Y = 5;
        _player.Sand = 0;
    }

    // Добавляет ячейку за льдом на (6,5) с заданной "наступабельностью"
    private Mock<IBaseCell> AddNextCell(bool isSteppable)
    {
        var nextCellMock = new Mock<IBaseCell>();
        nextCellMock.SetupAllProperties();
        nextCellMock.Object.X = 6;
        nextCellMock.Object.Y = 5;
        nextCellMock.Setup(c => c.PlayerStepInMe(_player)).Returns(isSteppable);
        _cells.Add(nextCellMock.Object);
        return nextCellMock;
    }

    [Test]
    [TestCase(3, 2)]
    [TestCase(1, 0)]
    public void PlayerStepInMe_WithSand_ReplacesIceWithDirtAndReturnsTrue(int sandBefore, int sandAfter)
    {
        // Preparation
        _player.Sand = sandBefore;

        // Act
        var result = _ice.PlayerStepInMe(_player);

        // Assert
        Assert.IsTrue(result, "Со песком игрок может встать на лёд (лёд превращается в грязь)");
        Assert.AreEqual(sandAfter, _player.Sand, "Количество песка должно уменьшиться на 1");
        Assert.IsInstanceOf<Dirt>(_cells[0], "Лёд должен быть заменён на Dirt");
    }

    [Test]
    public void PlayerStepInMe_SlidesToSteppableCell_ReturnsFalseAndMovesPlayer()
    {
        // Preparation
        AddNextCell(isSteppable: true);

        // Act
        var result = _ice.PlayerStepInMe(_player);

        // Assert
        Assert.IsFalse(result, "Ice сам двигает игрока, поэтому контроллеру возвращает false");
        Assert.AreEqual(6, _player.X, "Игрок должен проскользить на клетку за льдом");
        Assert.AreEqual(5, _player.Y);
    }

    [Test]
    public void PlayerStepInMe_NextCellNotSteppable_PlayerStaysOnIce()
    {
        // Preparation
        AddNextCell(isSteppable: false);

        // Act
        var result = _ice.PlayerStepInMe(_player);

        // Assert
        Assert.IsFalse(result);
        Assert.AreEqual(5, _player.X, "Игрок остаётся на клетке льда");
        Assert.AreEqual(5, _player.Y);
    }

    [Test]
    public void PlayerStepInMe_NextCellNull_DoesNotThrowAndPlayerStays()
    {
        // Preparation
        // ячейку за льдом не добавляем -> nextCell == null

        // Act
        var result = _ice.PlayerStepInMe(_player);

        // Assert
        Assert.IsFalse(result);
        Assert.AreEqual(5, _player.X);
        Assert.AreEqual(5, _player.Y);
    }

    [Test]
    public void PlayerStepInMe_WithSand_DoesNotSlide()
    {
        // Preparation
        _player.Sand = 1;

        // Act
        _ice.PlayerStepInMe(_player);

        // Assert - игрок не скользит, координаты меняет только логика песка (не трогает X/Y)
        Assert.AreEqual(4, _player.X);
        Assert.AreEqual(5, _player.Y);
    }
}