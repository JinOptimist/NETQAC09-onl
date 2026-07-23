namespace WebAppSmile.Models
{
      public class GoldPriceDto
    {
        public string Symbol { get; set; }             
        public string Name { get; set; }                
        public double Price { get; set; }                
        public string Currency { get; set; }             
        public string CurrencySymbol { get; set; }      
        public string UpdatedAtReadable { get; set; }    
        public double ExchangeRate { get; set; }        
        public DateTime UpdatedAt { get; set; }           
    }
}
