using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class remove_finalsalary_from_payroll_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalSalary",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "ScheduledClockInTime",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "WorkingHours",
                table: "Attendances");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FinalSalary",
                table: "Payrolls",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "ScheduledClockInTime",
                table: "Attendances",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "WorkingHours",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
