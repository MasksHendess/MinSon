using Microsoft.EntityFrameworkCore.Migrations;

namespace MinSon.Migrations
{
    public partial class userPropsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "discordUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MfAEnabled",
                table: "discordUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "discordUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "loacal",
                table: "discordUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "discordUsers");

            migrationBuilder.DropColumn(
                name: "MfAEnabled",
                table: "discordUsers");

            migrationBuilder.DropColumn(
                name: "Verified",
                table: "discordUsers");

            migrationBuilder.DropColumn(
                name: "loacal",
                table: "discordUsers");
        }
    }
}
