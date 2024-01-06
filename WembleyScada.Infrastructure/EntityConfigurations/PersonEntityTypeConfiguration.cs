using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WembleyScada.Domain.AggregateModels.PersonAggregate;

namespace WembleyScada.Infrastructure.EntityConfigurations;

public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(p => p.PersonId);

        builder.HasMany(p => p.WorkRecords).WithOne(p => p.Person).HasForeignKey(p => p.PersonId);
    }
}
