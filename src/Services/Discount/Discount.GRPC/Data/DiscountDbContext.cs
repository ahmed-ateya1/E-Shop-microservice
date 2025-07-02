using Discount.GRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Data
{
    public class DiscountDbContext : DbContext
    {
        public DbSet<Coupon> Coupons { get; set; }
        public DiscountDbContext(DbContextOptions<DiscountDbContext> options):base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>()
                .HasData(new Coupon
                {
                    Id = 1,
                    ProductName = "IPhone X",
                    Description = "IPhone Discount",
                    Amount = 150
                },
                new Coupon
                {
                    Id = 2,
                    ProductName = "Samsung S10",
                    Description = "Samsung Discount",
                    Amount = 100
                },
                new Coupon
                {
                    Id = 3,
                    ProductName = "Google Pixel",
                    Description = "Google Discount",
                    Amount = 50
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
