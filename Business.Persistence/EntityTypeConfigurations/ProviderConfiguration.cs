using Business.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Business.Persistence.EntityTypeConfigurations
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.HasKey(provider => provider.Id);
            builder.HasIndex(provider => provider.Id).IsUnique();

            builder.Property(provider => provider.Name).HasColumnType("nvarchar(max)");

            builder.HasData(new Provider { Id = 1, Name = "Provider1" },
                            new Provider { Id = 2, Name = "Provider2" },
                            new Provider { Id = 3, Name = "Provider3" });
        }
    }
}
