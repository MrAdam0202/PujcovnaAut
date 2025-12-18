using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PujcovnaAut.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategorie",
                columns: table => new
                {
                    KategorieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazevKategorie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DenniSazba = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorie", x => x.KategorieId);
                });

            migrationBuilder.CreateTable(
                name: "Pojisteni",
                columns: table => new
                {
                    PojisteniId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazevPlanu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CenaZaDen = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pojisteni", x => x.PojisteniId);
                });

            migrationBuilder.CreateTable(
                name: "Zakaznici",
                columns: table => new
                {
                    ZakaznikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jmeno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prijmeni = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumNarozeni = table.Column<DateTime>(type: "date", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zakaznici", x => x.ZakaznikId);
                });

            migrationBuilder.CreateTable(
                name: "Auta",
                columns: table => new
                {
                    AutoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Znacka = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SPZ = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Stav = table.Column<int>(type: "int", nullable: false),
                    KategorieId = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auta", x => x.AutoId);
                    table.ForeignKey(
                        name: "FK_Auta_Kategorie_KategorieId",
                        column: x => x.KategorieId,
                        principalTable: "Kategorie",
                        principalColumn: "KategorieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vypujcky",
                columns: table => new
                {
                    VypujckaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZakaznikId = table.Column<int>(type: "int", nullable: false),
                    AutoId = table.Column<int>(type: "int", nullable: false),
                    PojisteniId = table.Column<int>(type: "int", nullable: true),
                    DatumOd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumDo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CenaCelkem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stav = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vypujcky", x => x.VypujckaId);
                    table.ForeignKey(
                        name: "FK_Vypujcky_Auta_AutoId",
                        column: x => x.AutoId,
                        principalTable: "Auta",
                        principalColumn: "AutoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vypujcky_Pojisteni_PojisteniId",
                        column: x => x.PojisteniId,
                        principalTable: "Pojisteni",
                        principalColumn: "PojisteniId");
                    table.ForeignKey(
                        name: "FK_Vypujcky_Zakaznici_ZakaznikId",
                        column: x => x.ZakaznikId,
                        principalTable: "Zakaznici",
                        principalColumn: "ZakaznikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Kategorie",
                columns: new[] { "KategorieId", "DenniSazba", "NazevKategorie" },
                values: new object[,]
                {
                    { 1, 800m, "Economy" },
                    { 2, 1200m, "Standard" },
                    { 3, 2000m, "Premium" }
                });

            migrationBuilder.InsertData(
                table: "Pojisteni",
                columns: new[] { "PojisteniId", "CenaZaDen", "NazevPlanu" },
                values: new object[,]
                {
                    { 1, 150m, "Basic" },
                    { 2, 250m, "Standard" },
                    { 3, 400m, "Full" }
                });

            migrationBuilder.InsertData(
                table: "Zakaznici",
                columns: new[] { "ZakaznikId", "DatumNarozeni", "Email", "Jmeno", "Prijmeni", "Telefon" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "jan.novak@email.cz", "Jan", "Novák", "777123456" },
                    { 2, new DateTime(1985, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "eva.dvorakova@seznam.cz", "Eva", "Dvořáková", "608987654" },
                    { 3, new DateTime(1995, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "petr.svoboda@gmail.com", "Petr", "Svoboda", "720111222" },
                    { 4, new DateTime(1988, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "jana.novotna@post.cz", "Jana", "Novotná", "602333444" },
                    { 5, new DateTime(1979, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "martin.cerny@email.cz", "Martin", "Černý", "775555666" },
                    { 6, new DateTime(2000, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "lucie.prochazkova@seznam.cz", "Lucie", "Procházková", "731777888" },
                    { 7, new DateTime(1992, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "tomas.kucera@centrum.cz", "Tomáš", "Kučera", "603999000" },
                    { 8, new DateTime(1998, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "katka.vesela@gmail.com", "Kateřina", "Veselá", "722123789" },
                    { 9, new DateTime(1983, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "jakub.horak@email.cz", "Jakub", "Horák", "776456123" },
                    { 10, new DateTime(1991, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "misa.nemcova@post.cz", "Michaela", "Němcová", "605789123" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auta_KategorieId",
                table: "Auta",
                column: "KategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Vypujcky_AutoId",
                table: "Vypujcky",
                column: "AutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vypujcky_PojisteniId",
                table: "Vypujcky",
                column: "PojisteniId");

            migrationBuilder.CreateIndex(
                name: "IX_Vypujcky_ZakaznikId",
                table: "Vypujcky",
                column: "ZakaznikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vypujcky");

            migrationBuilder.DropTable(
                name: "Auta");

            migrationBuilder.DropTable(
                name: "Pojisteni");

            migrationBuilder.DropTable(
                name: "Zakaznici");

            migrationBuilder.DropTable(
                name: "Kategorie");
        }
    }
}
