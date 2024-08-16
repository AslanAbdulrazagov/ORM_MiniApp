using ORM_MiniApp.DTOs.Order;
using ORM_MiniApp.Exceptions;
using ORM_MiniApp.Models;
using ORM_MiniApp.Repositories.Abstractions;
using ORM_MiniApp.Services.Interfaces;

namespace ORM_MiniApp.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IProductRepository productRepository,IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _userRepository=userRepository;
        }
        public async Task AddOrderDetail(OrderDetailDto orderDetailDto)
        {
            var product = await _productRepository.GetSingleAsync(p => p.Id == orderDetailDto.ProductId);
            if (product == null)
            {
                throw new NotFoundException("Product not found.");
            }

            if (orderDetailDto.Quantity > product.Stock)
            {
                throw new InvalidOrderDetailException("Cannot order more than available stock.");
            }

            var orderDetail = new OrderDetail
            {
                OrderId = orderDetailDto.OrderId,
                ProductId = orderDetailDto.ProductId,
                Quantity = orderDetailDto.Quantity,
                PricePerItem = product.Price
            };

            product.Stock -= orderDetailDto.Quantity;
            _productRepository.Update(product);
            await _orderDetailRepository.CreateAsync(orderDetail);
            await _orderDetailRepository.SaveChangesAsync();
        }

        public async Task CancelOrder(int id)
        {
            var order = await _orderRepository.GetSingleAsync(x=>x.Id == id);
            if (order == null) { throw new NotFoundException("Order not found"); }
            if (order.Status == Enums.OrderStatus.Cancelled) throw new InvalidOrderException("order already cancelled");
            order.Status = Enums.OrderStatus.Cancelled;
            await _orderRepository.SaveChangesAsync();
        }

        public async Task CompleteOrder(int id)
        {
            var order = await _orderRepository.GetSingleAsync(x => x.Id == id);
            if (order == null) { throw new NotFoundException("Order not found"); }
            if (order.Status == Enums.OrderStatus.Completed) throw new InvalidOrderException("order already cancelled");
            order.Status = Enums.OrderStatus.Completed;
            await _orderRepository.SaveChangesAsync();
        }

        public async Task CreateOrder(OrderDto orderdto)
        {
            if (orderdto.TotalAmount <= 0)
            {
                throw new InvalidOrderException("Order total amount must be greater than zero!");
            }

            var order = new Order
            {
                UserId = orderdto.UserId,
                TotalAmount = orderdto.TotalAmount,
                Status = orderdto.Status,
                OrderDate = DateTime.UtcNow
            };

            await _orderRepository.CreateAsync(order);
            await _orderRepository.SaveChangesAsync();
        }

        public  async Task<List<Order>> GetOrders()
        {
            var orders = await _orderRepository.GetAllAsync("User");
            return orders;
        }
        public async Task<List<OrderDetailDto>> GetOrderDetailsByOrderId(int orderId)
        {
            var orderDetails = await _orderDetailRepository.GetAllAsync("Order");

            if (orderDetails == null || !orderDetails.Any())
            {
                throw new NotFoundException("No order details found for the given order ID.");
            }

            return orderDetails.Select(od => new OrderDetailDto
            {
                Id = od.Id,
                OrderId = od.OrderId,
                ProductId = od.ProductId,
                Quantity = od.Quantity,
                PricePerItem = od.PricePerItem
            }).ToList();
        }
        public async Task<List<OrderDto>> GetUserOrdersAsync(int userId)
        {
            var orders = await _orderRepository.GetAllAsync("User");
            return orders.Select(o => new OrderDto
            {
                
                UserId = o.UserId,
                
                TotalAmount = o.TotalAmount,
                Status = o.Status
            }).ToList();
        }
    }
}
