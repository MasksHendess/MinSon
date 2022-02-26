using Microsoft.EntityFrameworkCore.Migrations;

namespace MinSon.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cards",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    set = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    imageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    artist = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cmc = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    manaCost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    flavourText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rarity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    power = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    toughness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isMainBoard = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cards", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cards");
        }
    }
}
