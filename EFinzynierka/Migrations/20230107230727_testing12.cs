using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFinzynierka.Migrations
{
    /// <inheritdoc />
    public partial class testing12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RFIDLog_EmployeeID",
                table: "RFIDLog");



            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RFIDLog_EmployeeID",
                table: "RFIDLog");

            migrationBuilder.DropColumn(
                name: "AuthLevel",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "CardInfo",
                table: "Employee");

            migrationBuilder.CreateIndex(
                name: "IX_RFIDLog_EmployeeID",
                table: "RFIDLog",
                column: "EmployeeID");
        }
    }
}
