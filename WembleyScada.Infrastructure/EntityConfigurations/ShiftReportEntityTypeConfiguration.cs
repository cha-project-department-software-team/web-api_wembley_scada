using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WembleyScada.Domain.AggregateModels.ShiftReportAggregate;

namespace WembleyScada.Infrastructure.EntityConfigurations;
public class ShiftReportEntityTypeConfiguration : IEntityTypeConfiguration<ShiftReport>
{
    public void Configure(EntityTypeBuilder<ShiftReport> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.HasOne(p => p.Device)
            .WithMany()
            .HasForeignKey(p => p.DeviceId);

        builder.OwnsMany(p => p.Shots, p =>
        {
            p.WithOwner();
            p.Property(p => p.TimeStamp).IsRequired();
            p.Property(p => p.ExecutionTime).IsRequired();
            p.Property(p => p.CycleTime).IsRequired();
        });
    }
}
