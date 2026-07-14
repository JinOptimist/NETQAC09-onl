namespace ShopSimulator;

public class ShopGame
{
    private Shop _shop;
    private Cart _cart;
    private int _balance = 25000;

    public ShopGame()
    {
        _shop = new Shop();
        _cart = new Cart();
    }

    public void Run()
    {
        Console.WriteLine("Welcome to the Shop Simulator!");

        while (true)
        {
            Console.WriteLine($"{Environment.NewLine}Balance: {_balance} gold");
            Console.WriteLine("1. View catalog");
            Console.WriteLine("2. Search product by name");
            Console.WriteLine("3. Search products by category");
            Console.WriteLine("4. Buy product");
            Console.WriteLine("5. View cart");
            Console.WriteLine("6. Checkout");
            Console.WriteLine("7. Exit");
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewCatalog();
                    break;
                case "2":
                    SearchByName();
                    break;
                case "3":
                    SearchByCategory();
                    break;
                case "4":
                    BuyProduct();
                    break;
                case "5":
                    ViewCart();
                    break;
                case "6":
                    Checkout();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again");
                    break;
            }
        }
    }

    private void ViewCatalog()
    {
        Console.WriteLine($"{Environment.NewLine}--- CATALOG ---");
        var catalogLines = _shop.Catalog.Select((product) => 
            $"{product.Name} — {product.Price} gold, [{product.Category}] (left: {product.Stock})");
        Console.WriteLine(string.Join(Environment.NewLine, catalogLines));
    }

    private void SearchByName()
    {
        Console.Write("Enter the name of the product: ");
        var name = Console.ReadLine() ?? "";

        var product = _shop.SearchByName(name);
        if (product == null)
        {
            Console.WriteLine("Nothing found");
            return;
        }
        Console.WriteLine($"{product.Name} — {product.Price} gold (left: {product.Stock}) [{product.Category}]");
    }

    private void SearchByCategory()
    {
        Console.Write("Enter the category of the product: ");
        var category = Console.ReadLine() ?? "";

        var products = _shop.SearchByCategory(category);
        if (products.Count == 0)
        {
            Console.WriteLine("No products in this category.");
            return;
        }
        var productLines = products.Select(product => 
            $"{product.Name} — {product.Price} gold (left: {product.Stock}) [{product.Category}]");
        Console.WriteLine(string.Join(Environment.NewLine, productLines));
    }

    private void BuyProduct()
    {
        Console.Write("Enter the name of the product: ");
        var name = Console.ReadLine() ?? "";

        var product = _shop.SearchByName(name);
        if (product == null)
        {
            Console.WriteLine("Nothing found");
            return;
        }

        if (product.Stock <= 0)
        {
            Console.WriteLine($"«{product.Name}» is out of stock");
            return;
        }
        
        var remainingBalance = _balance - _cart.GetTotal();
        if (remainingBalance < product.Price)
        {
            Console.WriteLine("Insufficient balance");
            return;
        }

        product.Stock--;
        _cart.Add(product);
        Console.WriteLine($"{product.Name} is added to cart");
    }

    private void ViewCart()
    {
        if (_cart.Items.Count == 0)
        {
            Console.WriteLine("Your cart is empty");
            return;
        }

        Console.WriteLine($"{Environment.NewLine}--- YOUR CART ---");
        var cartLines = _cart.Items.Select(product => $" - {product.Name} ({product.Price} gold)");
        Console.WriteLine(string.Join(Environment.NewLine, cartLines));
    
        Console.WriteLine($"Total: {_cart.GetTotal()} gold");
    }

    private void Checkout()
    {
        if (_cart.Items.Count == 0)
        {
            Console.WriteLine("Your cart is empty");
            return;
        }

        int total = _cart.GetTotal();
        _balance -= total;
        
        _cart.Clear();
        Console.WriteLine($"Success! Current balance: {_balance} gold");
    }
}
/*
TO DO:
1) Поправить вычет товара со склада после покупки
2) Использовать удаление товара (Remove) из корзины (Cart)
*/