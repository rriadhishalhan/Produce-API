using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class changeid_roletoRole_Id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_account_role_tb_m_role_Id_Role",
                table: "tb_tr_account_role");

            migrationBuilder.RenameColumn(
                name: "Id_Role",
                table: "tb_tr_account_role",
                newName: "Role_Id");

            migrationBuilder.RenameIndex(
                name: "IX_tb_tr_account_role_Id_Role",
                table: "tb_tr_account_role",
                newName: "IX_tb_tr_account_role_Role_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_account_role_tb_m_role_Role_Id",
                table: "tb_tr_account_role",
                column: "Role_Id",
                principalTable: "tb_m_role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_account_role_tb_m_role_Role_Id",
                table: "tb_tr_account_role");

            migrationBuilder.RenameColumn(
                name: "Role_Id",
                table: "tb_tr_account_role",
                newName: "Id_Role");

            migrationBuilder.RenameIndex(
                name: "IX_tb_tr_account_role_Role_Id",
                table: "tb_tr_account_role",
                newName: "IX_tb_tr_account_role_Id_Role");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_account_role_tb_m_role_Id_Role",
                table: "tb_tr_account_role",
                column: "Id_Role",
                principalTable: "tb_m_role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
