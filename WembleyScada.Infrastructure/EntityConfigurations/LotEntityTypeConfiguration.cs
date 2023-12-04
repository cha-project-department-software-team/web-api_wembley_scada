using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WembleyScada.Domain.AggregateModels.ReferenceAggregate;

namespace WembleyScada.Infrastructure.EntityConfigurations;

public class LotEntityTypeConfiguration : IEntityTypeConfiguration<Lot>
{
    public void Configure(EntityTypeBuilder<Lot> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.HasIndex(p => p.LotId).IsUnique();
    }
}
