using System.Collections.Generic;
using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;
using MazeConsole.MazeModels.Intefaces; 
using Moq;
using NUnit.Framework;

namespace MazeConsole.Tests.MazeModels.Cells;

public class FlowerTest
{
    /// <summary>
    /// метод для создания заглушки игрока
    /// </summary>
    /// <param name="initialFlowers"></param>
    /// <returns></returns>
    private IPlayer CreatePlayerMock(int initialFlowers = 0)
    {
        var playerMock = new Mock<IPlayer>();
        playerMock.SetupAllProperties();
        
        var player = playerMock.Object;
        
        // задаем начальное количество цветов
        player.Flowers = initialFlowers; 
        
        return player;
    }

    /// <summary>
    /// метод для создания заглушки лабиринта
    /// </summary>
    /// <returns></returns>
    private IMaze CreateMazeMock()
    {
        var mazeMock = new Mock<IMaze>();
        mazeMock.Setup(maze => maze.LogMessages).Returns(new List<string>());
        return mazeMock.Object;
    }
    
    /// <summary>
    /// проверяем, что PlayerStepInMe возвращает true
    /// </summary>
    [Test]
    public void PlayerStepInMe_ShouldReturnTrue()
    {
        // arrange
        var player = CreatePlayerMock(initialFlowers: 0);
        var maze = CreateMazeMock();

        var flower = new Flower { MazeWhereIWasCreated = maze };

        // act
        var result = flower.PlayerStepInMe(player);

        // assert
        Assert.IsTrue(result, "Ожидаем, что вернулось true");
    }

    /// <summary>
    /// проверяем, что количество цветочков увеличивается при попадании на клетку
    /// </summary>
    [Test]
    public void PlayerStepInMe_FlowersCountCanBeBigger()
    {
        // arrange
        var player = CreatePlayerMock(initialFlowers: 1);
        var maze = CreateMazeMock();

        var flower = new Flower { MazeWhereIWasCreated = maze };

        // act
        flower.PlayerStepInMe(player);

        // assert
        Assert.IsTrue(player.Flowers == 2, "Ожидаем, что количество цветов увеличится с 1 до 2");
    }
    
    /// <summary>
    /// проверяем, что нельзя иметь больше 3 цветочков
    /// </summary>
    [Test]
    public void PlayerStepInMe_FlowersCountCantBe4()
    {
        // arrange
        var player = CreatePlayerMock(initialFlowers: 3);
        var maze = CreateMazeMock();

        var flower = new Flower { MazeWhereIWasCreated = maze };

        // act
        flower.PlayerStepInMe(player);

        // assert
        Assert.IsTrue(player.Flowers == 3, "Ожидаем, что количество цветов не стало 4");
    }
}