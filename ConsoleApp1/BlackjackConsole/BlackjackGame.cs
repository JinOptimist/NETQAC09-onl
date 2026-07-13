using BlackjackConsole.Models;
using System.Numerics;

namespace BlackjackConsole;

public class BlackjackGame
{
    private const int StartingBalance = 100;
    private const int Bet = 10;

    private Deck _deck;
    private readonly Player _player;
    private readonly Dealer _dealer;
    private readonly Random _random;

    public BlackjackGame(int? seed = null)
    {
        _random = seed.HasValue ? new Random(seed.Value) : new Random();
        _player = new Player("Игрок", StartingBalance);
        _dealer = new Dealer();
    }

    public void Play()
    {
        Console.WriteLine("======================================");
        Console.WriteLine("      Добро пожаловать в Блэкджек!");
        Console.WriteLine("======================================");

        AskPlayerName();

        Console.WriteLine($"\nПривет, {_player.Name}! Стартовый баланс: {_player.Balance} фишек.");
        Console.WriteLine($"Ставка за раунд: {Bet} фишек.");

        var keepPlaying = true;

        while (keepPlaying)
        {
            if (_player.Balance < Bet)
            {
                Console.WriteLine("\nУ вас закончились фишки. Игра окончена.");
                break;
            }

            PlayRound();

            keepPlaying = AskToContinue();
        }

        Console.WriteLine($"\nСпасибо за игру, {_player.Name}! Итоговый баланс: {_player.Balance} фишек.");
    }

    private void AskPlayerName()
    {
        Console.Write("Как вас зовут? ");
        var input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input) == false)
        {
            _player.Name = input.Trim();
        }
    }

    private void PlayRound()
    {
        StartNewRound();
        DealInitialCards();

        ShowTable(revealDealerCard: false);

        var playerBusted = RunPlayerTurn();

        if (playerBusted == false)
        {
            RunDealerTurn();
        }

        ShowTable(revealDealerCard: true);

        FinishRound(playerBusted);
    }

    private void StartNewRound()
    {
        _deck = new Deck(_random.Next());
        _deck.Shuffle();

        _player.Hand = new Hand();
        _dealer.Hand = new Hand();

        _player.Balance -= Bet;

        Console.WriteLine("\n--- Новый раунд ---");
    }

    private void DealInitialCards()
    {
        // раздаём по очереди: игроку, дилеру, игроку, дилеру - как в реальной игре
        for (var i = 0; i < 2; i++)
        {
            _player.Hand.AddCard(_deck.Draw());
            _dealer.Hand.AddCard(_deck.Draw());
        }
    }

    private bool RunPlayerTurn()
    {
        while (true)
        {
            var action = AskPlayerAction();

            switch (action)
            {
                case PlayerAction.Hit:
                    _player.Hand.AddCard(_deck.Draw());
                    ShowTable(revealDealerCard: false);

                    if (_player.Hand.IsBust)
                    {
                        Console.WriteLine("Перебор! Вы проиграли эту раздачу.");
                        return true;
                    }

                    continue;

                case PlayerAction.Stand:
                    return false;

                default:
                    Console.WriteLine("Неизвестное действие.");
                    continue;
            }
        }
    }

    private void RunDealerTurn()
    {
        Console.WriteLine("\nХод дилера...");

        while (_dealer.ShouldHit())
        {
            _dealer.Hand.AddCard(_deck.Draw());
        }
    }

    private PlayerAction AskPlayerAction()
    {
        while (true)
        {
            Console.WriteLine("\n1 — взять карту");
            Console.WriteLine("2 — остановиться");
            Console.Write("Ваш выбор: ");

            var input = Console.ReadLine();

            if (int.TryParse(input, out var choice) && Enum.IsDefined(typeof(PlayerAction), choice))
            {
                return (PlayerAction)choice;
            }

            Console.WriteLine("Некорректный ввод. Введите 1 или 2.");
        }
    }

    private void ShowTable(bool revealDealerCard)
    {
        Console.WriteLine();

        var dealerLine = revealDealerCard ? _dealer.ShowHand() : _dealer.ShowHandHidden();
        Console.WriteLine($"Карты дилера: {dealerLine}");
        Console.WriteLine($"Ваши карты: {_player.ShowHand()}");
    }

    private void FinishRound(bool playerBusted)
    {
        var playerScore = _player.Hand.GetScore();
        var dealerScore = _dealer.Hand.GetScore();

        Console.WriteLine($"\nСчёт: {_player.Name} - {playerScore}, Дилер - {dealerScore}");

        if (playerBusted)
        {
            Console.WriteLine($"Вы проиграли ставку в {Bet} фишек.");
        }
        else if (_dealer.Hand.IsBust)
        {
            Console.WriteLine("У дилера перебор! Вы выиграли раздачу!");
            _player.Balance += Bet * 2;
        }
        else if (playerScore > dealerScore)
        {
            Console.WriteLine("Вы выиграли раздачу!");
            _player.Balance += Bet * 2;
        }
        else if (playerScore == dealerScore)
        {
            Console.WriteLine("Ничья! Ставка возвращена.");
            _player.Balance += Bet;
        }
        else
        {
            Console.WriteLine("Дилер выиграл раздачу.");
        }

        Console.WriteLine($"Баланс: {_player.Balance} фишек.");
    }

    private bool AskToContinue()
    {
        Console.Write("\nСыграть ещё раз? (y/n): ");
        var input = Console.ReadLine();

        return input != null && input.Trim().ToLower() == "y";
    }
}
