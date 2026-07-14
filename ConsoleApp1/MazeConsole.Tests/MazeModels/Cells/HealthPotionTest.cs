using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;
using MazeConsole.MazeModels.Intefaces;
using Moq;
using NUnit.Framework;

namespace MazeConsole.Tests.MazeModels.Cells
{
    internal class HealthPotionTest
    {
        [Test] //is cell steppable

        public void PlayerStepInMe_CanStepInHealthPotionCell()
        {
            // Preparation
            var playerMock = new Mock<IPlayer>();  // moq, stub
            playerMock.SetupAllProperties();
            var player = playerMock.Object;
            var mazeMock = new Mock<IMaze>();      // moq, stub

            mazeMock.Setup(maze => maze.LogMessages)
                .Returns(new List<string>());

            var maze = mazeMock.Object;

            var healthPotion = new HealthPotion();
            healthPotion.MazeWhereIWasCreated = maze;

            // Act
            var result = healthPotion.PlayerStepInMe(player);

            // Assert
            Assert.IsTrue(result, "Health potion is steppable");
        }
    }
}
