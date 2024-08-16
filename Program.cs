using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ORM_MiniApp.Services.Interfaces;
using ORM_MiniApp.DTOs.Product;
using ORM_MiniApp.DTOs.User;
using ORM_MiniApp.DTOs.Order;
using ORM_MiniApp.DTOs.Payment;
using ORM_MiniApp.Exceptions;
using ORM_MiniApp.Models;
using ORM_MiniApp.Services.Implementations;
using ORM_MiniApp.Repositories.Abstractions;
using ORM_MiniApp.Repositories.Implementations;

namespace ORM_MiniApp
{
    class Program
    {
        private static IUserService _userService;
        private static IProductService _productService;
        private static IOrderService _orderService;
        private static IPaymentService _paymentService;

        //Services.AddScoped<IUserService, UserService>()


        static async Task Main(string[] args)
        {
            //AddScoped<IUserService, UserService>();

            var serviceProvider = new ServiceCollection()

                .AddScoped<IUserService, UserService>()
                .AddScoped<IProductService, ProductService>()
                .AddScoped<IOrderService, OrderService>()
                .AddScoped<IPaymentService, PaymentService>()
                .AddScoped<IUserRepository, UserRepository>()

    .AddScoped<IProductRepository, ProductRepository>()
    .AddScoped<IOrderRepository, OrderRepository>()
    .AddScoped<IPaymentRepository, PaymentRepository>()
    .AddScoped<IOrderDetailRepository, OrderDetailRepository>()
                .BuildServiceProvider();

            //_userService = serviceProvider.GetService<IUserService>();
            //_productService = serviceProvider.GetService<IProductService>();
            //_orderService = serviceProvider.GetService<IOrderService>();
            //_paymentService = serviceProvider.GetService<IPaymentService>();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. User Operations");
                Console.WriteLine("2. Product Operations");
                Console.WriteLine("3. Order Operations");
                Console.WriteLine("4. Payment Operations");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");
                var option = Console.ReadLine();

                try
                {
                    switch (option)
                    {
                        case "1":
                            await UserOperations();
                            break;
                        case "2":
                            await ProductOperations();
                            break;
                        case "3":
                            await OrderOperations();
                            break;
                        case "4":
                            await PaymentOperations();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Invalid option, please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private static async Task UserOperations()
        {
            Console.Clear();
            Console.WriteLine("1. Register User");
            Console.WriteLine("2. Login User");
            Console.WriteLine("3. Update User Info");
            Console.WriteLine("4. Get User Orders");
            Console.WriteLine("5. Export User Orders to Excel");
            Console.Write("Select an option: ");
            var option = Console.ReadLine();

            try
            {
                switch (option)
                {
                    case "1":
                        Console.Write("Full Name: ");
                        var fullName = Console.ReadLine();
                        Console.Write("Email: ");
                        var email = Console.ReadLine();
                        Console.Write("Password: ");
                        var password = Console.ReadLine();
                        Console.Write("Address: ");
                        var address = Console.ReadLine();
                        await _userService.RegisterUser(new UserRegisterDto
                        {
                            FullName = fullName,
                            Email = email,
                            Password = password,
                            Address = address
                        });
                        Console.WriteLine("User registered successfully.");
                        break;

                    case "2":
                        Console.Write("Full Name: ");
                        fullName = Console.ReadLine();
                        Console.Write("Password: ");
                        password = Console.ReadLine();
                        await _userService.Login(new UserLoginDto
                        {
                            FullName = fullName,
                            Password = password
                        });
                        break;

                    case "3":
                        Console.Write("User Id: ");
                        var userId = int.Parse(Console.ReadLine());
                        Console.Write("Full Name: ");
                        fullName = Console.ReadLine();
                        Console.Write("Email: ");
                        email = Console.ReadLine();
                        Console.Write("Password: ");
                        password = Console.ReadLine();
                        Console.Write("Address: ");
                        address = Console.ReadLine();
                        await _userService.Update(new User
                        {
                            Id = userId,
                            FullName = fullName,
                            Email = email,
                            Password = password,
                            Address = address
                        });
                        Console.WriteLine("User updated successfully.");
                        break;

                    case "4":
                        Console.Write("User Id: ");
                        userId = int.Parse(Console.ReadLine());
                        var orders = await _userService.GetUserOrders(userId);
                        foreach (var order in orders)
                        {
                            Console.WriteLine($"Order Id: {order.Id}, Total Amount: {order.TotalAmount}, Status: {order.Status}");
                        }
                        break;

                    case "5":
                        Console.Write("User Id: ");
                        userId = int.Parse(Console.ReadLine());
                        //await _userService.ExportUserOrdersToExcel(userId);
                        Console.WriteLine("Orders exported to Excel successfully.");
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static async Task ProductOperations()
        {
            Console.Clear();
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Update Product");
            Console.WriteLine("3. Delete Product");
            Console.WriteLine("4. Get Products");
            Console.WriteLine("5. Search Products");
            Console.WriteLine("6. Get Product by Id");
            Console.Write("Select an option: ");
            var option = Console.ReadLine();

            try
            {
                switch (option)
                {
                    case "1":
                        Console.Write("Name: ");
                        var name = Console.ReadLine();
                        Console.Write("Price: ");
                        var price = decimal.Parse(Console.ReadLine());
                        Console.Write("Stock: ");
                        var stock = int.Parse(Console.ReadLine());
                        Console.Write("Description: ");
                        var description = Console.ReadLine();
                        await _productService.AddProductAsync(new ProductPostDto
                        {
                            Name = name,
                            Price = price,
                            Stock = stock,
                            Description = description
                        });
                        Console.WriteLine("Product added successfully.");
                        break;

                    case "2":
                        Console.Write("Product Id: ");
                        var productId = int.Parse(Console.ReadLine());
                        Console.Write("Name: ");
                        name = Console.ReadLine();
                        Console.Write("Price: ");
                        price = decimal.Parse(Console.ReadLine());
                        Console.Write("Stock: ");
                        stock = int.Parse(Console.ReadLine());
                        Console.Write("Description: ");
                        description = Console.ReadLine();
                        await _productService.UpdateProductAsync(new ProductGetDto
                        {
                            Id = productId,
                            Name = name,
                            Price = price,
                            Stock = stock,
                            Description = description
                        });
                        Console.WriteLine("Product updated successfully.");
                        break;

                    case "3":
                        Console.Write("Product Id: ");
                        productId = int.Parse(Console.ReadLine());
                        await _productService.DeleteProductAsync(productId);
                        Console.WriteLine("Product deleted successfully.");
                        break;

                    case "4":
                        var products = await _productService.GetProductsAsync();
                        foreach (var product in products)
                        {
                            Console.WriteLine($"Product Id: {product.Id}, Name: {product.Name}, Price: {product.Price}, Stock: {product.Stock}");
                        }
                        break;

                    case "5":
                        Console.Write("Search Name: ");
                        var searchName = Console.ReadLine();
                        var searchedProducts = await _productService.SearchProducts(searchName);
                        foreach (var product in searchedProducts)
                        {
                            Console.WriteLine($"Product Id: {product.Id}, Name: {product.Name}, Price: {product.Price}, Stock: {product.Stock}");
                        }
                        break;

                    case "6":
                        Console.Write("Product Id: ");
                        productId = int.Parse(Console.ReadLine());
                        var productDto = await _productService.GetProductByIdAsync(productId);
                        Console.WriteLine($"Product Id: {productDto.Id}, Name: {productDto.Name}, Price: {productDto.Price}, Stock: {productDto.Stock}");
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static async Task OrderOperations()
        {
            Console.Clear();
            Console.WriteLine("1. Create Order");
            Console.WriteLine("2. Cancel Order");
            Console.WriteLine("3. Complete Order");
            Console.WriteLine("4. Add Order Detail");
            Console.WriteLine("5. Get Orders");
            Console.WriteLine("6. Get Order Details by Order Id");
            Console.Write("Select an option: ");
            var option = Console.ReadLine();

            try
            {
                switch (option)
                {
                    case "1":
                        Console.Write("User Id: ");
                        var userId = int.Parse(Console.ReadLine());
                        Console.Write("Total Amount: ");
                        var totalAmount = decimal.Parse(Console.ReadLine());
                        var status = Enum.Parse<Enums.OrderStatus>(Console.ReadLine(), true);
                        await _orderService.CreateOrder(new OrderDto
                        {
                            UserId = userId,
                            TotalAmount = totalAmount,
                            Status = status
                        });
                        Console.WriteLine("Order created successfully.");
                        break;

                    case "2":
                        Console.Write("Order Id: ");
                        var orderId = int.Parse(Console.ReadLine());
                        await _orderService.CancelOrder(orderId);
                        Console.WriteLine("Order cancelled successfully.");
                        break;

                    case "3":
                        Console.Write("Order Id: ");
                        orderId = int.Parse(Console.ReadLine());
                        await _orderService.CompleteOrder(orderId);
                        Console.WriteLine("Order completed successfully.");
                        break;

                    case "4":
                        Console.Write("Order Id: ");
                        orderId = int.Parse(Console.ReadLine());
                        Console.Write("Product Id: ");
                        var productId = int.Parse(Console.ReadLine());
                        Console.Write("Quantity: ");
                        var quantity = int.Parse(Console.ReadLine());
                        Console.Write("Price Per Item: ");
                        var pricePerItem = decimal.Parse(Console.ReadLine());
                        await _orderService.AddOrderDetail(new OrderDetailDto
                        {
                            OrderId = orderId,
                            ProductId = productId,
                            Quantity = quantity,
                            PricePerItem = pricePerItem
                        });
                        Console.WriteLine("Order detail added successfully.");
                        break;

                    case "5":
                        var orders = await _orderService.GetOrders();
                        foreach (var order in orders)
                        {
                            Console.WriteLine($"Order Id: {order.Id}, User Id: {order.UserId}, Total Amount: {order.TotalAmount}, Status: {order.Status}");
                        }
                        break;

                    case "6":
                        Console.Write("Order Id: ");
                        orderId = int.Parse(Console.ReadLine());
                        var orderDetails = await _orderService.GetOrderDetailsByOrderId(orderId);
                        foreach (var detail in orderDetails)
                        {
                            Console.WriteLine($"Order Detail Id: {detail.Id}, Product Id: {detail.ProductId}, Quantity: {detail.Quantity}, Price Per Item: {detail.PricePerItem}");
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static async Task PaymentOperations()
        {
            Console.Clear();
            Console.WriteLine("1. Make Payment");
            Console.WriteLine("2. Get Payments");
            Console.Write("Select an option: ");
            var option = Console.ReadLine();

            try
            {
                switch (option)
                {
                    case "1":
                        Console.Write("Order Id: ");
                        var orderId = int.Parse(Console.ReadLine());
                        Console.Write("Amount: ");
                        var amount = decimal.Parse(Console.ReadLine());
                        await _paymentService.MakePaymentAsync(new PaymentDto
                        {
                            OrderId = orderId,
                            Amount = amount
                        });
                        Console.WriteLine("Payment made successfully.");
                        break;

                    case "2":
                        var payments = await _paymentService.GetAllPaymentsAsync();
                        foreach (var payment in payments)
                        {
                            Console.WriteLine($" Order Id: {payment.OrderId}, Amount: {payment.Amount}, Payment Date: {payment.PaymentDate}");
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
