using Microsoft.EntityFrameworkCore;

namespace MN_API.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public DbSet<UserData> UserDataSet { get; set; } = null!;
    }
}
