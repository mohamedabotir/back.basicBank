using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.BasicBank.Migrations
{
    public partial class modifyTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transfers_RecieverId",
                table: "Transfers");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_RecieverId",
                table: "Transfers",
                column: "RecieverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transfers_RecieverId",
                table: "Transfers");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_RecieverId",
                table: "Transfers",
                column: "RecieverId",
                unique: true);
        }
    }
}
