using Microsoft.EntityFrameworkCore;
using ShopApp.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ShopApp.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Shop> Shops { get; set; }

        public DbSet<ShopItem> ShopItems { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ShopItemTag> ShopItemTags { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShopItemTag>()
            .HasKey(bc => new { bc.TagId, bc.ShopItemId});

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ShopItem>().Property<bool>("IsDeleted");
            modelBuilder.Entity<ShopItem>().HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);

            modelBuilder.Entity<Shop>().Property<bool>("IsDeleted");
            modelBuilder.Entity<Shop>().HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);

            modelBuilder.Entity<Tag>().Property<bool>("IsDeleted");
            modelBuilder.Entity<Tag>().HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);

            modelBuilder.Entity<ShopItemTag>().Property<bool>("IsDeleted");
            modelBuilder.Entity<ShopItemTag>().HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);
        }

        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }

        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }
        }
    }
}
