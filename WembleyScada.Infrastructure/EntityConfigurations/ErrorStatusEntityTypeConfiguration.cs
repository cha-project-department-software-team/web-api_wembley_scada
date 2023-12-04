using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using WembleyScada.Domain.AggregateModels.ErrorInformationAggregate;

namespace WembleyScada.Infrastructure.EntityConfigurations;

public class ErrorStatusEntityTypeConfiguration : IEntityTypeConfiguration<ErrorStatus>
{
    public void Configure(EntityTypeBuilder<ErrorStatus> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();
    }
}
