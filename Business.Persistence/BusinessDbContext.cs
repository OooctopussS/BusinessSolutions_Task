#nullable disable

using Business.Application.Interfaces;
using Business.Domain;
using Business.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Business.Persistence
{
    public class BusinessDbContext : DbContext, IBusinessDbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Provider> Providers { get; set; }

        public BusinessDbContext(DbContextOptions<BusinessDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new ProviderConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
