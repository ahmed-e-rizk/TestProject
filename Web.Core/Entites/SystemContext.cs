using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Web.Core.Entites;

public partial class SystemContext : DbContext
{
    public SystemContext()
    {
    }

    public SystemContext(DbContextOptions<SystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Phone> Phones { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SystemTest;Integrated Security=True;Connect Timeout=300;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Address");

            entity.Property(e => e.Address1)
                .HasMaxLength(200)
                .HasColumnName("Address");

            entity.HasOne(d => d.User).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Address_Users");
        });

        modelBuilder.Entity<Phone>(entity =>
        {
            entity.ToTable("Phone");

            entity.Property(e => e.Number).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.Phones)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Phone_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.CreatetionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
