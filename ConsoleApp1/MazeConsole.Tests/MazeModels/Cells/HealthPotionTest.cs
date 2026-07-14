using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;
using MazeConsole.MazeModels.Cells.Interaces;
using MazeConsole.MazeModels.Intefaces;
using Moq;
using NUnit.Framework;

namespace MazeConsole.Tests.MazeModels.Cells
{
    internal class HealthPotionTest
    {
        [Test]
        public void PlayerStepInMe_CanStepInHealthPotionCell()
        {
            // Preparation
            var playerMock = new Mock<IPlayer>();
            playerMock.SetupAllProperties();
            var player = playerMock.Object;

            var mazeMock = new Mock<IMaze>();
            mazeMock.Setup(m => m.Cells).Returns(new List<IBaseCell> 
            { 
                new Ground(), new Ground(), new Ground() 
            });
            mazeMock.Setup(m => m.Random).Returns(new Random());

            var healthPotion = new HealthPotion();
            healthPotion.MazeWhereIWasCreated = mazeMock.Object;

            // Act
            var result = healthPotion.PlayerStepInMe(player);

            // Assert
            Assert.IsTrue(result, "Health potion is steppable");
        }
    }
}
