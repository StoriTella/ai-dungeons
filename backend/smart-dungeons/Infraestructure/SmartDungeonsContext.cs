using Microsoft.EntityFrameworkCore;
using smart_dungeons.Domain.Users;
using smart_dungeons.Infraestructure.Users;

namespace smart_dungeons.Infrastructure
{
    public class SmartDungeonsDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }


        public SmartDungeonsDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
    }
}