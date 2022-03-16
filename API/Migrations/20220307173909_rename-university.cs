using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class renameuniversity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_education_tb_m_univeristy_University_Id",
                table: "tb_tr_education");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_univeristy",
                table: "tb_m_univeristy");

            migrationBuilder.RenameTable(
                name: "tb_m_univeristy",
                newName: "tb_m_university");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_university",
                table: "tb_m_university",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_education_tb_m_university_University_Id",
                table: "tb_tr_education",
                column: "University_Id",
                principalTable: "tb_m_university",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_education_tb_m_university_University_Id",
                table: "tb_tr_education");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_university",
                table: "tb_m_university");

            migrationBuilder.RenameTable(
                name: "tb_m_university",
                newName: "tb_m_univeristy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_univeristy",
                table: "tb_m_univeristy",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_education_tb_m_univeristy_University_Id",
                table: "tb_tr_education",
                column: "University_Id",
                principalTable: "tb_m_univeristy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
