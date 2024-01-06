using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WembleyScada.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonWorkRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Devices_DeviceId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_DeviceId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Persons");

            migrationBuilder.CreateTable(
                name: "PersonWorkRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeviceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonWorkRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonWorkRecord_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonWorkRecord_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonWorkRecord_DeviceId",
                table: "PersonWorkRecord",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonWorkRecord_PersonId",
                table: "PersonWorkRecord",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonWorkRecord");

            migrationBuilder.AddColumn<string>(
                name: "DeviceId",
                table: "Persons",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_DeviceId",
                table: "Persons",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Devices_DeviceId",
                table: "Persons",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "DeviceId");
        }
    }
}
