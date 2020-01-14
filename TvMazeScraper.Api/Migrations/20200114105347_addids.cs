using Microsoft.EntityFrameworkCore.Migrations;

namespace TvMazeScraper.Api.Migrations
{
    public partial class addids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShowId",
                table: "Shows",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CastMemberId",
                table: "CastMembers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowId",
                table: "Shows");

            migrationBuilder.DropColumn(
                name: "CastMemberId",
                table: "CastMembers");
        }
    }
}
