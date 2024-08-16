namespace ORM_MiniApp.DTOs.Order
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal PricePerItem { get; set; }

    }
}
