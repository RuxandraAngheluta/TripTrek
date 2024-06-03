using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TripTrek.Models;

public partial class PiiContext : DbContext
{
    public PiiContext()
    {
    }

    public PiiContext(DbContextOptions<PiiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Suggestion> Suggestions { get; set; }

    public virtual DbSet<TouristSpot> TouristSpots { get; set; }

    public virtual DbSet<Transport> Transports { get; set; }

    public virtual DbSet<TransportType> TransportTypes { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-D9KIOU8;Database=PII;Trusted_connection=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Account");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Address");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.Number).HasMaxLength(50);
            entity.Property(e => e.Street).HasMaxLength(50);
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Hotel");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PricePerNight).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Address).WithMany(p => p.Hotels)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_Hotels_Addresses");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Location");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.Street).HasMaxLength(50);
            entity.Property(e => e.TotalRating).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<Suggestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Suggestion");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Contra).HasMaxLength(50);
            entity.Property(e => e.Pro).HasMaxLength(50);
            entity.Property(e => e.Userid).HasColumnName("userid");
        });

        modelBuilder.Entity<TouristSpot>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.TicketPrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Address).WithMany(p => p.TouristSpots)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_TouristSpots_Addresses");
        });

        modelBuilder.Entity<Transport>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Duration).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Type).WithMany(p => p.Transports)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_Transports_TransportTypes");
        });

        modelBuilder.Entity<TransportType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Trip");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Budget).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Trips)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("FK_Trips_Hotels");

            entity.HasOne(d => d.Location).WithMany(p => p.Trips)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Trips_Locations");

            entity.HasOne(d => d.TouristSpots).WithMany(p => p.Trips)
                .HasForeignKey(d => d.TouristSpotsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Trips_TouristSpots");

            entity.HasOne(d => d.Transport).WithMany(p => p.Trips)
                .HasForeignKey(d => d.TransportId)
                .HasConstraintName("FK_Trips_Transport");

            entity.HasOne(d => d.User).WithMany(p => p.Trips)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Trips_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_User");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);

            entity.HasOne(d => d.Account).WithMany(p => p.Users)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Accounts");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
