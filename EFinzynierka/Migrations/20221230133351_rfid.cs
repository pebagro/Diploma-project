using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFinzynierka.Migrations
{
    /// <inheritdoc />
    public partial class rfid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Employee_EmployeeModelId",
                table: "Shift");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeModelId",
                table: "Shift",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "RFIDLog",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RFIDCardID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEntry = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RFIDLog", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RFIDLog_Employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RFIDLog_EmployeeID",
                table: "RFIDLog",
                column: "EmployeeID");

            /*migrationBuilder.AddForeignKey(
                name: "FK_Shift_Employee_EmployeeModelId",
                table: "Shift",
                column: "EmployeeModelId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Employee_EmployeeModelId",
                table: "Shift");

            migrationBuilder.DropTable(
                name: "RFIDLog");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeModelId",
                table: "Shift",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_Employee_EmployeeModelId",
                table: "Shift",
                column: "EmployeeModelId",
                principalTable: "Employee",
                principalColumn: "Id");
        }
    }
}
