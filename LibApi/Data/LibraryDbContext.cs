using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Data;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books => Set<Book>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.Property(b => b.Title).HasMaxLength(150).IsRequired();
            entity.Property(b => b.Author).HasMaxLength(100).IsRequired();
            entity.Property(b => b.Genre).HasMaxLength(80).IsRequired();
            entity.Property(b => b.PurchasePrice).HasColumnType("decimal(10,2)");
            entity.Property(b => b.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        });
    }
}
