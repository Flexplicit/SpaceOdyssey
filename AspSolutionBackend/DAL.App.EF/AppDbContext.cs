using System;
using System.Linq;
using App.Domain.TravelModels;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public AppDbContext() : base()
        {
        }

        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<RouteInfoData> RouteInfoData { get; set; } = null!;

        public virtual DbSet<TravelPrices> TravelPrices { get; set; } = null!;

        public virtual DbSet<Planet> Planets { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Legs> Legs { get; set; } = null!;
        public virtual DbSet<Provider> Providers { get; set; } = null!;
        public virtual DbSet<RouteInfo> RouteInfos { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.NoAction;
            }

            modelBuilder.Entity<Company>()
                .HasMany(company => company.Providers)
                .WithOne(provider => provider.Company!)
                .HasForeignKey(prov => prov.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<RouteInfoData>()
                .HasOne(routeInfoData => routeInfoData.Provider)
                .WithMany(provider => provider!.RouteInfoData!)
                .HasForeignKey(prov => prov.ProviderId)
                .OnDelete(DeleteBehavior.Cascade); 
            
            modelBuilder.Entity<Reservation>()
                .HasOne(reservation => reservation.TravelPrice)
                .WithMany(priceList => priceList!.Reservations!)
                .HasForeignKey(prov => prov.TravelPriceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}