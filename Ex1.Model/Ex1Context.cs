using System;
using System.Collections.Generic;
using Ex1.Model.Model;
using Microsoft.EntityFrameworkCore;

namespace Ex1.Model;

public partial class Ex1Context : DbContext
{
    public Ex1Context()
    {
    }

    public Ex1Context(DbContextOptions<Ex1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=my_host;Database=my_db;Username=my_user;Password=my_pw");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BornDate).HasColumnName("born_date");
            entity.Property(e => e.EducationMaxReached)
                .HasColumnType("character varying")
                .HasColumnName("education_max_reached");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasColumnType("character varying")
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasColumnType("character varying")
                .HasColumnName("gender");
            entity.Property(e => e.Interests)
                .HasColumnType("character varying")
                .HasColumnName("interests");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
