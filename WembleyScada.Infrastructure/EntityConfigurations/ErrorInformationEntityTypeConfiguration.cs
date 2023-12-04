using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WembleyScada.Domain.AggregateModels.ErrorInformationAggregate;

namespace WembleyScada.Infrastructure.EntityConfigurations;

public class ErrorInformationEntityTypeConfiguration : IEntityTypeConfiguration<ErrorInformation>
{
    public void Configure(EntityTypeBuilder<ErrorInformation> builder)
    {
        builder.HasKey(p => p.ErrorId);

        builder.HasOne(p => p.Device).WithMany().HasForeignKey(p => p.DeviceId);
        builder.HasMany(p => p.ErrorStatuses).WithOne(p => p.ErrorInformation).HasForeignKey(p => p.ErrorId);
    }
}
