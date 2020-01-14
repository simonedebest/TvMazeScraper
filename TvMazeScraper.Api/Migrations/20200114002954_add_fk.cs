using Microsoft.EntityFrameworkCore.Migrations;

namespace TvMazeScraper.Api.Migrations
{
    public partial class add_fk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CastMembers_Shows_ShowEntityId",
                table: "CastMembers");

            migrationBuilder.AlterColumn<int>(
                name: "ShowEntityId",
                table: "CastMembers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CastMembers_Shows_ShowEntityId",
                table: "CastMembers",
                column: "ShowEntityId",
                principalTable: "Shows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CastMembers_Shows_ShowEntityId",
                table: "CastMembers");

            migrationBuilder.AlterColumn<int>(
                name: "ShowEntityId",
                table: "CastMembers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_CastMembers_Shows_ShowEntityId",
                table: "CastMembers",
                column: "ShowEntityId",
                principalTable: "Shows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
