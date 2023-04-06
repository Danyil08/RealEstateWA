using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RealEstateWA
{

    public partial class DbRealEstateContext : DbContext
    {
        public DbRealEstateContext()
        {
        }

        public DbRealEstateContext(DbContextOptions<DbRealEstateContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }

        public virtual DbSet<Location> Locations { get; set; }

        public virtual DbSet<Offer> Offers { get; set; }

        public virtual DbSet<Reservation> Reservations { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Server= LAPTOP-PRVPM3BL\\SQLEXPRESS; Database=DB_Real_Estate; Trusted_Connection=True; Trust Server Certificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Title)
                    .HasMaxLength(31)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Area)
                    .HasMaxLength(31)
                    .IsUnicode(false);
                entity.Property(e => e.BrieflyAbout)
                    .HasMaxLength(1023)
                    .IsUnicode(false);
                entity.Property(e => e.CityId).HasColumnName("CityID");
                entity.Property(e => e.Street)
                    .HasMaxLength(31)
                    .IsUnicode(false);

                entity.HasOne(d => d.City).WithMany(p => p.Locations)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Location_City");
            });

            modelBuilder.Entity<Offer>(entity =>
            {
                entity.ToTable("Offer");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Description)
                    .HasMaxLength(1023)
                    .IsUnicode(false);
                entity.Property(e => e.EstateType)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.LocationId).HasColumnName("LocationID");
                entity.Property(e => e.PostDate).HasColumnType("date");
                entity.Property(e => e.Price).HasColumnType("smallmoney");
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Location).WithMany(p => p.Offers)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Offer_Location");

                entity.HasOne(d => d.User).WithMany(p => p.Offers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Offer_User");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("Reservation");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.FinDate).HasColumnType("date");
                entity.Property(e => e.OfferId).HasColumnName("OfferID");
                entity.Property(e => e.Price).HasColumnType("smallmoney");
                entity.Property(e => e.StDate).HasColumnType("date");
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Offer).WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.OfferId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_Offer");

                entity.HasOne(d => d.User).WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.FullName)
                    .HasMaxLength(63)
                    .IsUnicode(false);
                entity.Property(e => e.Overview)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.PhNumber)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.RegistrationDate).HasColumnType("date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}