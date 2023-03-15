using Business.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Business.Persistence.EntityTypeConfigurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(orderItem => orderItem.Id);
            builder.HasIndex(orderItem => orderItem.Id).IsUnique();

            builder.Property(orderItem => orderItem.Name).HasColumnType("nvarchar(max)");
            builder.Property(orderItem => orderItem.Quantity).HasColumnType("decimal(18,3)");
            builder.Property(orderItem => orderItem.Unit).HasColumnType("nvarchar(max)");
        }
    }
}
