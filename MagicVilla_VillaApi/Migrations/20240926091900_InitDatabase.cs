using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_VillaApi.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Villas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    Sqft = table.Column<int>(type: "int", nullable: false),
                    Occupancy = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amenity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedAt", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Swimming pool, jacuzzi, gym", new DateTime(2024, 9, 26, 12, 18, 58, 963, DateTimeKind.Local).AddTicks(6823), "This is a beautiful villa with 1 bedroom and 1 bathroom.", "villa1.jpg", "Villa 1", 5, 101.0, 2001, new DateTime(2024, 9, 26, 12, 18, 58, 963, DateTimeKind.Local).AddTicks(6873) },
                    { 2, "Swimming pool, jacuzzi, gym", new DateTime(2024, 9, 26, 12, 18, 58, 963, DateTimeKind.Local).AddTicks(6876), "This is a beautiful villa with 2 bedrooms and 2 bathrooms.", "villa2.jpg", "Villa 2", 6, 102.0, 2002, new DateTime(2024, 9, 26, 12, 18, 58, 963, DateTimeKind.Local).AddTicks(6877) },
                    { 3, "Swimming pool, jacuzzi, gym", new DateTime(2024, 9, 26, 12, 18, 58, 963, DateTimeKind.Local).AddTicks(6879), "This is a beautiful villa with 3 bedrooms and 3 bathrooms.", "villa3.jpg", "Villa 3", 7, 103.0, 2003, new DateTime(2024, 9, 26, 12, 18, 58, 963, DateTimeKind.Local).AddTicks(6881) },
                    { 4, "Swimming pool, jacuzzi, gym", new DateTime(2024, 9, 26, 12, 18, 58, 963, DateTimeKind.Local).AddTicks(6883), "This is a beautiful villa with 4 bedrooms and 4 bathrooms.", "villa4.jpg", "Villa 4", 8, 104.0, 2004, new DateTime(2024, 9, 26, 12, 18, 58, 963, DateTimeKind.Local).AddTicks(6884) },
                    { 5, "Swimming pool, jacuzzi, gym", new DateTime(2024, 9, 26, 12, 18, 58, 963, DateTimeKind.Local).AddTicks(6886), "This is a beautiful villa with 5 bedrooms and 5 bathrooms.", "villa5.jpg", "Villa 5", 9, 105.0, 2005, new DateTime(2024, 9, 26, 12, 18, 58, 963, DateTimeKind.Local).AddTicks(6887) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Villas");
        }
    }
}
