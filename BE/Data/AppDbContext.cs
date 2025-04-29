using Microsoft.EntityFrameworkCore;
using MYWEBAPI.Models;
using Microsoft.Net.Http.Headers;
using Models;

namespace MYWEBAPI.Data
{
    public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<User> Users { get; set; }
    public DbSet<Categories> Categories { get; set; }
    public DbSet<Products> Products { get; set; }
    public DbSet<Orders> Orders { get; set; }
    public DbSet<OrdersDetails> OrdersDetails { get; set; }
    public DbSet<CartItems> CartItems { get; set; }
    public DbSet<Reviews> Reviews { get; set; }
    public DbSet<Payments> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Cấu hình quan hệ nếu cần
        modelBuilder.Entity<OrdersDetails>()
            .HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId);
    }
}
}