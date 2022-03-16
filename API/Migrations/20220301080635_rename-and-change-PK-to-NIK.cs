using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class renameandchangePKtoNIK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Employees",
                newName: "NIK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NIK",
                table: "Employees",
                newName: "id");
        }
    }
}
