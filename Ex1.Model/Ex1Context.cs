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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BornDate).HasColumnName("born_date");
            entity.Property(e => e.EducationMaxReached)
                .HasColumnType("int")
                .HasColumnName("education_max_reached");
            entity.Property(e => e.Email)
                .HasColumnType("text")
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasColumnType("text")
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasColumnType("int")
                .HasColumnName("gender");
            entity.Property(e => e.Interests)
                .HasColumnType("text")
                .HasColumnName("interests");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
