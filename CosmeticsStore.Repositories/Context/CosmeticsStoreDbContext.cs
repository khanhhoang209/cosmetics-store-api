using CosmeticsStore.Repositories.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CosmeticsStore.Repositories.Context;

public class CosmeticsStoreDbContext : IdentityDbContext<ApplicationUser>
{
    public CosmeticsStoreDbContext(DbContextOptions<CosmeticsStoreDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json",true,true)
            .Build();
        var strConn = config["ConnectionStrings:CosmeticsStoreDb"];

        return strConn!;
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Composite primary key for Cart
        modelBuilder.Entity<Cart>()
            .HasKey(c => new { c.UserId, c.ProductId });

        // Primary key for Category
        modelBuilder.Entity<Category>()
            .HasKey(c => c.CategoryId);

        // Primary key for Order
        modelBuilder.Entity<Order>()
            .HasKey(o => o.OrderId);

        // Composite primary key for OrderDetail
        modelBuilder.Entity<OrderDetail>()
            .HasKey(od => new { od.OrderId, od.ProductId });

        // Primary key for Product
        modelBuilder.Entity<Product>()
            .HasKey(p => p.ProductId);

        // Configuring relationships and other properties if needed
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Product)
            .WithMany(p => p.OrderDetails)
            .HasForeignKey(od => od.ProductId);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        modelBuilder.Entity<Cart>()
            .HasOne(c => c.User)
            .WithMany(u => u.Carts)
            .HasForeignKey(c => c.UserId);

        modelBuilder.Entity<Cart>()
            .HasOne(c => c.Product)
            .WithMany(p => p.Carts)
            .HasForeignKey(c => c.ProductId);

    }
}