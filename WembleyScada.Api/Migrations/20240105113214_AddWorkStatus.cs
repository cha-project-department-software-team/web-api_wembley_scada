using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WembleyScada.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "PersonWorkRecord");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Lot");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Lot",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "ShiftNumber",
                table: "Lot",
                newName: "LotStatus");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "PersonWorkRecord",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkStatus",
                table: "PersonWorkRecord",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Lot",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "PersonWorkRecord");

            migrationBuilder.DropColumn(
                name: "WorkStatus",
                table: "PersonWorkRecord");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Lot");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Lot",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "LotStatus",
                table: "Lot",
                newName: "ShiftNumber");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "PersonWorkRecord",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Lot",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
