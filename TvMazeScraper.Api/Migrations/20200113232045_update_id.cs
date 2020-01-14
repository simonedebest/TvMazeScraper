using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TvMazeScraper.Api.Migrations
{
    public partial class update_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Shows",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "CastMembers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Shows");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "CastMembers");
        }
    }
}
