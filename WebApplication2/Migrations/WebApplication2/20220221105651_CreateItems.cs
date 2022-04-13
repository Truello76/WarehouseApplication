using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations.WebApplication2
{
    public partial class CreateItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropColumn(
                name: "StanZamowienia",
                table: "Zamowienia");

            migrationBuilder.AddColumn<string>(
                name: "Adres",
                table: "Klienci",
                nullable: true);*/

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ItemCount = table.Column<long>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    ShelfCode = table.Column<string>(nullable: true),
                    Fragile = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            /*migrationBuilder.DropColumn(
                name: "Adres",
                table: "Klienci");

            migrationBuilder.AddColumn<string>(
                name: "StanZamowienia",
                table: "Zamowienia",
                type: "nvarchar(max)",
                nullable: true);*/
        }
    }
}
