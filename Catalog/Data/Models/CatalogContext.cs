using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Catalog.Data.Models;

public partial class CatalogContext : IdentityDbContext<Userr>
{
    public CatalogContext()
    {
    }

    public CatalogContext(DbContextOptions<CatalogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<FilmActor> FilmActors { get; set; }

    public virtual DbSet<FilmGenre> FilmGenres { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Userr> Userrs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=catalog;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //NEW
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__actor__3213E83F8BC2FE9F");

            entity.ToTable("actor");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Nationality)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("nationality");
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__film__3213E83F1A35FDA1");

            entity.ToTable("film");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Director)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("director");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Title)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.Year).HasColumnName("year");
        });

        modelBuilder.Entity<FilmActor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__filmActo__3213E83F035D2562");

            entity.ToTable("filmActor");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ActorId).HasColumnName("ActorID");
            entity.Property(e => e.FilmId).HasColumnName("filmID");

            entity.HasOne(d => d.Actor).WithMany(p => p.FilmActors)
                .HasForeignKey(d => d.ActorId)
                .HasConstraintName("FK__filmActor__Actor__2C3393D0");

            entity.HasOne(d => d.Film).WithMany(p => p.FilmActors)
                .HasForeignKey(d => d.FilmId)
                .HasConstraintName("FK__filmActor__filmI__2B3F6F97");
        });

        modelBuilder.Entity<FilmGenre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FilmGenr__3213E83F2D59D779");

            entity.ToTable("FilmGenre");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.FilmId).HasColumnName("FilmID");
            entity.Property(e => e.GenreId).HasColumnName("GenreID");

            entity.HasOne(d => d.Film).WithMany(p => p.FilmGenres)
                .HasForeignKey(d => d.FilmId)
                .HasConstraintName("FK__FilmGenre__FilmI__2F10007B");

            entity.HasOne(d => d.Genre).WithMany(p => p.FilmGenres)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK__FilmGenre__Genre__300424B4");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__genre__3213E83F2F38C26D");

            entity.ToTable("genre");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Userr>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__userr__3213E83F76B2E205");

            entity.ToTable("userr");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
        /*    entity.Property(e => e.Password)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("username");
   */     });
  
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
