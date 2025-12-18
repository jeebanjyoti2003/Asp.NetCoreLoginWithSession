using Asp.NetCoreLoginWithSession.Data;
using Asp.NetCoreLoginWithSession.Models;
using Microsoft.EntityFrameworkCore;

namespace Asp.NetCoreLoginWithSession.Service
{
    public class UserService : IUserService
    {
        protected UserContext _userContext;
        public UserService(UserContext userContext)
        {
            _userContext = userContext;
        }
        public async Task<int> AddUserAsync(User user)
        {
            try
            {
                await _userContext.AddAsync(user);
                await _userContext.SaveChangesAsync();
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        public async Task<User> GetUserAsync(string mail)
        {
            try
            {
                var data = await _userContext.UserMaster.FirstOrDefaultAsync(x => x.UserEmail == mail);
                if (data != null)
                {
                    return data;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }


        public async Task<int> UpdateUserAsync(User user)
        {
            try
            {
                _userContext.UserMaster.Update(user);
                await _userContext.SaveChangesAsync();
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        public async Task<User> ValidateUser(string email, string password)
        {
            try
            {
                var data = await _userContext.UserMaster.FirstOrDefaultAsync(x => x.UserEmail == email);
                if (data != null) 
                {
                    if(data.Password == password)
                    {
                        return data;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
