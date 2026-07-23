namespace WebAppSmile.Models
{
    public class CocktailApiResponse
    {
        public List<DrinkDto> Drinks { get; set; }
    }

    public class DrinkDto
    {
        public string StrDrink { get; set; } //название коктейля
        public string StrDrinkThumb { get; set; } //фото коктейля
    }
}