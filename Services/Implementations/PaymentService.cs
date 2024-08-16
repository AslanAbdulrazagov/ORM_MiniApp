using ORM_MiniApp.DTOs.Payment;
using ORM_MiniApp.Exceptions;
using ORM_MiniApp.Models;
using ORM_MiniApp.Repositories.Abstractions;
using ORM_MiniApp.Repositories.Implementations;
using ORM_MiniApp.Services.Interfaces;

namespace ORM_MiniApp.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private IPaymentRepository _paymentRepository;
        private IOrderRepository _orderRepository;

        public PaymentService(IPaymentRepository paymentRepository, IOrderRepository orderRepository)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;

        }
        public async Task<List<PaymentDto>> GetAllPaymentsAsync()
        {
            var payments = await _paymentRepository.GetAllAsync("Orders");
            List<PaymentDto> result = new List<PaymentDto>();
            foreach (var payment in payments)
            {
                
                
                    PaymentDto dto = new PaymentDto()
                    {
                        
                        Amount = payment.Amount,
                        Order = payment.Order,
                        OrderId = payment.OrderId,
                        PaymentDate = payment.PaymentDate
                    };
                    result.Add(dto);
                
            }
            return result;
        }

        public async Task MakePaymentAsync(PaymentDto payment)
        {
            var order = await _orderRepository.GetSingleAsync(x => x.Id == payment.OrderId , "OrderDetails");

            if (order == null) throw new NotFoundException("Order not found");

            foreach (var item in order.Details)
            {
                payment.Amount += item.Quantity * item.PricePerItem;
            }
            if (order.Details == null || !order.Details.Any())
                throw new InvalidPaymentException("Cannot make a payment for an order with no order details");
            Payment newPayment = new Payment()
            {
                PaymentDate = DateTime.UtcNow,
                Amount = payment.Amount,
                OrderId = payment.OrderId
            };
            await _paymentRepository.CreateAsync(newPayment);
            await _paymentRepository.SaveChangesAsync();
        }
    }
}
