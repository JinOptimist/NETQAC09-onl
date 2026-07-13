namespace PersonalBudget_Turgenev;

public class Transaction
{
    public decimal Amount { get; set; }

    public TransactionType Type { get; set; }

    public Category Category { get; set; }

    public string Comment { get; set; }

    public DateTime Date { get; set; }

    public Transaction(
        decimal amount,
        TransactionType type,
        Category category,
        string comment)
    {
        Amount = amount;
        Type = type;
        Category = category;
        Comment = comment;
        Date = DateTime.Now;
    }
}