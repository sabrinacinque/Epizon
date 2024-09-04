using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Epizon.Migrations
{
    /// <inheritdoc />
    public partial class AddCorrectRivenditoreIdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Utente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ruolo = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Compratore_Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Compratore_Cognome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Compratore_Indirizzo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Compratore_Citta = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Compratore_CAP = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Compratore_Provincia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Compratore_Telefono = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    RagioneSociale = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Cognome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PartitaIva = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Indirizzo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Citta = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CAP = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Provincia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Pec = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CodiceDestinatario = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ordini",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataOrdine = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Totale = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CompratoreId = table.Column<int>(type: "int", nullable: true),
                    RivenditoreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordini", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ordini_Utente_CompratoreId",
                        column: x => x.CompratoreId,
                        principalTable: "Utente",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ordini_Utente_RivenditoreId",
                        column: x => x.RivenditoreId,
                        principalTable: "Utente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    
                });

            migrationBuilder.CreateTable(
                name: "Articoli",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titolo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Descrizione = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Prezzo = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ImmagineCopertina = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Immagine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Immagine3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TempiDiSpedizione = table.Column<int>(type: "int", nullable: true),
                    Categoria = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RivenditoreId = table.Column<int>(type: "int", nullable: true),
                    OrdineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articoli", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articoli_Ordini_OrdineId",
                        column: x => x.OrdineId,
                        principalTable: "Ordini",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Articoli_Utente_RivenditoreId",
                        column: x => x.RivenditoreId,
                        principalTable: "Utente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdineArticoli",
                columns: table => new
                {
                    OrdineId = table.Column<int>(type: "int", nullable: false),
                    ArticoloId = table.Column<int>(type: "int", nullable: false),
                    Quantità = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdineArticoli", x => new { x.OrdineId, x.ArticoloId });
                    table.ForeignKey(
                        name: "FK_OrdineArticoli_Articoli_ArticoloId",
                        column: x => x.ArticoloId,
                        principalTable: "Articoli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdineArticoli_Ordini_OrdineId",
                        column: x => x.OrdineId,
                        principalTable: "Ordini",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articoli_OrdineId",
                table: "Articoli",
                column: "OrdineId");

            migrationBuilder.CreateIndex(
                name: "IX_Articoli_RivenditoreId",
                table: "Articoli",
                column: "RivenditoreId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdineArticoli_ArticoloId",
                table: "OrdineArticoli",
                column: "ArticoloId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordini_CompratoreId",
                table: "Ordini",
                column: "CompratoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordini_RivenditoreId",
                table: "Ordini",
                column: "RivenditoreId");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdineArticoli");

            migrationBuilder.DropTable(
                name: "Articoli");

            migrationBuilder.DropTable(
                name: "Ordini");

            migrationBuilder.DropTable(
                name: "Utente");
        }
    }
}
