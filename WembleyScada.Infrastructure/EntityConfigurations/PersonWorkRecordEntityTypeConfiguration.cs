using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WembleyScada.Domain.AggregateModels.PersonAggregate;

namespace WembleyScada.Infrastructure.EntityConfigurations;

public class PersonWorkRecordEntityTypeConfiguration : IEntityTypeConfiguration<PersonWorkRecord>
{
    public void Configure(EntityTypeBuilder<PersonWorkRecord> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.HasOne(x => x.Device).WithMany(x => x.WorkRecords).HasForeignKey(x => x.DeviceId);
    }
}
