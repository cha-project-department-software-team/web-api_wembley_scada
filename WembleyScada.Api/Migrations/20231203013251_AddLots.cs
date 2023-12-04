using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WembleyScada.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddLots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LotId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LotSize = table.Column<int>(type: "int", nullable: false),
                    ReferenceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lot_References_ReferenceId",
                        column: x => x.ReferenceId,
                        principalTable: "References",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lot_LotId",
                table: "Lot",
                column: "LotId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lot_ReferenceId",
                table: "Lot",
                column: "ReferenceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lot");
        }
    }
}
