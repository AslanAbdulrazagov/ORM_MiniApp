using ORM_MiniApp.DTOs.Payment;

namespace ORM_MiniApp.Services.Interfaces
{
    public interface IPaymentService
    {
        public Task MakePaymentAsync(PaymentDto payment);
        public Task<List<PaymentDto>> GetAllPaymentsAsync();

    }
}
