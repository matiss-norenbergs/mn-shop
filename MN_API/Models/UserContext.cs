using DBreeze;
using Microsoft.EntityFrameworkCore;

namespace MN_API.Models
{
    public class UserContext : DbContext
    {
        public DBreezeEngine userEngine = null;

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            var test = options;
        }

        public DbSet<UserData> UserDataSet { get; set; } = null!;
    }
}
