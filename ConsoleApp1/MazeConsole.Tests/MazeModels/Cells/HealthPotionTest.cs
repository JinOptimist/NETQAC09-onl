using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;
using MazeConsole.MazeModels.Cells.Interaces;
using MazeConsole.MazeModels.Intefaces;
using Moq;
using NUnit.Framework;

namespace MazeConsole.Tests.MazeModels.Cells
{
    public class HealthPotionTest
    {
        [Test]
        public void PlayerStepInMe_CanStepInHealthPotionCell() //Тест на наступабельность
        {
            // Preparation
            var playerMock = new Mock<IPlayer>(); // создается мок игрока
            playerMock.SetupAllProperties();
            var player = playerMock.Object;

            var mazeMock = new Mock<IMaze>(); //создается мок лабиринта

            var groundCell = new Ground { X = 0, Y = 0 }; //в лабиринте размещаем землю, т.к. зелье спавнится только на ней
            mazeMock.Setup(maze => maze.Cells)
                .Returns(new List<IBaseCell> { groundCell });

            var randomMock = new Mock<Random>(); //видимо, из-за рандома в HealthPotion, нужно замокать рандом, чтобы тест был предсказуемым
            randomMock.Setup(r => r.Next(It.IsAny<int>())).Returns(0);
            mazeMock.Setup(maze => maze.Random)
                .Returns(randomMock.Object);

            var maze = mazeMock.Object; //получаем объект лабиринта из мока

            var healthPotion = new HealthPotion(); //создаем зелье и кладем в мок лабиринта
            healthPotion.MazeWhereIWasCreated = maze; 

            // Act
            var result = healthPotion.PlayerStepInMe(player); // вызываем метод PlayerStepInMe у зелья, передавая в него игрока

            // Assert
            Assert.IsTrue(result, "We expect that player can step on Coin"); // проверяем, что игрок может наступить на зелье
        }

        [Test]
        public void PlayerStepInMe_HealthPotionReplacedWithGround() //Тест на замену зелья на землю при наступании
        {
            // Preparation
            var playerMock = new Mock<IPlayer>();
            playerMock.SetupAllProperties();
            var player = playerMock.Object;

            var mazeMock = new Mock<IMaze>();

            var groundCell = new Ground { X = 0, Y = 0 };
            mazeMock.Setup(maze => maze.Cells)
                .Returns(new List<IBaseCell> { groundCell });

            var randomMock = new Mock<Random>();
            randomMock.Setup(r => r.Next(It.IsAny<int>())).Returns(0);
            mazeMock.Setup(maze => maze.Random)
                .Returns(randomMock.Object);

            var maze = mazeMock.Object;

            var healthPotion = new HealthPotion();
            healthPotion.MazeWhereIWasCreated = maze;

            // Act
            healthPotion.PlayerStepInMe(player); 

            // Assert
            mazeMock.Verify(maze => maze.ReplaceCellToGround(healthPotion), Times.Once,  //проверяем, что метод ReplaceCellToGround был вызван
                "Health potion should be replaced with a ground cell when player steps on it");
        }

        [TestCase(0, 1)]
        public void PlayerStepInMe_PlayerHealthPotionCountIncrementsCorrectly(int initialHealthPotions, int expectedHealthPotions) //Тест на увеличение количества зелий у игрока
        {
            // Preparation
            var playerMock = new Mock<IPlayer>(); 
            playerMock.SetupAllProperties();
            var player = playerMock.Object;

            player.HealthPotion = initialHealthPotions;

            var mazeMock = new Mock<IMaze>();

            var groundCell = new Ground { X = 0, Y = 0 };
            mazeMock.Setup(maze => maze.Cells)
                .Returns(new List<IBaseCell> { groundCell });

            var randomMock = new Mock<Random>();
            randomMock.Setup(r => r.Next(It.IsAny<int>())).Returns(0);
            mazeMock.Setup(maze => maze.Random)
                .Returns(randomMock.Object);

            var maze = mazeMock.Object;

            var healthPotion = new HealthPotion();
            healthPotion.MazeWhereIWasCreated = maze;

            // Act
            healthPotion.PlayerStepInMe(player);

            // Assert
            Assert.AreEqual(expectedHealthPotions, player.HealthPotion, // проверяем, что количество зелий у игрока увеличилось на 1
                $"Player should have {expectedHealthPotions} health potions after stepping on the health potion cell");
        }

