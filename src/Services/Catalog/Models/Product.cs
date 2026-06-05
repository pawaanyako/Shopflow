namespace Catalog.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
