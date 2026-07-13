namespace PersonalBudget_Turgenev;

public class BudgetApp
{
    private readonly Budget _budget = new();

    private readonly List<Category> _incomeCategories = new()
    {
        new Category("Зарплата", TransactionType.Income),
        new Category("Подарок", TransactionType.Income),
        new Category("Прочее", TransactionType.Income)
    };

    private readonly List<Category> _expenseCategories = new()
    {
        new Category("Еда", TransactionType.Expense),
        new Category("Транспорт", TransactionType.Expense),
        new Category("Развлечения", TransactionType.Expense),
        new Category("Прочее", TransactionType.Expense)
    };

    public void Start()
    {
        while (true)
        {
            ShowMenu();

            var userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    AddTransaction(TransactionType.Income);
                    break;

                case "2":
                    AddTransaction(TransactionType.Expense);
                    break;

                case "3":
                    ShowBalance();
                    break;

                case "4":
                    ShowAllTransactions();
                    break;

                case "5":
                    ShowTransactionsByCategory();
                    break;

                case "6":
                    ShowExpensesByCategory();
                    break;

                case "0":
                    Console.WriteLine("Работа программы завершена.");
                    return;

                default:
                    Console.WriteLine("Неизвестный пункт меню.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private void ShowMenu()
    {
        Console.WriteLine("Личный бюджет");
        Console.WriteLine(
            $"Текущий баланс: {_budget.GetBalance()} руб.");
        Console.WriteLine();

        Console.WriteLine("1. Добавить доход");
        Console.WriteLine("2. Добавить расход");
        Console.WriteLine("3. Показать баланс");
        Console.WriteLine("4. История операций");
        Console.WriteLine("5. Операции по категории");
        Console.WriteLine("6. Расходы по категориям");
        Console.WriteLine("0. Выход");

        Console.Write("> ");
    }

    private void AddTransaction(TransactionType type)
    {
        Console.Write("Сумма: ");
        var amountText = Console.ReadLine();

        var isAmountCorrect = decimal.TryParse(
            amountText,
            out var amount);

        if (!isAmountCorrect || amount <= 0)
        {
            Console.WriteLine(
                "Сумма должна быть числом больше нуля."
            );

            return;
        }

        var categories = type == TransactionType.Income
            ? _incomeCategories
            : _expenseCategories;

        ShowCategories(categories);

        Console.Write("Выберите категорию: ");
        var categoryText = Console.ReadLine();

        var isCategoryCorrect = int.TryParse(
            categoryText,
            out var categoryNumber);

        if (!isCategoryCorrect
            || categoryNumber < 1
            || categoryNumber > categories.Count)
        {
            Console.WriteLine("Такой категории нет.");
            return;
        }

        var selectedCategory =
            categories[categoryNumber - 1];

        Console.Write("Комментарий: ");
        var comment = Console.ReadLine() ?? string.Empty;

        var transaction = new Transaction(
            amount,
            type,
            selectedCategory,
            comment
        );

        _budget.Add(transaction);

        if (type == TransactionType.Income)
        {
            Console.WriteLine("Доход записан.");
        }
        else
        {
            Console.WriteLine("Расход записан.");
        }

        Console.WriteLine(
            $"Баланс: {_budget.GetBalance()} руб."
        );
    }

    private void ShowCategories(List<Category> categories)
    {
        Console.WriteLine("Категории:");

        for (var index = 0;
             index < categories.Count;
             index++)
        {
            Console.WriteLine(
                $"{index + 1}. {categories[index].Name}"
            );
        }
    }

    private void ShowBalance()
    {
        Console.WriteLine(
            $"Текущий баланс: {_budget.GetBalance()} руб."
        );
    }

    private void ShowAllTransactions()
    {
        var transactions =
            _budget.GetAllTransactions();

        if (transactions.Count == 0)
        {
            Console.WriteLine("Операций пока нет.");
            return;
        }

        Console.WriteLine("История операций:");

        foreach (var transaction in transactions)
        {
            ShowTransaction(transaction);
        }
    }

    private void ShowTransactionsByCategory()
    {
        Console.Write(
            "Введите название категории: "
        );

        var categoryName =
            Console.ReadLine() ?? string.Empty;

        var transactions =
            _budget.GetTransactionsByCategory(
                categoryName
            );

        if (transactions.Count == 0)
        {
            Console.WriteLine(
                "Операции по этой категории не найдены."
            );

            return;
        }

        foreach (var transaction in transactions)
        {
            ShowTransaction(transaction);
        }
    }

    private void ShowExpensesByCategory()
    {
        var expensesByCategory =
            _budget.GetExpensesByCategory();

        if (expensesByCategory.Count == 0)
        {
            Console.WriteLine(
                "Расходов пока нет."
            );

            return;
        }

        Console.WriteLine(
            "Расходы по категориям:"
        );

        foreach (var expense in expensesByCategory)
        {
            Console.WriteLine(
                $"{expense.Key}: {expense.Value} руб."
            );
        }
    }

    private void ShowTransaction(
        Transaction transaction)
    {
        var typeText =
            transaction.Type == TransactionType.Income
                ? "Доход"
                : "Расход";

        Console.WriteLine(
            $"{transaction.Date:dd.MM.yyyy HH:mm} | " +
            $"{typeText} | " +
            $"{transaction.Category.Name} | " +
            $"{transaction.Amount} руб. | " +
            $"{transaction.Comment}"
        );
    }
}