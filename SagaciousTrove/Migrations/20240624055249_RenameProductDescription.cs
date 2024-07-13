using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SagaciousTrove.Migrations
{
    public partial class RenameProductDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "description",
                table: "Products",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "description");
        }
    }
}
