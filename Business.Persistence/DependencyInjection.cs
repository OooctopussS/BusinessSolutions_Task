#nullable disable
using Business.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];

            services.AddDbContext<BusinessDbContext>(options =>
            {
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Business.WebApi"));
            });

            services.AddScoped<IBusinessDbContext>(provider => provider.GetService<BusinessDbContext>());

            return services;
        }
    }
}
