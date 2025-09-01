using System;
using System.Collections.Generic;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data;

public partial class LibraryContextDBF : DbContext
{
    public LibraryContextDBF()
    {
    }

    public LibraryContextDBF(DbContextOptions<LibraryContextDBF> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=LibraryManagementDB;Trusted_Connection=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasIndex(e => e.AuthorId, "IX_Books_AuthorId");

            entity.HasOne(d => d.Author).WithMany(p => p.Books).HasForeignKey(d => d.AuthorId);

            entity.HasMany(d => d.GenresGenres).WithMany(p => p.BooksBooks)
                .UsingEntity<Dictionary<string, object>>(
                    "BookGenre",
                    r => r.HasOne<Genre>().WithMany().HasForeignKey("GenresGenreId"),
                    l => l.HasOne<Book>().WithMany().HasForeignKey("BooksBookId"),
                    j =>
                    {
                        j.HasKey("BooksBookId", "GenresGenreId");
                        j.ToTable("BookGenres");
                        j.HasIndex(new[] { "GenresGenreId" }, "IX_BookGenres_GenresGenreId");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
