using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mojo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMoisAmortissements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MoisAmortissements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmortissementId = table.Column<int>(type: "int", nullable: false),
                    NumeroMois = table.Column<int>(type: "int", nullable: false),
                    Montant = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActif = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoisAmortissements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoisAmortissements_Amortissements_AmortissementId",
                        column: x => x.AmortissementId,
                        principalTable: "Amortissements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoisAmortissements_AmortissementId_NumeroMois",
                table: "MoisAmortissements",
                columns: new[] { "AmortissementId", "NumeroMois" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoisAmortissements");
        }
    }
}
