using LibraryApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibraryApi.Migrations;

[DbContext(typeof(LibraryDbContext))]
partial class LibraryDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.11")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

        modelBuilder.Entity("LibraryApi.Models.Book", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

            b.Property<string>("Author")
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)");

            b.Property<DateTime>("CreatedAt")
                .ValueGeneratedOnAdd()
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETUTCDATE()");

            b.Property<string>("Genre")
                .IsRequired()
                .HasMaxLength(80)
                .HasColumnType("nvarchar(80)");

            b.Property<string>("InternalNotes")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            b.Property<decimal>("PurchasePrice")
                .HasColumnType("decimal(10,2)");

            b.Property<int>("PublishedYear")
                .HasColumnType("int");

            b.Property<string>("StorageLocation")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            b.Property<string>("Title")
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("nvarchar(150)");

            b.HasKey("Id");

            b.ToTable("Books");
        });
#pragma warning restore 612, 618
    }
}
