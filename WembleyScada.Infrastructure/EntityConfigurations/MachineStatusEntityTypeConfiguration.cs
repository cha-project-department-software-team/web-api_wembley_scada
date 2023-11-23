﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WembleyScada.Domain.AggregateModels.MachineStatusAggregate;

namespace WembleyScada.Infrastructure.EntityConfigurations;

public class MachineStatusEntityTypeConfiguration : IEntityTypeConfiguration<MachineStatus>
{
    public void Configure(EntityTypeBuilder<MachineStatus> builder)
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
