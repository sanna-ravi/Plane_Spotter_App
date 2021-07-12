using Microsoft.EntityFrameworkCore.Migrations;

namespace Spotters.Data.Migrations
{
    public partial class SpotterTableUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpotterImageUrl",
                table: "PlaneSpotters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpotterImageUrl",
                table: "PlaneSpotters");
        }
    }
}
