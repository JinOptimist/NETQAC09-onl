namespace PersonalBudget_Turgenev;

public class Budget
{
    private readonly List<Transaction> _transactions = new();

    public void Add(Transaction transaction)
    {
        if (transaction.Amount <= 0)
        {
            throw new Exception(
                "Сумма операции должна быть больше нуля."
            );
        }

        _transactions.Add(transaction);
    }

    public decimal GetBalance()
    {
        var income = _transactions
            .Where(transaction =>
                transaction.Type == TransactionType.Income)
            .Sum(transaction => transaction.Amount);

        var expenses = _transactions
            .Where(transaction =>
                transaction.Type == TransactionType.Expense)
            .Sum(transaction => transaction.Amount);

        return income - expenses;
    }

    public List<Transaction> GetAllTransactions()
    {
        return _transactions.ToList();
    }

    public List<Transaction> GetTransactionsByCategory(
        string categoryName)
    {
        return _transactions
            .Where(transaction =>
                transaction.Category.Name.Equals(
                    categoryName,
                    StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public Dictionary<string, decimal> GetExpensesByCategory()
    {
        return _transactions
            .Where(transaction =>
                transaction.Type == TransactionType.Expense)
            .GroupBy(transaction => transaction.Category.Name)
            .ToDictionary(
                group => group.Key,
                group => group.Sum(
                    transaction => transaction.Amount));
    }
}