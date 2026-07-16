using BlackjackConsole.Models;

namespace BlackjackConsole;

public class BlackjackGame
{
    private const int START_BALANCE = 100; // Начальние фишки
    private const int BET = 10; //размер ставки

    private Deck _deck;
    private readonly Player _player;
    private readonly Dealer _dealer;
    private readonly Random _random;

    public BlackjackGame(int? seed = null)
    {
        _random = seed.HasValue ? new Random(seed.Value) : new Random();
        _player = new Player("Игрок", START_BALANCE);
        _dealer = new Dealer();
    }

    public void Play()
    {
        Console.WriteLine("======================================");
        Console.WriteLine("      Добро пожаловать в Блэкджек!");
        Console.WriteLine("======================================");

        AskPlayerName(); // метод, спрашиваем имя игрока

        Console.WriteLine($"\nПривет, {_player.Name}! Стартовый баланс: {_player.Balance} фишек.");
        Console.WriteLine($"Ставка за раунд: {BET} фишек.");

        var keepPlaying = true;

        while (keepPlaying)
        {
            if (_player.Balance < BET) // если фишек не хватает на ставку
            {
                Console.WriteLine("\nУ вас закончились фишки. Игра окончена.");
                break;
            }

            PlayRound(); //запускаем метод раунд

            keepPlaying = AskToContinue(); // спрашиваем, играть ли ещё
        }

        Console.WriteLine($"\nСпасибо за игру, {_player.Name}! Итоговый баланс: {_player.Balance} фишек.");
    }

    private void AskPlayerName()
    {
        Console.Write("Как вас зовут? ");
        var input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input) == false)
        {
            _player.Name = input.Trim(); //сохраняем имя игрока
        }
    }

    private void PlayRound()
    {
        StartNewRound();// создаем колоду и новые карты у игроков, списываем фишки
        DealInitialCards(); //раздаем карты

        ShowTable(revealDealerCard: false); // показываем карты, 1 карта дилера скрыта

        var playerBusted = RunPlayerTurn(); //ход игрока, узнаём был ли перебор

        if (playerBusted == false)
        {
            RunDealerTurn();//если у игрока нет перебора, играет дилер
        }

        ShowTable(revealDealerCard: true);

        FinishRound(playerBusted);//итоги раунда
    }

    private void StartNewRound()
    {
        _deck = new Deck(_random.Next());
        _deck.Shuffle();

        _player.Hand = new Hand();
        _dealer.Hand = new Hand();

        _player.Balance -= BET;

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

    private bool RunPlayerTurn() //ход игрока: спрашиваем действия, пока не Stand или перебор
    {
        while (true)
        {
            var action = AskPlayerAction(); // метод взяеть еще карту или остановиться

            switch (action)
            {
                case PlayerAction.Hit:
                    _player.Hand.AddCard(_deck.Draw()); // добавляем новую карту
                    ShowTable(revealDealerCard: false); // показываем карты, карта дилера скрыта

                    if (_player.Hand.IsBust) // если перебор    о
                    {
                        Console.WriteLine("Перебор! Вы проиграли эту раздачу.");
                        return true;
                    }

                    continue;

                case PlayerAction.Stand:
                    return false;
                                   
            }
        }
    }

    private void RunDealerTurn() // ход дилера
    {
        Console.WriteLine("\nХод дилера...");

        while (_dealer.ShouldHit()) //должен ли дилер сейчас взять ещё одну карту
        {
            _dealer.Hand.AddCard(_deck.Draw());
        }
    }

    private PlayerAction AskPlayerAction()  // метод спрашивает у игрока Hit или Stand
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

    private void ShowTable(bool revealDealerCard) //метод показывает текущие карты
    {
        Console.WriteLine();

        var dealerLine = revealDealerCard ? _dealer.ShowHand() : _dealer.ShowHandHidden();
        // выбираем, показать все карты дилера или скрыть первую
        Console.WriteLine($"Карты дилера: {dealerLine}");
        Console.WriteLine($"Ваши карты: {_player.ShowHand()}");
    }

    private void FinishRound(bool playerBusted) //// подводит итог раунда и меняет баланс игрока
    {
        var playerScore = _player.Hand.GetScore();
        var dealerScore = _dealer.Hand.GetScore();
        //считаем очки

        Console.WriteLine($"\nСчёт: {_player.Name} - {playerScore}, Дилер - {dealerScore}");

        if (playerBusted)
        {
            Console.WriteLine($"Вы проиграли ставку в {BET} фишек.");
        }
        else if (_dealer.Hand.IsBust)
        {
            Console.WriteLine("У дилера перебор! Вы выиграли раздачу!");
            _player.Balance += BET * 2;
        }
        else if (playerScore > dealerScore)
        {
            Console.WriteLine("Вы выиграли раздачу!");
            _player.Balance += BET * 2;
        }
        else if (playerScore == dealerScore)
        {
            Console.WriteLine("Ничья! Ставка возвращена.");
            _player.Balance += BET;
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
