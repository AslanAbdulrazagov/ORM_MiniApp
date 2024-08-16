namespace ORM_MiniApp.DTOs.Payment
{
    public class PaymentDto
    {
        public int OrderId { get; set; }
        public Models.Order Order { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
