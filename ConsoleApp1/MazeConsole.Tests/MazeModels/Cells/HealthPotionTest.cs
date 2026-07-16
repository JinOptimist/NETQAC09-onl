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
        private Mock<IPlayer> _playerMock;
        private Mock<IMaze> _mazeMock;
        private Mock<Random> _randomMock;
        private IPlayer _player;
        private IMaze _maze;
        private HealthPotion _healthPotion;

        [SetUp]
        public void Setup()
        {
            // Setup Player
            _playerMock = new Mock<IPlayer>();
            _playerMock.SetupAllProperties();
            _player = _playerMock.Object;

            // Setup Maze and Random
            _mazeMock = new Mock<IMaze>();
            _randomMock = new Mock<Random>();
            _randomMock.Setup(r => r.Next(It.IsAny<int>())).Returns(0);
            _mazeMock.Setup(maze => maze.Random)
                .Returns(_randomMock.Object);

            _maze = _mazeMock.Object;

            // Setup HealthPotion
            _healthPotion = new HealthPotion();
            _healthPotion.MazeWhereIWasCreated = _maze;
        }

        private void SetupMazeCells(params IBaseCell[] cells)
        {
            _mazeMock.Setup(maze => maze.Cells)
                .Returns(new List<IBaseCell>(cells));
        }

        private void SetupRandomNext(int returnValue)
        {
            _randomMock.Setup(r => r.Next(It.IsAny<int>())).Returns(returnValue);
        }

        [Test]
        public void PlayerStepInMe_CanStepInHealthPotionCell() //Тест на наступабельность
        {
            // Preparation
            var groundCell = new Ground { X = 0, Y = 0 }; //в лабиринте размещаем землю, т.к. зелье спавнится только на ней
            SetupMazeCells(groundCell);

            // Act
            var result = _healthPotion.PlayerStepInMe(_player); // вызываем метод PlayerStepInMe у зелья, передавая в него игрока

            // Assert
            Assert.IsTrue(result, "We expect that player can step on Coin"); // проверяем, что игрок может наступить на зелье
        }

        [Test]
        public void PlayerStepInMe_HealthPotionReplacedWithGround() //Тест на замену зелья на землю при наступании
        {
            // Preparation
            var groundCell = new Ground { X = 0, Y = 0 };
            SetupMazeCells(groundCell);

            // Act
            _healthPotion.PlayerStepInMe(_player);

            // Assert
            _mazeMock.Verify(maze => maze.ReplaceCellToGround(_healthPotion), Times.Once,  //проверяем, что метод ReplaceCellToGround был вызван
                "Health potion should be replaced with a ground cell when player steps on it");
        }

        [TestCase(0, 1)]
        public void PlayerStepInMe_PlayerHealthPotionCountIncrementsCorrectly(int initialHealthPotions, int expectedHealthPotions) //Тест на увеличение количества зелий у игрока
        {
            // Preparation
            _player.HealthPotion = initialHealthPotions;

            var groundCell = new Ground { X = 0, Y = 0 };
            SetupMazeCells(groundCell);

            // Act
            _healthPotion.PlayerStepInMe(_player);

            // Assert
            Assert.AreEqual(expectedHealthPotions, _player.HealthPotion, // проверяем, что количество зелий у игрока увеличилось на 1
                $"Player should have {expectedHealthPotions} health potions after stepping on the health potion cell");
        }

        [TestCase(1)]
        [TestCase(2)]
        public void PlayerStepInMe_ThrowsExceptionWhenPlayerAlreadyHasMaxHealthPotions(int initialHealthPotions) //Тест на экзепшн, когда у игрока уже есть максимум зелий
        {
            // Preparation
            _player.HealthPotion = initialHealthPotions;

            var groundCell = new Ground { X = 0, Y = 0 };
            SetupMazeCells(groundCell);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _healthPotion.PlayerStepInMe(_player)); //проверяем, что есть экзепшн
        }

        [Test]
        public void PlayerStepInMe_NewHealthPotionReplacedToRandomGround() //Тест на создание нового зелья с координатами слуйчайной земли
        {
            // Preparation
            var groundCell1 = new Ground { X = 2, Y = 3 }; //создаем первую ячейку земли 
            var groundCell2 = new Ground { X = 5, Y = 7 }; //создаем вторую ячейку земли (куда упадет новое зелье)
            SetupMazeCells(groundCell1, groundCell2);
            SetupRandomNext(1); //кладем в groundCell2

            IBaseCell capturedCell = null; //Созадем коробочку для сохранения нового зелья
            _mazeMock.Setup(m => m.ReplaceToCell(It.IsAny<IBaseCell>())) //настраиваем перехват вызова ReplaceToCell. Когда  в тесте будет вызван ReplaceToCell, то мок перехватит это зелье
                .Callback<IBaseCell>(cell => capturedCell = cell); //а с помощью коллбека сохранит это зелье в переменную capturedCell, чтобы потом проверить его наличие и координаты

            // Act
            _healthPotion.PlayerStepInMe(_player);

            // Assert 
            _mazeMock.Verify(maze => maze.ReplaceToCell(It.IsAny<IBaseCell>()), Times.Once, //проверяем, что ReplaceToCell был вызван
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
            var wallCell = new Wall { X = 0, Y = 0 }; //добавляем только клетку стены, чтобы проверить, что зелье не может быть создано
            SetupMazeCells(wallCell);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _healthPotion.PlayerStepInMe(_player));
        }
    }
}
