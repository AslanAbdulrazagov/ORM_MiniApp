namespace ORM_MiniApp.DTOs.Product
{
    public class ProductPostDto
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; } = null!;
    }
}
