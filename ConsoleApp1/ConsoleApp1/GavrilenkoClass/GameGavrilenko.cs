//основной класс-запуск частей программы random,input,guess 
namespace ConsoleApp1.GavrilenkoClass;

public class GameGavrilenko
{
    private NumberRandom _random = new();
    private InputService _input = new();
    private GuessGame _game = new();

    public void Play()
    {
        Console.Clear();
        Console.WriteLine("The game 'Guess the number'");

        var range = _input.ReadRange();//получение диаппазона
        var mode = _input.ReadMode(); // получение режима игры, кто загадывает число

        var secret = _random.Generate(range.Min, range.Max, mode); //получение загаданного числа

        _game.StartGame(secret, range.Min, range.Max); //запуск игры
    }
}