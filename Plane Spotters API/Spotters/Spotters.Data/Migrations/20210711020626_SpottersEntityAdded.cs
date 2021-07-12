using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spotters.Data.Migrations
{
    public partial class SpottersEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlaneSpotters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternalId = table.Column<Guid>(nullable: false, defaultValue: Guid.NewGuid()),
                    CreatedOn = table.Column<DateTime>(nullable: true, defaultValue: DateTime.Now),
                    UpdatedOn = table.Column<DateTime>(nullable: true, defaultValue: DateTime.Now),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Make = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Registration = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    DateandTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaneSpotters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaneSpotters_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PlaneSpotters_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaneSpotters_CreatedBy",
                table: "PlaneSpotters",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PlaneSpotters_UpdatedBy",
                table: "PlaneSpotters",
                column: "UpdatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaneSpotters");
        }
    }
}
