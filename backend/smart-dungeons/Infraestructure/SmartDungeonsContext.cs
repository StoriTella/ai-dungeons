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
            modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

            modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
    }
}