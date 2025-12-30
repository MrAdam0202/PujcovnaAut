using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeed : Migration
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
                    KoeficientPoj = table.Column<double>(type: "float", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    DatumVytvoreni = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    DatumVytvoreni = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    DatumNarozeni = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CisloRP = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    DatumVytvoreni = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    SPZ = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    RokVyroby = table.Column<int>(type: "int", nullable: false),
                    CenaZaDen = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stav = table.Column<int>(type: "int", nullable: false),
                    KategorieId = table.Column<int>(type: "int", nullable: false),
                    Obrazek = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    DatumVytvoreni = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    DatumOd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumDo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PocetDni = table.Column<int>(type: "int", nullable: false),
                    CelkovaCena = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CenaCelkem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stav = table.Column<int>(type: "int", nullable: false),
                    PojisteniId = table.Column<int>(type: "int", nullable: false),
                    Poznamka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutoId = table.Column<int>(type: "int", nullable: false),
                    ZakaznikId = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    DatumVytvoreni = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                        principalColumn: "PojisteniId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vypujcky_Zakaznici_ZakaznikId",
                        column: x => x.ZakaznikId,
                        principalTable: "Zakaznici",
                        principalColumn: "ZakaznikId",
                        onDelete: ReferentialAction.Cascade);
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
