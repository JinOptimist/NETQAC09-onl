//основной класс-запуск частей программы random,input,guess 
using System;

public class GameGavrilenko
{
    private readonly NumberRandom _random = new();
    private readonly InputService _input = new();
    private readonly GuessGame _game = new();
public void Play()
//очистка
    {
        Console.Clear();
        Console.WriteLine("The game 'Guess the number'");

        var range = _input.ReadRange();//получение диаппазона
        var mode = _input.ReadMode(); // получение режима игры, кто загадывает число

        int secret = _random.Generate(range.Min, range.Max, mode); //получение загаданного числа

        _game.StartGame(secret, range.Min, range.Max); //запуск игры
    }
}