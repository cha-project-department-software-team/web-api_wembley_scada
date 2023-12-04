using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WembleyScada.Api.Migrations
{
    /// <inheritdoc />
    public partial class fixShot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "A",
                table: "Shot",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OEE",
                table: "Shot",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "P",
                table: "Shot",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Q",
                table: "Shot",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "A",
                table: "ShiftReports",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ElapsedTime",
                table: "ShiftReports",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<double>(
                name: "P",
                table: "ShiftReports",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "A",
                table: "Shot");

            migrationBuilder.DropColumn(
                name: "OEE",
                table: "Shot");

            migrationBuilder.DropColumn(
                name: "P",
                table: "Shot");

            migrationBuilder.DropColumn(
                name: "Q",
                table: "Shot");

            migrationBuilder.DropColumn(
                name: "A",
                table: "ShiftReports");

            migrationBuilder.DropColumn(
                name: "ElapsedTime",
                table: "ShiftReports");

            migrationBuilder.DropColumn(
                name: "P",
                table: "ShiftReports");
        }
    }
}
