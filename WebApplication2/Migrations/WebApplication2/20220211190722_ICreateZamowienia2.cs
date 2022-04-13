using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations.WebApplication2
{
    public partial class ICreateZamowienia2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zamowienia_Klienci_ClientId",
                table: "Zamowienia");

            migrationBuilder.DropForeignKey(
                name: "FK_Zamowienia_Przedmioty_ItemId",
                table: "Zamowienia");

            migrationBuilder.DropIndex(
                name: "IX_Zamowienia_ClientId",
                table: "Zamowienia");

            migrationBuilder.DropIndex(
                name: "IX_Zamowienia_ItemId",
                table: "Zamowienia");

            migrationBuilder.AddColumn<int>(
                name: "KlienciId",
                table: "Zamowienia",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrzedmiotyId",
                table: "Zamowienia",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StanZamowienia",
                table: "Zamowienia",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Zamowienia_KlienciId",
                table: "Zamowienia",
                column: "KlienciId");

            migrationBuilder.CreateIndex(
                name: "IX_Zamowienia_PrzedmiotyId",
                table: "Zamowienia",
                column: "PrzedmiotyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Zamowienia_Klienci_KlienciId",
                table: "Zamowienia",
                column: "KlienciId",
                principalTable: "Klienci",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zamowienia_Przedmioty_PrzedmiotyId",
                table: "Zamowienia",
                column: "PrzedmiotyId",
                principalTable: "Przedmioty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zamowienia_Klienci_KlienciId",
                table: "Zamowienia");

            migrationBuilder.DropForeignKey(
                name: "FK_Zamowienia_Przedmioty_PrzedmiotyId",
                table: "Zamowienia");

            migrationBuilder.DropIndex(
                name: "IX_Zamowienia_KlienciId",
                table: "Zamowienia");

            migrationBuilder.DropIndex(
                name: "IX_Zamowienia_PrzedmiotyId",
                table: "Zamowienia");

            migrationBuilder.DropColumn(
                name: "KlienciId",
                table: "Zamowienia");

            migrationBuilder.DropColumn(
                name: "PrzedmiotyId",
                table: "Zamowienia");

            migrationBuilder.DropColumn(
                name: "StanZamowienia",
                table: "Zamowienia");

            migrationBuilder.CreateIndex(
                name: "IX_Zamowienia_ClientId",
                table: "Zamowienia",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Zamowienia_ItemId",
                table: "Zamowienia",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Zamowienia_Klienci_ClientId",
                table: "Zamowienia",
                column: "ClientId",
                principalTable: "Klienci",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zamowienia_Przedmioty_ItemId",
                table: "Zamowienia",
                column: "ItemId",
                principalTable: "Przedmioty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
