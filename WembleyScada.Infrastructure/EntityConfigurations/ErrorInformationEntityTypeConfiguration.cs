using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WembleyScada.Domain.AggregateModels.ErrorInformationAggregate;

namespace WembleyScada.Infrastructure.EntityConfigurations;

public class ErrorInformationEntityTypeConfiguration : IEntityTypeConfiguration<ErrorInformation>
{
    public void Configure(EntityTypeBuilder<ErrorInformation> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(p => p.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.HasOne(p => p.Device)
            .WithMany()
            .HasForeignKey(p => p.DeviceId);
    }
}
