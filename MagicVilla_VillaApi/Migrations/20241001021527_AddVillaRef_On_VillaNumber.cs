using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_VillaApi.Migrations
{
    /// <inheritdoc />
    public partial class AddVillaRef_On_VillaNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "villaNumbers",
                keyColumn: "VillaNo",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "villaNumbers",
                keyColumn: "VillaNo",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "villaNumbers",
                keyColumn: "VillaNo",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "villaNumbers",
                keyColumn: "VillaNo",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "villaNumbers",
                keyColumn: "VillaNo",
                keyValue: 508);

            migrationBuilder.AddColumn<int>(
                name: "VillaId",
                table: "villaNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 1, 5, 15, 26, 449, DateTimeKind.Local).AddTicks(2836), new DateTime(2024, 10, 1, 5, 15, 26, 449, DateTimeKind.Local).AddTicks(2900) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 1, 5, 15, 26, 449, DateTimeKind.Local).AddTicks(2904), new DateTime(2024, 10, 1, 5, 15, 26, 449, DateTimeKind.Local).AddTicks(2906) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 1, 5, 15, 26, 449, DateTimeKind.Local).AddTicks(2908), new DateTime(2024, 10, 1, 5, 15, 26, 449, DateTimeKind.Local).AddTicks(2909) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 1, 5, 15, 26, 449, DateTimeKind.Local).AddTicks(2912), new DateTime(2024, 10, 1, 5, 15, 26, 449, DateTimeKind.Local).AddTicks(2913) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 1, 5, 15, 26, 449, DateTimeKind.Local).AddTicks(2915), new DateTime(2024, 10, 1, 5, 15, 26, 449, DateTimeKind.Local).AddTicks(2917) });

            migrationBuilder.CreateIndex(
                name: "IX_villaNumbers_VillaId",
                table: "villaNumbers",
                column: "VillaId");

            migrationBuilder.AddForeignKey(
                name: "FK_villaNumbers_Villas_VillaId",
                table: "villaNumbers",
                column: "VillaId",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_villaNumbers_Villas_VillaId",
                table: "villaNumbers");

            migrationBuilder.DropIndex(
                name: "IX_villaNumbers_VillaId",
                table: "villaNumbers");

            migrationBuilder.DropColumn(
                name: "VillaId",
                table: "villaNumbers");

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
    }
}
