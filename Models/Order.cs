using ORM_MiniApp.Enums;

namespace ORM_MiniApp.Models
{
    public class Order:BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }

        
    }
}
