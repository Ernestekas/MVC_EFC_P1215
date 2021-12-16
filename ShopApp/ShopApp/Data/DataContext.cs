using Microsoft.EntityFrameworkCore;
using ShopApp.Models;

namespace ShopApp.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Shop> Shops { get; set; }

        public DbSet<ShopItem> ShopItems { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shop>().HasData(
                new Shop()
                {
                    Id = 1,
                    Name = "Robotukai"
                },
                 new Shop()
                 {
                     Id = 2,
                     Name = "Kojinės ir aš"
                 });

            modelBuilder.Entity<ShopItem>().HasData(
                new ShopItem()
                {
                    Id = 1,
                    Name = "Tiristorius"
                },
                new ShopItem()
                {
                    Id = 2,
                    Name = "Transformatorius"
                },
                new ShopItem()
                {
                    Id = 3,
                    Name = "Mėlynos kojinės"
                },
                new ShopItem()
                {
                    Id = 4,
                    Name = "Raudonos kojinės-pirštinės"
                },
                new ShopItem()
                {
                    Id = 5,
                    Name = "Permatomos kojinės"
                });
        }
    }
}
