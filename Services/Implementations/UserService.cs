using ORM_MiniApp.DTOs.User;
using ORM_MiniApp.Exceptions;
using ORM_MiniApp.Models;
using ORM_MiniApp.Repositories.Abstractions;
using ORM_MiniApp.Repositories.Implementations;
using ORM_MiniApp.Services.Interfaces;

namespace ORM_MiniApp.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        public UserService(IUserRepository userRepository, IOrderRepository orderRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;

        }
        public async Task<List<Order>> GetUserOrders(int id)
        {
           
            var user = await _userRepository.GetSingleAsync(u => u.Id == id);
            if (user == null)
                throw new NotFoundException($"User not found with id:{id}");
            var orders = await  _orderRepository.GetAllAsync( "User");
           


            return orders;
        }

        public async Task Login(UserLoginDto user)
        {
            var userDto = await _userRepository.GetSingleAsync((u => u.FullName == user.FullName && u.Password == user.Password));
            if (userDto == null)
                throw new UserAuthenticationException("Fullname or Password is wrong!");
            Console.WriteLine("You Login Successfully!");
        }

        public async Task RegisterUser(UserRegisterDto newUser)
        {
            if (string.IsNullOrEmpty(newUser.Email) || string.IsNullOrEmpty(newUser.Password) ||
               string.IsNullOrEmpty(newUser.FullName) || string.IsNullOrEmpty(newUser.Address))
                throw new InvalidUserInformationException("All information should be entered for register!");
            var user = new User()
            {
                FullName = newUser.FullName,
                Email = newUser.Email,
                Password = newUser.Password,
                Address = newUser.Address
            };
            var users = await  _userRepository.GetAllAsync();
            foreach (var item in users)
            {
                if (item.Email == user.Email)
                    throw new InvalidUserInformationException("This email already exist!");
            }

            await _userRepository.CreateAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            var userUp = await _userRepository.GetSingleAsync(u => u.Id == user.Id);
            if (userUp == null)
                throw new NotFoundException($"User not found with id:{user.Id}");
          
            userUp.Address = user.Address;
            userUp.Email = user.Email;
            userUp.Password = user.Password;
            userUp.Email= user.Email;
            _userRepository.Update(userUp);
            await _userRepository.SaveChangesAsync();
        }
    }
}
