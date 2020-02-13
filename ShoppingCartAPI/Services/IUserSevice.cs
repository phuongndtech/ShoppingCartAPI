using ShoppingCartAPI.DTO;
using ShoppingCartAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCartAPI.Services
{
    public interface IUserSevice
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int userId);
        Task<string> CreateUser(UserRequest userRequest);
        Task<string> UpdateUser(int userId,UserRequest userRequest);
        Task<string> DeteleUser(int userId);
        Task<User> Authenticate(string username, string password);
    }
}