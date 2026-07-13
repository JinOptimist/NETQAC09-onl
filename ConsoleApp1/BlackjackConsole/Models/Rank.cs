namespace BlackjackConsole.Models;

// Достоинство карты.
// Значения 2-14 подобраны так, чтобы не совпадать друг с другом
// (иначе Jack/Queen/King, у которых одинаковое количество очков (10),
// перепутались бы между собой при сравнении и в ToString())
public enum Rank
{
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13,
    Ace = 14
}
