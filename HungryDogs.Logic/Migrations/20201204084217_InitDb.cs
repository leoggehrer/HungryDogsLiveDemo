using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HungryDogs.Logic.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Restaurant",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UniqueName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpeningHour",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    Weekday = table.Column<int>(type: "int", nullable: false),
                    OpenFrom = table.Column<TimeSpan>(type: "time", nullable: false),
                    OpenTo = table.Column<TimeSpan>(type: "time", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningHour", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpeningHour_Restaurant_RestaurantId",
                        column: x => x.RestaurantId,
                        principalSchema: "dbo",
                        principalTable: "Restaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecialOpeningHour",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    From = table.Column<DateTime>(type: "datetime2", nullable: true),
                    To = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialOpeningHour", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialOpeningHour_Restaurant_RestaurantId",
                        column: x => x.RestaurantId,
                        principalSchema: "dbo",
                        principalTable: "Restaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHour_RestaurantId",
                schema: "dbo",
                table: "OpeningHour",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_UniqueName",
                schema: "dbo",
                table: "Restaurant",
                column: "UniqueName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpecialOpeningHour_RestaurantId",
                schema: "dbo",
                table: "SpecialOpeningHour",
                column: "RestaurantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpeningHour",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SpecialOpeningHour",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Restaurant",
                schema: "dbo");
        }
    }
}
