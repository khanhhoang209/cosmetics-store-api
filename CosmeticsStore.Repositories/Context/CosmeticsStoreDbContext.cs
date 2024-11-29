using CosmeticsStore.Repositories.Models.Domain;
using Microsoft.AspNetCore.Identity;
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

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Method> Methods { get; set; }

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
        var connectionString = config["ConnectionStrings:CosmeticsStoreDb"];

        return connectionString!;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Composite primary key for Cart
        modelBuilder.Entity<Cart>()
            .HasKey(c => new { c.UserId, c.ProductId });

        // Primary key for Category
        modelBuilder.Entity<Category>()
            .HasKey(c => c.Id);

        // Primary key for Order
        modelBuilder.Entity<Order>()
            .HasKey(o => o.Id);

        // Composite primary key for OrderDetail
        modelBuilder.Entity<OrderDetail>()
            .HasKey(od => new { od.OrderId, od.ProductId });

        // Primary key for Product
        modelBuilder.Entity<Product>()
            .HasKey(p => p.Id);

        // Primary key for Payment
        modelBuilder.Entity<Payment>()
            .HasKey(p => p.Id);

        // Primary key for Method
        modelBuilder.Entity<Method>()
            .HasKey(m => m.Id);

        // Configuring relationships and other properties if needed
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Payment)
            .WithOne(p => p.Order)
            .HasForeignKey<Payment>(p => p.OrderId);

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

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Method)
            .WithMany(m => m.Payments)
            .HasForeignKey(o => o.MethodId);

        // Seeding Data
        modelBuilder.Entity<IdentityRole>().HasData(SeedingRoles());

    }

    private ICollection<IdentityRole> SeedingRoles()
    {
        return new List<IdentityRole>()
        {
            new IdentityRole() {
                Id = "3631e38b-60dd-4d1a-af7f-a26f21c2ef82",
                Name = "manager",
                NormalizedName = "MANAGER",
                ConcurrencyStamp = "3631e38b-60dd-4d1a-af7f-a26f21c2ef82"
            },
            new IdentityRole() {
                Id = "51ef7e08-ff07-459b-8c55-c7ebac505103",
                Name = "staff",
                NormalizedName = "STAFF",
                ConcurrencyStamp = "51ef7e08-ff07-459b-8c55-c7ebac505103"
            },
            new IdentityRole() {
                Id = "37a7c5df-4898-4fd4-8e5f-d2abd4b57520",
                Name = "customer",
                NormalizedName = "CUSTOMER",
                ConcurrencyStamp = "37a7c5df-4898-4fd4-8e5f-d2abd4b57520"
            }
        };
    }
}