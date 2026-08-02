using System.ComponentModel.DataAnnotations;
using MazeConsole.MazeExceptions;
using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;
using MazeConsole.MazeModels.Cells.Interaces;
using MazeConsole.MazeModels.Intefaces;
using Moq;
using NUnit.Framework;

namespace MazeConsole.Tests.MazeModels.Cells;

public class TreeTest
{
    // то что будет использоваться в нескольких тестах
    private Tree _tree;
    private Mock<IPlayer> _playerMock;
    private IPlayer _player;
    private Mock<IMaze> _mazeMock;
    private List<IBaseCell> _cells;

    [SetUp]
    public void Setup()
    {
        _cells = new List<IBaseCell>(); // список клеток чтобы можно было найти graund
        _mazeMock = new Mock<IMaze>(); // mock лабиринт
        _mazeMock.Setup(m => m.Cells).Returns(_cells);

        //создать дерево и привязать к мок лабиринту
        _tree = new Tree();
        _tree.MazeWhereIWasCreated = _mazeMock.Object;

        //создать мок игрока и храним координаты
        _playerMock = new Mock<IPlayer>();
        _playerMock.SetupAllProperties();
        _player = _playerMock.Object;

    }

    
            [Test] 
            public void MySymbol_Should_ReturnW()
            {
                var tree = _tree;
                var result = tree.MySymbol;
                Assert.That(result, Is.EqualTo('W'));
            }

            [Test]
            public void PlayerStepInMe_WhenMazeHasGround_MovesPlayerToGround()
                {
                var ground = new Ground {X = 4, Y = 5}; // координаты куда переносит дерево
                _cells.Add(ground);
                var result = _tree.PlayerStepInMe(_player);
                Assert.That(result, Is.False); // дерево не разрешает встать на себя
                // перенесение игрока на граунд
                Assert.That(_player.X, Is.EqualTo(4));
                Assert.That(_player.Y, Is.EqualTo(5));
            
        }

    }

    