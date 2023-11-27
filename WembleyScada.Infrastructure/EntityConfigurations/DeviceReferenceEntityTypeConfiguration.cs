using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WembleyScada.Domain.AggregateModels.DeviceReferenceAggregate;

namespace WembleyScada.Infrastructure.EntityConfigurations;

public class DeviceReferenceEntityTypeConfiguration : IEntityTypeConfiguration<DeviceReference>
{
    public void Configure(EntityTypeBuilder<DeviceReference> builder)
    {
        builder.HasKey(x => new { x.DeviceId, x.ReferenceId });

        builder.HasOne(p => p.Device).WithMany().HasForeignKey(p => p.DeviceId).IsRequired();
        builder.HasOne(p => p.Reference).WithMany().HasForeignKey(p => p.ReferenceId).IsRequired();
        builder.HasMany(p => p.MFCs).WithOne().HasForeignKey(p => new { p.DeviceId, p.ReferenceId });
    }
}
