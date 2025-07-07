using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using myapi.Models;
namespace myapi.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payment{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Cart>()
                        .HasIndex(c => c.UserId)
                        .IsUnique();

            builder.Entity<Cart>()
                .HasMany(c => c.CartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId);

            builder.Entity<CartItem>()
                .HasOne(ci => ci.FoodItem)
                .WithMany()
                .HasForeignKey(ci => ci.FoodItemId);
            builder.Entity<Order>()
            .HasIndex(o => o.MomoPaymentId)
            .IsUnique(false);
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole{
                    Name="User",
                    NormalizedName="USER"

                },
                new IdentityRole{
                    Name="Admin",
                    NormalizedName="ADMIN"
                }

            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}