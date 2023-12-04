using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WembleyScada.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddErrorStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ErrorInformations",
                table: "ErrorInformations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ErrorInformations");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "ErrorInformations");

            migrationBuilder.DropColumn(
                name: "ShiftNumber",
                table: "ErrorInformations");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "ErrorInformations");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "ErrorInformations");

            migrationBuilder.AlterColumn<string>(
                name: "ErrorId",
                table: "ErrorInformations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ErrorInformations",
                table: "ErrorInformations",
                column: "ErrorId");

            migrationBuilder.CreateTable(
                name: "ErrorStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ErrorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    ShiftNumber = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErrorStatus_ErrorInformations_ErrorId",
                        column: x => x.ErrorId,
                        principalTable: "ErrorInformations",
                        principalColumn: "ErrorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorStatus_ErrorId",
                table: "ErrorStatus",
                column: "ErrorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ErrorInformations",
                table: "ErrorInformations");

            migrationBuilder.AlterColumn<string>(
                name: "ErrorId",
                table: "ErrorInformations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ErrorInformations",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "ErrorInformations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ShiftNumber",
                table: "ErrorInformations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "ErrorInformations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "ErrorInformations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ErrorInformations",
                table: "ErrorInformations",
                column: "Id");
        }
    }
}
