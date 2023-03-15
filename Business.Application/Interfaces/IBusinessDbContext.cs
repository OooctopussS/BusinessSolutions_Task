using Business.Domain;
using Microsoft.EntityFrameworkCore;

namespace Business.Application.Interfaces
{
    public interface IBusinessDbContext
    {
        DbSet<Order> Orders { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }
        DbSet<Provider> Providers { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
