using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WembleyScada.Domain.AggregateModels.DeviceAggregate;

namespace WembleyScada.Infrastructure.EntityConfigurations;
public class DeviceEntityTypeConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.HasKey(p => p.DeviceId);
    }
}
