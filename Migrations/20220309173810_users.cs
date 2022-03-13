using Microsoft.EntityFrameworkCore.Migrations;

namespace MinSon.Migrations
{
    public partial class users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "discordUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvatarHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultAvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mention = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discordUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "showcaseProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    webbUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    brandName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    showcase_Item1_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    showcase_Item1_Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    birthYear = table.Column<int>(type: "int", nullable: false),
                    owners = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    webbImage_Item1 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_showcaseProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "zeldaQuotes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    character = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    quote = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_zeldaQuotes", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "discordUsers");

            migrationBuilder.DropTable(
                name: "showcaseProducts");

            migrationBuilder.DropTable(
                name: "zeldaQuotes");
        }
    }
}
