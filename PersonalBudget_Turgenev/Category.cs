namespace PersonalBudget_Turgenev;

public class Category
{
	public string Name { get; set; }

	public TransactionType Type { get; set; }

	public Category(string name, TransactionType type)
	{
		Name = name;
		Type = type;
	}
}