using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;
using MazeConsole.MazeModels.Cells.Interaces;
using MazeConsole.MazeModels.Intefaces;
using Moq;
using NUnit.Framework;

namespace MazeConsole.Tests.MazeModels.Cells;

public class SnakeTest
{
    private Snake _snake;
    private Mock<IMaze> _mazeMock;
    private Mock<IPlayer> _playerMock;
    private List<string> _logMessages;

    [SetUp]
    public void Setup()
    {
        _logMessages = new List<string>();

        _mazeMock = new Mock<IMaze>();
        _mazeMock.Setup(maze => maze.LogMessages)
            .Returns(_logMessages);

        _snake = new Snake { X = 0, Y = 0 };
        _snake.MazeWhereIWasCreated = _mazeMock.Object;

        _playerMock = new Mock<IPlayer>();
        _playerMock.SetupAllProperties();
    }

    [Test]
    public void MoveSnake_ReplacesCellToSnake_WhenSnakeMeetsLessThanThree()
    {
        // Arrange
        var ground1 = new Ground { X = 1, Y = 0 };
        var ground2 = new Ground { X = 2, Y = 0 };
        _mazeMock.Setup(maze => maze.Cells)
            .Returns(new List<IBaseCell> { _snake, ground1, ground2 });
        var random = new Random(42);

        // Act
        _snake.MoveSnake(2, random);

        // Assert
        _mazeMock.Verify(x => x.ReplaceCellToGround(_snake),
            Times.Once, "old nest must be replaced with Ground");
        _mazeMock.Verify(x => x.ReplaceCellToSnake(It.IsAny<IBaseCell>()),
            Times.Once, "a new cell must become Snake");
    }

    [Test]
    public void MoveSnake_LogsScareMessage_WhenSnakeMeetsLessThanThree()
    {
        // Arrange
        var ground1 = new Ground { X = 1, Y = 0 };
        _mazeMock.Setup(maze => maze.Cells)
            .Returns(new List<IBaseCell> { _snake, ground1 });
        var random = new Random(42);

        // Act
        _snake.MoveSnake(0, random);

        // Assert
        Assert.IsTrue(_logMessages.Contains("You've scared the snake! And she has scared you!"),
            "expected a scare message when the snake is relocated");
    }

    [Test]
    public void MoveSnake_DoesNotReplaceCellToSnake_WhenSnakeMeetsThreeOrMore()
    {
        // Arrange
        var ground1 = new Ground { X = 1, Y = 0 };
        _mazeMock.Setup(maze => maze.Cells)
            .Returns(new List<IBaseCell> { _snake, ground1 });
        var random = new Random(42);

        // Act
        _snake.MoveSnake(3, random);

        // Assert
        _mazeMock.Verify(x => x.ReplaceCellToGround(_snake),
            Times.Once, "old nest must be cleared even when the snake leaves for good");
        _mazeMock.Verify(x => x.ReplaceCellToSnake(It.IsAny<IBaseCell>()),
            Times.Never, "snake must not move if she met the player 3+ times");
    }

    [Test]
    public void MoveSnake_LogsLeavingMessage_WhenSnakeMeetsThreeOrMore()
    {
        // Arrange
        var ground1 = new Ground { X = 1, Y = 0 };
        _mazeMock.Setup(maze => maze.Cells)
            .Returns(new List<IBaseCell> { _snake, ground1 });
        var random = new Random(42);

        // Act
        _snake.MoveSnake(5, random);

        // Assert
        Assert.IsTrue(_logMessages.Contains("This is a bad neighborhood. Snake is moving out for good."),
            "expected a leaving message when snake meets are 3 or more");
    }

    [Test]
    public void PlayerStepInMe_IncrementsSnakeMeetsAndReturnsTrue()
    {
        // Arrange
        var player = _playerMock.Object;
        player.SnakeMeets = 0;
        var ground1 = new Ground { X = 1, Y = 0 };
        _mazeMock.Setup(maze => maze.Cells)
            .Returns(new List<IBaseCell> { _snake, ground1 });

        // Act
        var result = _snake.PlayerStepInMe(player);

        // Assert
        Assert.IsTrue(player.SnakeMeets == 1, "SnakeMeets must be incremented after a step");
        Assert.IsTrue(result, "player must be able to step on Snake");
    }
}