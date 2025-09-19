using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.DataAccess.DbContexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
            .HasMany(o => o.Items)
            .WithOne()  
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
