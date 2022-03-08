using Microsoft.EntityFrameworkCore.Migrations;

namespace MinSon.Migrations
{
    public partial class brandName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "showcaseProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");
        }
    }
}
