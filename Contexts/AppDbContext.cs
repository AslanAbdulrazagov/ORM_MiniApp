using Microsoft.EntityFrameworkCore;
using ORM_MiniApp.Configurations;
using ORM_MiniApp.Models;

namespace ORM_MiniApp.Contexts
{
    public class AppDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-4GGPT8EP\\SQLEXPRESS;Database=ORM;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
        DbSet<Order> Orders { get; set; } = null!;
        DbSet<Payment> Payments { get; set; } = null!;
        DbSet<Product> Products { get; set; } = null!;
        DbSet<User> Users { get; set; } = null!;
        DbSet<OrderDetail> OrderDetails { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
