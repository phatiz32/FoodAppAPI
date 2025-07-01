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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
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