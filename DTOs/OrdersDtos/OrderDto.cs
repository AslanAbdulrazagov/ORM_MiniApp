using ORM_MiniApp.Enums;

namespace ORM_MiniApp.DTOs.Order
{
    public class OrderDto
    {
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
    }
}
