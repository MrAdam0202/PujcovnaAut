using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PujcovnaAut.Migrations
{
    /// <inheritdoc />
    public partial class OpravaZnacek : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auta",
                keyColumn: "AutoId",
                keyValue: 1,
                columns: new[] { "Model", "Znacka" },
                values: new object[] { "Fabia III", "Škoda" });

            migrationBuilder.UpdateData(
                table: "Auta",
                keyColumn: "AutoId",
                keyValue: 2,
                columns: new[] { "Model", "Znacka" },
                values: new object[] { "Octavia IV", "Škoda" });

            migrationBuilder.UpdateData(
                table: "Auta",
                keyColumn: "AutoId",
                keyValue: 3,
                columns: new[] { "Model", "Znacka" },
                values: new object[] { "i30", "Hyundai" });

            migrationBuilder.UpdateData(
                table: "Auta",
                keyColumn: "AutoId",
                keyValue: 4,
                columns: new[] { "Model", "Znacka" },
                values: new object[] { "Golf VIII", "VW" });

            migrationBuilder.UpdateData(
                table: "Auta",
                keyColumn: "AutoId",
                keyValue: 5,
                columns: new[] { "Model", "Znacka" },
                values: new object[] { "Ceed", "Kia" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auta",
                keyColumn: "AutoId",
                keyValue: 1,
                columns: new[] { "Model", "Znacka" },
                values: new object[] { "Škoda Fabia III", "" });

            migrationBuilder.UpdateData(
                table: "Auta",
                keyColumn: "AutoId",
                keyValue: 2,
                columns: new[] { "Model", "Znacka" },
                values: new object[] { "Škoda Octavia IV", "" });

            migrationBuilder.UpdateData(
                table: "Auta",
                keyColumn: "AutoId",
                keyValue: 3,
                columns: new[] { "Model", "Znacka" },
                values: new object[] { "Hyundai i30", "" });

            migrationBuilder.UpdateData(
                table: "Auta",
                keyColumn: "AutoId",
                keyValue: 4,
                columns: new[] { "Model", "Znacka" },
                values: new object[] { "VW Golf VIII", "" });

            migrationBuilder.UpdateData(
                table: "Auta",
                keyColumn: "AutoId",
                keyValue: 5,
                columns: new[] { "Model", "Znacka" },
                values: new object[] { "Kia Ceed", "" });
        }
    }
}
