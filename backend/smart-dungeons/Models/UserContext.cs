using Microsoft.EntityFrameworkCore;

namespace smart_dungeons.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<User> TodoItems { get; set; }
    }
}