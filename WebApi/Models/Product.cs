namespace WebApi.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int ArtNumber { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
