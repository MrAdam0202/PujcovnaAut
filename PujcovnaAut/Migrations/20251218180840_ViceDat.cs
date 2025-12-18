using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PujcovnaAut.Migrations
{
    /// <inheritdoc />
    public partial class ViceDat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Telefon",
                table: "Zakaznici",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Zakaznici",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatumNarozeni",
                table: "Zakaznici",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.InsertData(
                table: "Auta",
                columns: new[] { "AutoId", "KategorieId", "Model", "SPZ", "Stav", "Znacka" },
                values: new object[,]
                {
                    { 6, 3, "A4", "6F9 1122", 0, "Audi" },
                    { 7, 2, "Corolla", "7G8 3344", 0, "Toyota" },
                    { 8, 1, "Focus", "8H7 5566", 0, "Ford" },
                    { 9, 3, "320d", "9I6 7788", 0, "BMW" },
                    { 10, 1, "Clio", "1J5 9900", 0, "Renault" }
                });

            migrationBuilder.InsertData(
                table: "Zakaznici",
                columns: new[] { "ZakaznikId", "DatumNarozeni", "Email", "Jmeno", "Prijmeni", "Telefon" },
                values: new object[,]
                {
                    { 3, new DateTime(1995, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "petr.svoboda@gmail.com", "Petr", "Svoboda", "720111222" },
                    { 4, new DateTime(1988, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "jana.novotna@post.cz", "Jana", "Novotná", "602333444" },
                    { 5, new DateTime(1979, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "martin.cerny@email.cz", "Martin", "Černý", "775555666" },
                    { 6, new DateTime(2000, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "lucie.prochazkova@seznam.cz", "Lucie", "Procházková", "731777888" },
                    { 7, new DateTime(1992, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "tomas.kucera@centrum.cz", "Tomáš", "Kučera", "603999000" },
                    { 8, new DateTime(1998, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "katka.vesela@gmail.com", "Kateřina", "Veselá", "722123789" },
                    { 9, new DateTime(1983, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "jakub.horak@email.cz", "Jakub", "Horák", "776456123" },
                    { 10, new DateTime(1991, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "misa.nemcova@post.cz", "Michaela", "Němcová", "605789123" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Auta",
                keyColumn: "AutoId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Auta",
                keyColumn: "AutoId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Auta",
                keyColumn: "AutoId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Auta",
                keyColumn: "AutoId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Auta",
                keyColumn: "AutoId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Zakaznici",
                keyColumn: "ZakaznikId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Zakaznici",
                keyColumn: "ZakaznikId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Zakaznici",
                keyColumn: "ZakaznikId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Zakaznici",
                keyColumn: "ZakaznikId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Zakaznici",
                keyColumn: "ZakaznikId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Zakaznici",
                keyColumn: "ZakaznikId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Zakaznici",
                keyColumn: "ZakaznikId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Zakaznici",
                keyColumn: "ZakaznikId",
                keyValue: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Telefon",
                table: "Zakaznici",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Zakaznici",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatumNarozeni",
                table: "Zakaznici",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
