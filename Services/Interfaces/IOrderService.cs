using ORM_MiniApp.DTOs.Order;
using ORM_MiniApp.Models;

namespace ORM_MiniApp.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrder(OrderDto order);
        Task CancelOrder(int id);

        Task CompleteOrder(int id);

        Task<List<Order>> GetOrders();
        Task AddOrderDetail(OrderDetailDto orderDetailDto);
        Task<List<OrderDetailDto>> GetOrderDetailsByOrderId(int orderId);
    }
}
