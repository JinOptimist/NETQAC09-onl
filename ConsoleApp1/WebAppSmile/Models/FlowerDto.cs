namespace WebAppSmile.Models
{
    public class FlowerResponseDto
    {
        public List<FlowerDto> results { get; set; } = new();
    }


    public class FlowerDto
    {
        public string? canonicalName { get; set; }

        public string? scientificName { get; set; }

        public string? genus { get; set; }
    }
}