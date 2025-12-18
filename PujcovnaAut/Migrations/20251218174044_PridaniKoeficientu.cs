using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PujcovnaAut.Migrations
{
    /// <inheritdoc />
    public partial class PridaniKoeficientu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vypujcky_Pojisteni_PojisteniId",
                table: "Vypujcky");

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

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Kategorie");

            migrationBuilder.AlterColumn<int>(
                name: "PojisteniId",
                table: "Vypujcky",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PocetDni",
                table: "Vypujcky",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "KoeficientPoj",
                table: "Kategorie",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Kategorie",
                keyColumn: "KategorieId",
                keyValue: 1,
                column: "KoeficientPoj",
                value: 1.0);

            migrationBuilder.UpdateData(
                table: "Kategorie",
                keyColumn: "KategorieId",
                keyValue: 2,
                column: "KoeficientPoj",
                value: 1.5);

            migrationBuilder.UpdateData(
                table: "Kategorie",
                keyColumn: "KategorieId",
                keyValue: 3,
                column: "KoeficientPoj",
                value: 2.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Vypujcky_Pojisteni_PojisteniId",
                table: "Vypujcky",
                column: "PojisteniId",
                principalTable: "Pojisteni",
                principalColumn: "PojisteniId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vypujcky_Pojisteni_PojisteniId",
                table: "Vypujcky");

            migrationBuilder.DropColumn(
                name: "PocetDni",
                table: "Vypujcky");

            migrationBuilder.DropColumn(
                name: "KoeficientPoj",
                table: "Kategorie");

            migrationBuilder.AlterColumn<int>(
                name: "PojisteniId",
                table: "Vypujcky",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Kategorie",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Vypujcky_Pojisteni_PojisteniId",
                table: "Vypujcky",
                column: "PojisteniId",
                principalTable: "Pojisteni",
                principalColumn: "PojisteniId");
        }
    }
}
