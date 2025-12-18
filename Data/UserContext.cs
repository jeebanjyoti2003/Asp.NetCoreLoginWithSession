using Asp.NetCoreLoginWithSession.Models;
using Microsoft.EntityFrameworkCore;

namespace Asp.NetCoreLoginWithSession.Data
{
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
       
        public DbSet<User> UserMaster { get; set; }
    }
}
