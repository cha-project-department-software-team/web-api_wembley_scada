using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WembleyScada.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Devices_PersonId",
                table: "Persons");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceId",
                table: "Persons",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_DeviceId",
                table: "Persons",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Devices_DeviceId",
                table: "Persons",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "DeviceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Devices_DeviceId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_DeviceId",
                table: "Persons");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceId",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Devices_PersonId",
                table: "Persons",
                column: "PersonId",
                principalTable: "Devices",
                principalColumn: "DeviceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
