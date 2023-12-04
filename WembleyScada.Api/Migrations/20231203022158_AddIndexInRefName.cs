using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WembleyScada.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexInRefName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RefName",
                table: "References",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_References_RefName",
                table: "References",
                column: "RefName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_References_RefName",
                table: "References");

            migrationBuilder.AlterColumn<string>(
                name: "RefName",
                table: "References",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
