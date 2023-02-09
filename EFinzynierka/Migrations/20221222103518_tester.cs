using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFinzynierka.Migrations
{
    /// <inheritdoc />
    public partial class tester : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropForeignKey(
                name: "FK_MonthlyModel_Scheduler_SchedulerModelId",
                table: "MonthlyModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Scheduler_Employee_EmployeeModelId",
                table: "Scheduler");

            migrationBuilder.DropIndex(
                name: "IX_Scheduler_EmployeeModelId",
                table: "Scheduler");

            migrationBuilder.DropIndex(
                name: "IX_MonthlyModel_EmployeeModelId",
                table: "MonthlyModel");

            migrationBuilder.DropIndex(
                name: "IX_MonthlyModel_SchedulerModelId",
                table: "MonthlyModel");

            migrationBuilder.DropColumn(
                name: "EmployeeModelId",
                table: "Scheduler");

            migrationBuilder.DropColumn(
                name: "EmployeeModelId",
                table: "MonthlyModel");

            migrationBuilder.DropColumn(
                name: "SchedulerModelId",
                table: "MonthlyModel");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Scheduler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MonthlyId",
                table: "Scheduler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SchedulesId",
                table: "MonthlyModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shift_Employee_EmployeeModelId",
                        column: x => x.EmployeeModelId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Scheduler_EmployeeId",
                table: "Scheduler",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyModel_SchedulesId",
                table: "MonthlyModel",
                column: "SchedulesId");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_EmployeeModelId",
                table: "Shift",
                column: "EmployeeModelId");

           
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonthlyModel_Scheduler_SchedulesId",
                table: "MonthlyModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Scheduler_Employee_EmployeeId",
                table: "Scheduler");

            migrationBuilder.DropTable(
                name: "Shift");

            migrationBuilder.DropIndex(
                name: "IX_Scheduler_EmployeeId",
                table: "Scheduler");

            migrationBuilder.DropIndex(
                name: "IX_MonthlyModel_SchedulesId",
                table: "MonthlyModel");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Scheduler");

            migrationBuilder.DropColumn(
                name: "MonthlyId",
                table: "Scheduler");

            migrationBuilder.DropColumn(
                name: "SchedulesId",
                table: "MonthlyModel");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeModelId",
                table: "Scheduler",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeModelId",
                table: "MonthlyModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SchedulerModelId",
                table: "MonthlyModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scheduler_EmployeeModelId",
                table: "Scheduler",
                column: "EmployeeModelId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyModel_EmployeeModelId",
                table: "MonthlyModel",
                column: "EmployeeModelId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyModel_SchedulerModelId",
                table: "MonthlyModel",
                column: "SchedulerModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonthlyModel_Employee_EmployeeModelId",
                table: "MonthlyModel",
                column: "EmployeeModelId",
                principalTable: "Employee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MonthlyModel_Scheduler_SchedulerModelId",
                table: "MonthlyModel",
                column: "SchedulerModelId",
                principalTable: "Scheduler",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Scheduler_Employee_EmployeeModelId",
                table: "Scheduler",
                column: "EmployeeModelId",
                principalTable: "Employee",
                principalColumn: "Id");
        }
    }
}
