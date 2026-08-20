using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;
using MazeConsole.MazeModels.Cells.Interaces;
using MazeConsole.MazeModels.Intefaces;
using Moq;
using NUnit.Framework;

namespace MazeConsole.Tests.MazeModels.Cells;

public class ThiefTest
{

    private Thief CreateThief(out Mock<IMaze> mazeMock)
    {
        mazeMock = new Mock<IMaze>();
        mazeMock.Setup(maze => maze.LogMessages)
            .Returns(new List<string>());

        var thief = new Thief();
        thief.MazeWhereIWasCreated = mazeMock.Object;
        return thief;
    }

    [Test]
    public void PlayerStepInMe_WhenPlayerHasCoins_SetsCoinsToZero()
    {
        // Preparation
        var thief = CreateThief(out var mazeMock);

        var playerMock = new Mock<IPlayer>();
        playerMock.SetupProperty(x => x.Coin);
        var player = playerMock.Object;
        player.Coin = 10;

        // Act
        thief.PlayerStepInMe(player);

        // Assertю
        Assert.IsTrue(player.Coin == 0, "Thief must set player's coins to zero");
    }

    [Test]
    public void PlayerStepInMe_WhenPlayerHasCoins_ReturnsTrue()
    {
        var thief = CreateThief(out var mazeMock);

        var playerMock = new Mock<IPlayer>();
        playerMock.SetupProperty(x => x.Coin);
        var player = playerMock.Object;
        player.Coin = 10;

        var result = thief.PlayerStepInMe(player);

        Assert.IsTrue(result, "Player can step on Thief");
    }

    [Test]
    public void PlayerStepInMe_WhenPlayerHasCoins_ReplacesThiefWithGround()
    {
        var thief = CreateThief(out var mazeMock);

        var playerMock = new Mock<IPlayer>();
        playerMock.SetupProperty(x => x.Coin);
        var player = playerMock.Object;
        player.Coin = 10;

        thief.PlayerStepInMe(player);

        mazeMock.Verify(x => x.ReplaceCellToGround(thief),
            Times.Once,
            "After robbery the thief cell must be replaced with ground");
    }

    [Test]
    public void PlayerStepInMe_WhenPlayerHasZeroCoins_DoesNotReplaceCell()
    {
        var thief = CreateThief(out var mazeMock);

        var playerMock = new Mock<IPlayer>();
        playerMock.SetupAllProperties();
        var player = playerMock.Object;
        player.Coin = 0;

        thief.PlayerStepInMe(player);

        mazeMock.Verify(x => x.ReplaceCellToGround(It.IsAny<IBaseCell>()),
            Times.Never,
            "With zero coins thief is not activated, cell stays");
    }

    [Test]
    public void PlayerStepInMe_WhenPlayerHasZeroCoins_ReturnsTrue()
    {
        var thief = CreateThief(out var mazeMock);

        var playerMock = new Mock<IPlayer>();
        playerMock.SetupAllProperties();
        var player = playerMock.Object;
        player.Coin = 0;

        var result = thief.PlayerStepInMe(player);

        Assert.IsTrue(result, "Player can step on Thief even when poor");
    }

    [Test]
    public void PlayerStepInMe_WhenPlayerHasZeroCoins_AddsTooPoorLog()
    {
        var thief = CreateThief(out var mazeMock);

        var playerMock = new Mock<IPlayer>();
        playerMock.SetupAllProperties();
        var player = playerMock.Object;
        player.Coin = 0;

        thief.PlayerStepInMe(player);

        Assert.IsTrue(
            mazeMock.Object.LogMessages.Exists(m => m.Contains("too poor")),
            "Poor player log message must be added");
    }

    [Test]
    public void PlayerStepInMe_ThrowExceptionOnNegativeCoins()
    {
        var thief = CreateThief(out var mazeMock);

        var playerMock = new Mock<IPlayer>();
        playerMock.SetupProperty(x => x.Coin);
        var player = playerMock.Object;
        player.Coin = -5;

        Assert.Throws<Exception>(() => thief.PlayerStepInMe(player),
            "Negative coins is invalid state, must throw");
    }
}