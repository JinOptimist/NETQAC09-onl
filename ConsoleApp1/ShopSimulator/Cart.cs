namespace ShopSimulator;

public class Cart
{
    public List<Product> Items = new List<Product>();
    
    public void Add(Product product)
    {
        Items.Add(product);
    }

    public bool Remove(Product product)
    {
        return Items.Remove(product);
    }

    public int GetTotal()
    {
        return Items.Sum(x => x.Price);
    }

    public void Clear()
    {
        Items.Clear();
    }
}