using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Epizon.Migrations
{
    public partial class UpdateOrdineArticolo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articoli_Utenti_RivenditoreId",
                table: "Articoli");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordini_Utenti_CompratoreId",
                table: "Ordini");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordini_Utenti_RivenditoreId",
                table: "Ordini");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Utenti",
                table: "Utenti");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Utenti");

            migrationBuilder.RenameTable(
                name: "Utenti",
                newName: "Utente");

            migrationBuilder.AlterColumn<string>(
                name: "Ruolo",
                table: "Utente",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Utente",
                table: "Utente",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Articoli_Utente_RivenditoreId",
                table: "Articoli",
                column: "RivenditoreId",
                principalTable: "Utente",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction); // Modificato qui

            migrationBuilder.AddForeignKey(
                name: "FK_Ordini_Utente_CompratoreId",
                table: "Ordini",
                column: "CompratoreId",
                principalTable: "Utente",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction); // Modificato qui

            migrationBuilder.AddForeignKey(
                name: "FK_Ordini_Utente_RivenditoreId",
                table: "Ordini",
                column: "RivenditoreId",
                principalTable: "Utente",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction); // Modificato qui
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articoli_Utente_RivenditoreId",
                table: "Articoli");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordini_Utente_CompratoreId",
                table: "Ordini");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordini_Utente_RivenditoreId",
                table: "Ordini");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Utente",
                table: "Utente");

            migrationBuilder.RenameTable(
                name: "Utente",
                newName: "Utenti");

            migrationBuilder.AlterColumn<string>(
                name: "Ruolo",
                table: "Utenti",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Utenti",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Utenti",
                table: "Utenti",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Articoli_Utenti_RivenditoreId",
                table: "Articoli",
                column: "RivenditoreId",
                principalTable: "Utenti",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction); // Modificato qui

            migrationBuilder.AddForeignKey(
                name: "FK_Ordini_Utenti_CompratoreId",
                table: "Ordini",
                column: "CompratoreId",
                principalTable: "Utenti",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction); // Modificato qui

            migrationBuilder.AddForeignKey(
                name: "FK_Ordini_Utenti_RivenditoreId",
                table: "Ordini",
                column: "RivenditoreId",
                principalTable: "Utenti",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction); // Modificato qui
        }
    }
}
