namespace ShopSimulator;

public class Product(string name, int price, int stock, string category)
{
    public string Name { get; set; } = name;
    public int Price { get; set; } = price;
    public int Stock { get; set; } = stock;
    public string Category { get; set; } = category;
}