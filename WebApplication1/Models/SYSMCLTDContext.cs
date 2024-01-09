using System;
using System.Collections.Generic;
using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;

namespace WebApplication1.Models
{
    public partial class CusttomersController : DbContext
    {
        public CusttomersController()
        {
        }

        public CusttomersController(DbContextOptions<CusttomersController> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
      
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=.;Database=SYSMCLTD;Trusted_Connection=True;Encrypt=False;");
                optionsBuilder.UseSqlServer("Server=DESKTOP-TM9HHHO\\SQLEXPRESS; Database=SYSMCLTD; Trusted_Connection=True; TrustServerCertificate=True;", builder =>
                {
                    builder.EnableRetryOnFailure(100, TimeSpan.FromSeconds(10), null);
                });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Created)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Street).HasMaxLength(50);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Addresses__Custo__3C69FB99");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.Created)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.OfficeNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Contacts__Custom__403A8C7D");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Created)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CustomerNumber).HasDefaultValueSql("(newid())");

                entity.Property(e => e.NameFull).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
