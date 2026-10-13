using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApi.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Books",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Genre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                PublishedYear = table.Column<int>(type: "int", nullable: false),
                PurchasePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                InternalNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                StorageLocation = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Books", x => x.Id);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Books");
    }
}
