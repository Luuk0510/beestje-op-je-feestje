using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BeestjeOpJeFeestje.Domain
{
    public partial class BeestjeOpJeFeestjeContext : IdentityDbContext
    {
        public BeestjeOpJeFeestjeContext()
        {
        }

        public BeestjeOpJeFeestjeContext(DbContextOptions<BeestjeOpJeFeestjeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<Accessories> Accessories { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<AnimalType> AnimalTypes { get; set; }
        public virtual DbSet<CustomerCardType>  CustomerCardTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=beestje_op_je_feestje;Integrated Security=True;Pooling=False;Encrypt=False;Trust Server Certificate=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("gebruiker");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("voornaam");

                entity.Property(e => e.MiddelName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tussenvoegsel");

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("achternaam");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("adres");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.PhoneNumber).HasColumnName("telefoonnummer");

                entity.Property(e => e.CustomerCard)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("klantenkaart");

                entity.HasOne(d => d.CustomerCardType)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.CustomerCard)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_accounts_typeklantenkaart");
            });

            modelBuilder.Entity<Animal>(entity =>
            {
                entity.ToTable("beestjes");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("naam");

                entity.Property(e => e.Image)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("afbeelding");

                entity.Property(e => e.Price)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("prijs");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.HasOne(d => d.AnimalType)
                    .WithMany(p => p.Animals)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_beestjes_type_naam");

                entity.HasMany(e => e.Bookings)
                    .WithMany(b => b.Animals)
                    .UsingEntity<Dictionary<string, object>>(
                        "BeestjeBoeking",
                        j => j.HasOne<Booking>().WithMany().HasForeignKey("BoekingId"),
                        j => j.HasOne<Animal>().WithMany().HasForeignKey("BeestjeId"),
                        j =>
                        {
                            j.HasKey("BeestjeId", "BoekingId");
                            j.ToTable("beestje_has_boekingen");
                        });
            });

            modelBuilder.Entity<Accessories>(entity =>
            {
                entity.ToTable("accessoires");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("naam");

                entity.Property(e => e.Price)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("prijs");

                entity.HasMany(e => e.Bookings)
                    .WithMany(b => b.Accessories)
                    .UsingEntity<Dictionary<string, object>>(
                        "accessoiresboeking",
                        j => j.HasOne<Booking>().WithMany().HasForeignKey("boeking_id"),
                        j => j.HasOne<Accessories>().WithMany().HasForeignKey("accessoires_id"),
                        j =>
                        {
                            j.HasKey("boeking_id", "accessoires_id");
                            j.ToTable("accessoires_has_boekingen");
                        });
            });


            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("boekingen");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("gebruiker_id");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("datum");

                entity.Property(e => e.TotalPrice).HasColumnName("totaal_prijs");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_boekingen_accounts_id");
            });

            modelBuilder.Entity<AnimalType>(entity =>
            {
                entity.ToTable("beestje_type");

                entity.HasKey(e => e.Name);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("naam");
            });

            modelBuilder.Entity<CustomerCardType>(entity =>
            {
                entity.ToTable("klantenkaart_type");

                entity.HasKey(e => e.Type);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
