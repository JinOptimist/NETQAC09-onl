namespace ShopSimulator;

public class Shop
{
    public List<Product> Catalog = new List<Product>
    {
        new Product("Magic Wand", 572, 25, "Accessories"),
        new Product("Power Treads", 1400, 19, "Accessories"),
        new Product("Black King Bar", 4050, 12, "Armor"),
        new Product("Hurricane Pike", 4450, 8, "Armor"),
        new Product("Battle Fury", 4100, 10, "Weapons"),
        new Product("Butterfly", 5450, 5, "Weapons"),
        new Product("Mjollnir", 5500, 2, "Artifacts"),
        new Product("Sange and Yasha", 4200, 15, "Artifacts"),
    };

    public Product? SearchByName(string name)
    {
        return Catalog.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
    }
    
    public List<Product> SearchByCategory(string category)
    {
        return Catalog.Where(x => x.Category.ToLower() == category.ToLower()).ToList();
    }
}