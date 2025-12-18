using Asp.NetCoreLoginWithSession.Models;

namespace Asp.NetCoreLoginWithSession.Service
{
    public interface IUserService
    {
        Task<User> GetUserAsync(string id);

        Task<int> UpdateUserAsync(User user);

        Task<int> AddUserAsync(User user);

        Task<User> ValidateUser(string email,string password);
    }
}