        [TestCase(1)]
        [TestCase(2)]
        public void PlayerStepInMe_ThrowsExceptionWhenPlayerAlreadyHasMaxHealthPotions(int initialHealthPotions) //Тест на эксепшн, когда у игрока уже есть максимум зелий
        {
            // Preparation
            var playerMock = new Mock<IPlayer>(); 
            playerMock.SetupAllProperties();
            var player = playerMock.Object;

            player.HealthPotion = initialHealthPotions;

            var mazeMock = new Mock<IMaze>();

            var groundCell = new Ground { X = 0, Y = 0 };
            mazeMock.Setup(maze => maze.Cells)
                .Returns(new List<IBaseCell> { groundCell });

            var randomMock = new Mock<Random>();
            randomMock.Setup(r => r.Next(It.IsAny<int>())).Returns(0);
            mazeMock.Setup(maze => maze.Random)
                .Returns(randomMock.Object);

            var maze = mazeMock.Object;

            var healthPotion = new HealthPotion();
            healthPotion.MazeWhereIWasCreated = maze;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => healthPotion.PlayerStepInMe(player)); //проверяем, что есть эксепшн
        }

        [Test]
        public void PlayerStepInMe_NewHealthPotionReplacedToRandomGround() //Тест на создание нового зелья с координатами случайной земли
        {
            // Preparation
            var playerMock = new Mock<IPlayer>(); 
            playerMock.SetupAllProperties();
            var player = playerMock.Object;

            var mazeMock = new Mock<IMaze>();

            var groundCell1 = new Ground { X = 2, Y = 3 }; //создаем первую ячейку земли 
            var groundCell2 = new Ground { X = 5, Y = 7 }; //создаем вторую ячейку земли (куда упадет новое зелье)
            mazeMock.Setup(maze => maze.Cells)
                .Returns(new List<IBaseCell> { groundCell1, groundCell2 });

            var randomMock = new Mock<Random>();
            randomMock.Setup(r => r.Next(It.IsAny<int>())).Returns(1); //кладем в groundCell2
            mazeMock.Setup(maze => maze.Random)
                .Returns(randomMock.Object);

            var maze = mazeMock.Object;

            var healthPotion = new HealthPotion();
            healthPotion.MazeWhereIWasCreated = maze;

            IBaseCell capturedCell = null; //Cоздаем коробочку для сохранения нового зелья
            mazeMock.Setup(m => m.ReplaceToCell(It.IsAny<IBaseCell>())) //настраиваем перехват вызова ReplaceToCell. Когда  в тесте будет вызван ReplaceToCell, то мок перехватит это зелье
                .Callback<IBaseCell>(cell => capturedCell = cell); //а с помощью коллбека сохранит это зелье в переменную capturedCell, чтобы потом проверить его наличие и координаты

            // Act
            healthPotion.PlayerStepInMe(player);

            // Assert 
            mazeMock.Verify(maze => maze.ReplaceToCell(It.IsAny<IBaseCell>()), Times.Once, //проверяем, что ReplaceToCell был вызван
                "ReplaceToCell should be called once");

            Assert.That(capturedCell, Is.Not.Null, "ReplaceToCell should have been called with a cell"); //проверяем, что зелье было передано
            Assert.That(capturedCell, Is.InstanceOf<HealthPotion>(), "The cell should be a HealthPotion");

            var newHealthPotion = capturedCell as HealthPotion; //приводим к HealthPotion
            Assert.That(newHealthPotion.X, Is.EqualTo(groundCell2.X), "New health potion X matches the selected ground"); //проверяем X координату
            Assert.That(newHealthPotion.Y, Is.EqualTo(groundCell2.Y), "New health potion Y matched the selected ground"); //проверяем Y координату
        }

        [Test]
        public void HealthPotion_ThrowsExceptionWhenNoGround()
        {
            // Preparation
            var playerMock = new Mock<IPlayer>();
            playerMock.SetupAllProperties();
            var player = playerMock.Object;

            var mazeMock = new Mock<IMaze>();

            var wallCell = new Wall { X = 0, Y = 0 }; //добавляем только клетку стены, чтобы проверить, что зелье не может быть создано
            mazeMock.Setup(maze => maze.Cells)
                .Returns(new List<IBaseCell> { wallCell });

            var randomMock = new Mock<Random>();
            randomMock.Setup(r => r.Next(It.IsAny<int>())).Returns(0);
            mazeMock.Setup(maze => maze.Random)
                .Returns(randomMock.Object);

            var maze = mazeMock.Object;

            var healthPotion = new HealthPotion();
            healthPotion.MazeWhereIWasCreated = maze;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => healthPotion.PlayerStepInMe(player));
        }
    }
}
