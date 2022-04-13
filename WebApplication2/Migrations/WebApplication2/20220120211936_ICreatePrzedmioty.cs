using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations.WebApplication2
{
    public partial class ICreatePrzedmioty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Przedmioty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(nullable: true),
                    Ilosc = table.Column<int>(nullable: false),
                    CenaJednostkowa = table.Column<decimal>(nullable: false),
                    Wrazliwy = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Przedmioty", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Przedmioty");
        }
    }
}
