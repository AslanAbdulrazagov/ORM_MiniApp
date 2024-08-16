using ORM_MiniApp.DTOs.User;
using ORM_MiniApp.Models;

namespace ORM_MiniApp.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterUser(UserRegisterDto newUser);
        Task Login(UserLoginDto user);
        Task Update(User user);
        Task<List<Order>> GetUserOrders(int id);
        //Task<IActionResult> ExportUserOrdersToExcel(int userId);
    }
}
