using System.ComponentModel.DataAnnotations;
using MazeConsole.MazeExceptions;
using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;
using MazeConsole.MazeModels.Intefaces;
using Moq;
using NUnit.Framework;

namespace MazeConsole.Tests.MazeModels.Cells;

public class FireTest
{
    //поля необзодимые всем тестам пожтому создаем их один раз тут на уровне класса
    private Fire _fire;
    private Mock<IPlayer> _playerMock;
    private IPlayer _player;
    private Mock<IMaze> _mazeMock;
    private List<string> _logMessages;

    [SetUp]
    public void SetUp()
    {
        _logMessages = new List<string>(); // список нужен чтобы проверить запись ошибки в лог
        _mazeMock = new Mock<IMaze>(); // тестлабиринт
        _mazeMock.Setup(m => m.LogMessages)
            .Returns(_logMessages); //вернуть список при запроси логмеседж по мок лабиринту 
        _mazeMock.Setup(m => m.Seed).Returns(12345); // нудно для текста ошибки

        _fire = new Fire { X = 2, Y = 3 };
        _fire.MazeWhereIWasCreated = _mazeMock.Object;

        _playerMock = new Mock<IPlayer>(); //мокигрок
        _playerMock.SetupAllProperties(); // разрешение мок игрокку хранить координаты
        _player = _playerMock.Object; // получить обхект игрока из мок
        _player.CurrentHealth = 20; // здоровье тест игрока начальное
        _player.MazeWhereIWasCreated = _mazeMock.Object;
    }

    [Test]
    public void MySymbol_Should_ReturnF()
    {
        var fire = _fire;
        var result = fire.MySymbol;
        Assert.That(result, Is.EqualTo('F')); // проверяет что рисуется огонь на поле
    }

    [Test]
    public void PlayerStepInMe_WhenPlayerHasEnoughHealth_DecreasesHealth()

    {
        _player.CurrentHealth = 20;
        var result = _fire.PlayerStepInMe(_player);
        Assert.IsFalse(result);
        Assert.That(_player.CurrentHealth, Is.EqualTo(15)); // огонь отнимает по 5 

    }

    [Test]
    public void PlayerStepInMe_ShouldReturn_False()

    {
        _player.CurrentHealth = 20;
        var fire = _fire;
        var player = _playerMock.Object;
        var result = fire.PlayerStepInMe(player);
        Assert.That(result, Is.False);

    }

    [Test]
    public void PlayerStepInMe_Should_ThrowFireCellException_WhenPlayerHealthIsZeroOrLess()
    {
        var fire = _fire;
        var player = _playerMock.Object;
        {
            _player.CurrentHealth = 5;
            Assert.Throws<FireCellException>(() => _fire.PlayerStepInMe(_player));
        }
    }
}
