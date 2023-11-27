using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WembleyScada.Domain.AggregateModels.ProductAggregate;

namespace WembleyScada.Infrastructure.EntityConfigurations;

public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(p => p.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.HasMany(p => p.Devices).WithMany();
        builder.HasMany(p => p.References).WithOne(p => p.Product).HasForeignKey(p => p.ProductId);
    }
}
