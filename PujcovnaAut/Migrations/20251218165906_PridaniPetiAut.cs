using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PujcovnaAut.Migrations
{
    /// <inheritdoc />
    public partial class PridaniPetiAut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Auta",
                columns: new[] { "AutoId", "KategorieId", "Model", "SPZ", "Stav", "Znacka" },
                values: new object[,]
                {
                    { 1, 1, "Škoda Fabia III", "1P1 1234", 0, "" },
                    { 2, 2, "Škoda Octavia IV", "2B5 5678", 0, "" },
                    { 3, 1, "Hyundai i30", "3A2 9012", 0, "" },
                    { 4, 2, "VW Golf VIII", "4C8 3456", 0, "" },
                    { 5, 1, "Kia Ceed", "5E7 7890", 0, "" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Auta",
                keyColumn: "AutoId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Auta",
                keyColumn: "AutoId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Auta",
                keyColumn: "AutoId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Auta",
                keyColumn: "AutoId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Auta",
                keyColumn: "AutoId",
                keyValue: 5);
        }
    }
}
