﻿// <auto-generated />
using System;
using DAL.App.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.App.EF.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("App.Domain.TravelModels.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ReservationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ReservationId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("App.Domain.TravelModels.Legs", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RouteInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TravelPricesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RouteInfoId");

                    b.HasIndex("TravelPricesId");

                    b.ToTable("Legs");
                });

            modelBuilder.Entity("App.Domain.TravelModels.Planet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Name")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Planets");
                });

            modelBuilder.Entity("App.Domain.TravelModels.Provider", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("FlightEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FlightStart")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LegsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("LegsId");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("App.Domain.TravelModels.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalQuotedPrice")
                        .HasColumnType("float");

                    b.Property<int>("TotalQuotedTravelTimeInMinutes")
                        .HasColumnType("int");

                    b.Property<Guid>("TravelPricesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TravelPricesId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("App.Domain.TravelModels.RouteInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Distance")
                        .HasColumnType("bigint");

                    b.Property<Guid>("FromId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ReservationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ToId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FromId");

                    b.HasIndex("ReservationId");

                    b.HasIndex("ToId");

                    b.ToTable("RouteInfos");
                });

            modelBuilder.Entity("App.Domain.TravelModels.RouteInfoData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProviderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ReservationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RouteInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProviderId");

                    b.HasIndex("ReservationId");

                    b.HasIndex("RouteInfoId");

                    b.ToTable("RouteInfoData");
                });

            modelBuilder.Entity("App.Domain.TravelModels.TravelPrices", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ValidUntil")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("TravelPrices");
                });

            modelBuilder.Entity("App.Domain.TravelModels.Company", b =>
                {
                    b.HasOne("App.Domain.TravelModels.Reservation", "Reservation")
                        .WithMany()
                        .HasForeignKey("ReservationId");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("App.Domain.TravelModels.Legs", b =>
                {
                    b.HasOne("App.Domain.TravelModels.RouteInfo", "RouteInfo")
                        .WithMany()
                        .HasForeignKey("RouteInfoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("App.Domain.TravelModels.TravelPrices", "TravelPrices")
                        .WithMany("Legs")
                        .HasForeignKey("TravelPricesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RouteInfo");

                    b.Navigation("TravelPrices");
                });

            modelBuilder.Entity("App.Domain.TravelModels.Provider", b =>
                {
                    b.HasOne("App.Domain.TravelModels.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("App.Domain.TravelModels.Legs", "Legs")
                        .WithMany("Providers")
                        .HasForeignKey("LegsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Legs");
                });

            modelBuilder.Entity("App.Domain.TravelModels.Reservation", b =>
                {
                    b.HasOne("App.Domain.TravelModels.TravelPrices", "TravelPrice")
                        .WithMany()
                        .HasForeignKey("TravelPricesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TravelPrice");
                });

            modelBuilder.Entity("App.Domain.TravelModels.RouteInfo", b =>
                {
                    b.HasOne("App.Domain.TravelModels.Planet", "From")
                        .WithMany()
                        .HasForeignKey("FromId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("App.Domain.TravelModels.Reservation", "Reservation")
                        .WithMany()
                        .HasForeignKey("ReservationId");

                    b.HasOne("App.Domain.TravelModels.Planet", "To")
                        .WithMany()
                        .HasForeignKey("ToId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("From");

                    b.Navigation("Reservation");

                    b.Navigation("To");
                });

            modelBuilder.Entity("App.Domain.TravelModels.RouteInfoData", b =>
                {
                    b.HasOne("App.Domain.TravelModels.Provider", "Provider")
                        .WithMany("RouteInfoData")
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("App.Domain.TravelModels.Reservation", null)
                        .WithMany("RouteInfoData")
                        .HasForeignKey("ReservationId");

                    b.HasOne("App.Domain.TravelModels.RouteInfo", "RouteInfo")
                        .WithMany("RouteInfoData")
                        .HasForeignKey("RouteInfoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Provider");

                    b.Navigation("RouteInfo");
                });

            modelBuilder.Entity("App.Domain.TravelModels.Legs", b =>
                {
                    b.Navigation("Providers");
                });

            modelBuilder.Entity("App.Domain.TravelModels.Provider", b =>
                {
                    b.Navigation("RouteInfoData");
                });

            modelBuilder.Entity("App.Domain.TravelModels.Reservation", b =>
                {
                    b.Navigation("RouteInfoData");
                });

            modelBuilder.Entity("App.Domain.TravelModels.RouteInfo", b =>
                {
                    b.Navigation("RouteInfoData");
                });

            modelBuilder.Entity("App.Domain.TravelModels.TravelPrices", b =>
                {
                    b.Navigation("Legs");
                });
#pragma warning restore 612, 618
        }
    }
}
