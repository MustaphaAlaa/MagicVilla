using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_VillaApi.Migrations
{
    /// <inheritdoc />
    public partial class VillaNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "villaNumbers",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    SpecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_villaNumbers", x => x.VillaNo);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 1, 2, 54, 9, 619, DateTimeKind.Local).AddTicks(2702), new DateTime(2024, 10, 1, 2, 54, 9, 619, DateTimeKind.Local).AddTicks(2704) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 1, 2, 54, 9, 619, DateTimeKind.Local).AddTicks(2707), new DateTime(2024, 10, 1, 2, 54, 9, 619, DateTimeKind.Local).AddTicks(2709) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 1, 2, 54, 9, 619, DateTimeKind.Local).AddTicks(2711), new DateTime(2024, 10, 1, 2, 54, 9, 619, DateTimeKind.Local).AddTicks(2712) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 1, 2, 54, 9, 619, DateTimeKind.Local).AddTicks(2715), new DateTime(2024, 10, 1, 2, 54, 9, 619, DateTimeKind.Local).AddTicks(2716) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 1, 2, 54, 9, 619, DateTimeKind.Local).AddTicks(2718), new DateTime(2024, 10, 1, 2, 54, 9, 619, DateTimeKind.Local).AddTicks(2719) });

            migrationBuilder.InsertData(
                table: "villaNumbers",
                columns: new[] { "VillaNo", "CreatedAt", "SpecialDetails", "UpdatedAt" },
                values: new object[,]
                {
                    { 102, new DateTime(2024, 10, 1, 2, 54, 9, 619, DateTimeKind.Local).AddTicks(2522), "Test", new DateTime(2024, 10, 1, 2, 54, 9, 619, DateTimeKind.Local).AddTicks(2588) },
                    { 103, new DateTime(2024, 10, 1, 2, 54, 9, 619, DateTimeKind.Local).AddTicks(2590), "Test", new DateTime(2024, 10, 1, 2, 54, 9, 619, DateTimeKind.Local).AddTicks(2592) },
                    { 202, new DateTime(2024, 10, 1, 2, 54, 9, 619, DateTimeKind.Local).AddTicks(2596), "Test", new DateTime(2024, 10, 1, 2, 54, 9, 619, DateTimeKind.Local).AddTicks(2598) },
                    { 303, new DateTime(2024, 10, 1, 2, 54, 9, 619, DateTimeKind.Local).AddTicks(2593), "Test", new DateTime(2024, 10, 1, 2, 54, 9, 619, DateTimeKind.Local).AddTicks(2595) },
                    { 508, new DateTime(2024, 10, 1, 2, 54, 9, 619, DateTimeKind.Local).AddTicks(2600), "Test", new DateTime(2024, 10, 1, 2, 54, 9, 619, DateTimeKind.Local).AddTicks(2601) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "villaNumbers");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 26, 12, 18, 58, 963, DateTimeKind.Local).AddTicks(6823), new DateTime(2024, 9, 26, 12, 18, 58, 963, DateTimeKind.Local).AddTicks(6873) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 26, 12, 18, 58, 963, DateTimeKind.Local).AddTicks(6876), new DateTime(2024, 9, 26, 12, 18, 58, 963, DateTimeKind.Local).AddTicks(6877) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 26, 12, 18, 58, 963, DateTimeKind.Local).AddTicks(6879), new DateTime(2024, 9, 26, 12, 18, 58, 963, DateTimeKind.Local).AddTicks(6881) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 26, 12, 18, 58, 963, DateTimeKind.Local).AddTicks(6883), new DateTime(2024, 9, 26, 12, 18, 58, 963, DateTimeKind.Local).AddTicks(6884) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 26, 12, 18, 58, 963, DateTimeKind.Local).AddTicks(6886), new DateTime(2024, 9, 26, 12, 18, 58, 963, DateTimeKind.Local).AddTicks(6887) });
        }
    }
}
