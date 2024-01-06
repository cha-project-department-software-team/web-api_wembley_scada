﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WembleyScada.Infrastructure;

#nullable disable

namespace WembleyScada.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240105113214_AddWorkStatus")]
    partial class AddWorkStatus
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DeviceProduct", b =>
                {
                    b.Property<string>("DevicesDeviceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("DevicesDeviceId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("DeviceProduct");
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.DeviceAggregate.Device", b =>
                {
                    b.Property<string>("DeviceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DisplayPriority")
                        .HasColumnType("int");

                    b.HasKey("DeviceId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.DeviceReferenceAggregate.DeviceReference", b =>
                {
                    b.Property<string>("DeviceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ReferenceId")
                        .HasColumnType("int");

                    b.HasKey("DeviceId", "ReferenceId");

                    b.HasIndex("ReferenceId");

                    b.ToTable("DeviceReferences");
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.DeviceReferenceAggregate.MFC", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DeviceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReferenceId")
                        .HasColumnType("int");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId", "ReferenceId");

                    b.ToTable("MFC");
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.ErrorInformationAggregate.ErrorInformation", b =>
                {
                    b.Property<string>("ErrorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DeviceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ErrorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ErrorId");

                    b.HasIndex("DeviceId");

                    b.ToTable("ErrorInformations");
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.ErrorInformationAggregate.ErrorStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("ErrorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ShiftNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ErrorId");

                    b.ToTable("ErrorStatus");
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.MachineStatusAggregate.MachineStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeviceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ShiftNumber")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("MachineStatus");
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.PersonAggregate.Person", b =>
                {
                    b.Property<string>("PersonId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PersonName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.PersonAggregate.PersonWorkRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DeviceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("PersonId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("WorkStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("PersonId");

                    b.ToTable("PersonWorkRecord");
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.ProductAggregate.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DeviceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.ReferenceAggregate.Lot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LotId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("LotSize")
                        .HasColumnType("int");

                    b.Property<int>("LotStatus")
                        .HasColumnType("int");

                    b.Property<int?>("ReferenceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("LotId")
                        .IsUnique();

                    b.HasIndex("ReferenceId");

                    b.ToTable("Lot");
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.ReferenceAggregate.Reference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DeviceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("RefName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("RefName")
                        .IsUnique();

                    b.ToTable("References");
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.ShiftReportAggregate.ShiftReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("A")
                        .HasColumnType("float");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DefectCount")
                        .HasColumnType("int");

                    b.Property<string>("DeviceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<TimeSpan>("ElapsedTime")
                        .HasColumnType("time");

                    b.Property<double>("P")
                        .HasColumnType("float");

                    b.Property<int>("ProductCount")
                        .HasColumnType("int");

                    b.Property<int>("ShiftNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("ShiftReports");
                });

            modelBuilder.Entity("DeviceProduct", b =>
                {
                    b.HasOne("WembleyScada.Domain.AggregateModels.DeviceAggregate.Device", null)
                        .WithMany()
                        .HasForeignKey("DevicesDeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WembleyScada.Domain.AggregateModels.ProductAggregate.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.DeviceReferenceAggregate.DeviceReference", b =>
                {
                    b.HasOne("WembleyScada.Domain.AggregateModels.DeviceAggregate.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WembleyScada.Domain.AggregateModels.ReferenceAggregate.Reference", "Reference")
                        .WithMany()
                        .HasForeignKey("ReferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("Reference");
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.DeviceReferenceAggregate.MFC", b =>
                {
                    b.HasOne("WembleyScada.Domain.AggregateModels.DeviceReferenceAggregate.DeviceReference", null)
                        .WithMany("MFCs")
                        .HasForeignKey("DeviceId", "ReferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.ErrorInformationAggregate.ErrorInformation", b =>
                {
                    b.HasOne("WembleyScada.Domain.AggregateModels.DeviceAggregate.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.ErrorInformationAggregate.ErrorStatus", b =>
                {
                    b.HasOne("WembleyScada.Domain.AggregateModels.ErrorInformationAggregate.ErrorInformation", "ErrorInformation")
                        .WithMany("ErrorStatuses")
                        .HasForeignKey("ErrorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ErrorInformation");
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.MachineStatusAggregate.MachineStatus", b =>
                {
                    b.HasOne("WembleyScada.Domain.AggregateModels.DeviceAggregate.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.PersonAggregate.PersonWorkRecord", b =>
                {
                    b.HasOne("WembleyScada.Domain.AggregateModels.DeviceAggregate.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WembleyScada.Domain.AggregateModels.PersonAggregate.Person", "Person")
                        .WithMany("WorkRecords")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.ReferenceAggregate.Lot", b =>
                {
                    b.HasOne("WembleyScada.Domain.AggregateModels.ReferenceAggregate.Reference", null)
                        .WithMany("Lots")
                        .HasForeignKey("ReferenceId");
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.ReferenceAggregate.Reference", b =>
                {
                    b.HasOne("WembleyScada.Domain.AggregateModels.ProductAggregate.Product", "Product")
                        .WithMany("References")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.ShiftReportAggregate.ShiftReport", b =>
                {
                    b.HasOne("WembleyScada.Domain.AggregateModels.DeviceAggregate.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("WembleyScada.Domain.AggregateModels.ShiftReportAggregate.Shot", "Shots", b1 =>
                        {
                            b1.Property<int>("ShiftReportId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<double>("A")
                                .HasColumnType("float");

                            b1.Property<double>("CycleTime")
                                .HasColumnType("float");

                            b1.Property<double>("ExecutionTime")
                                .HasColumnType("float");

                            b1.Property<double>("OEE")
                                .HasColumnType("float");

                            b1.Property<double>("P")
                                .HasColumnType("float");

                            b1.Property<double>("Q")
                                .HasColumnType("float");

                            b1.Property<DateTime>("TimeStamp")
                                .HasColumnType("datetime2");

                            b1.HasKey("ShiftReportId", "Id");

                            b1.ToTable("Shot");

                            b1.WithOwner()
                                .HasForeignKey("ShiftReportId");
                        });

                    b.Navigation("Device");

                    b.Navigation("Shots");
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.DeviceReferenceAggregate.DeviceReference", b =>
                {
                    b.Navigation("MFCs");
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.ErrorInformationAggregate.ErrorInformation", b =>
                {
                    b.Navigation("ErrorStatuses");
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.PersonAggregate.Person", b =>
                {
                    b.Navigation("WorkRecords");
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.ProductAggregate.Product", b =>
                {
                    b.Navigation("References");
                });

            modelBuilder.Entity("WembleyScada.Domain.AggregateModels.ReferenceAggregate.Reference", b =>
                {
                    b.Navigation("Lots");
                });
#pragma warning restore 612, 618
        }
    }
}
