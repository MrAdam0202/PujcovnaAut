using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DataEntity
{
    public class PujcovnaContext : DbContext
    {
        public DbSet<Auto> Auta { get; set; }
        public DbSet<Zakaznik> Zakaznici { get; set; }
        public DbSet<Vypujcka> Vypujcky { get; set; }
        public DbSet<Kategorie> Kategorie { get; set; }
        public DbSet<Pojisteni> Pojisteni { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Pokud není konfigurace nastavena, je použit SQL Server s definovaným Connection Stringem.
            // Je zapnuto Lazy Loading (líné načítání) pro automatické dotahování vazeb.
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=(localdb)\\MSSQLLocalDB;" +
                    "Initial Catalog=PujcovnaAut2025;" +
                    "Integrated Security=True;" +
                    "TrustServerCertificate=True")
                    .UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Je definována přesnost datového typu decimal pro finanční částky.
            modelBuilder.Entity<Kategorie>().Property(k => k.DenniSazba).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Pojisteni>().Property(p => p.CenaZaDen).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Vypujcka>().Property(v => v.CenaCelkem).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Vypujcka>().Property(v => v.CelkovaCena).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Auto>().Property(a => a.CenaZaDen).HasColumnType("decimal(18,2)");
        }

        /// <summary>
        /// Metoda pro naplnění databáze výchozími daty (Seedování).
        /// Je volána při startu aplikace, pokud jsou tabulky prázdné.
        /// </summary>
        public void Seed()
        {
            // 1. KATEGORIE
            // Je ověřeno, zda tabulka obsahuje data. Pokud ne, jsou vloženy kategorie.
            if (!Kategorie.Any())
            {
                Kategorie.AddRange(
                    new Kategorie { NazevKategorie = "Economy", DenniSazba = 500m, KoeficientPoj = 1.0 },
                    new Kategorie { NazevKategorie = "Standard", DenniSazba = 1000m, KoeficientPoj = 1.2 },
                    new Kategorie { NazevKategorie = "Premium", DenniSazba = 2500m, KoeficientPoj = 1.5 }
                );
                SaveChanges();
            }

            // 2. POJIŠTĚNÍ
            // Pokud tabulka neobsahuje data, jsou vloženy plány pojištění.
            if (!Pojisteni.Any())
            {
                Pojisteni.AddRange(
                    new Pojisteni { NazevPlanu = "Basic", CenaZaDen = 100m },
                    new Pojisteni { NazevPlanu = "Standard", CenaZaDen = 300m },
                    new Pojisteni { NazevPlanu = "Full Coverage", CenaZaDen = 500m }
                );
                SaveChanges();
            }

            // 3. AUTA
            // Pokud nejsou nalezena žádná auta, je vloženo 10 testovacích vozidel.
            if (!Auta.Any())
            {
                // Jsou načtena ID kategorií pro správné párování.
                var k1 = Kategorie.First(k => k.NazevKategorie == "Economy").KategorieId;
                var k2 = Kategorie.First(k => k.NazevKategorie == "Standard").KategorieId;
                var k3 = Kategorie.First(k => k.NazevKategorie == "Premium").KategorieId;

                Auta.AddRange(
                    new Auto { SPZ = "1P1 1234", Znacka = "Škoda", Model = "Fabia III", KategorieId = k1, Stav = StavAuta.Volne, CenaZaDen = 900m, RokVyroby = 2019 },
                    new Auto { SPZ = "2B5 5678", Znacka = "Škoda", Model = "Octavia IV", KategorieId = k2, Stav = StavAuta.Volne, CenaZaDen = 1500m, RokVyroby = 2020 },
                    new Auto { SPZ = "3A2 9012", Znacka = "Hyundai", Model = "i30", KategorieId = k1, Stav = StavAuta.Volne, CenaZaDen = 850m, RokVyroby = 2018 },
                    new Auto { SPZ = "4C8 3456", Znacka = "VW", Model = "Golf VIII", KategorieId = k2, Stav = StavAuta.Volne, CenaZaDen = 1600m, RokVyroby = 2021 },
                    new Auto { SPZ = "5E7 7890", Znacka = "Kia", Model = "Ceed", KategorieId = k1, Stav = StavAuta.Volne, CenaZaDen = 800m, RokVyroby = 2019 },
                    new Auto { SPZ = "6F9 1122", Znacka = "Audi", Model = "A4", KategorieId = k3, Stav = StavAuta.Volne, CenaZaDen = 3000m, RokVyroby = 2022 },
                    new Auto { SPZ = "7G8 3344", Znacka = "Toyota", Model = "Corolla", KategorieId = k2, Stav = StavAuta.Volne, CenaZaDen = 1400m, RokVyroby = 2021 },
                    new Auto { SPZ = "8H7 5566", Znacka = "Ford", Model = "Focus", KategorieId = k1, Stav = StavAuta.Volne, CenaZaDen = 950m, RokVyroby = 2020 },
                    new Auto { SPZ = "9I6 7788", Znacka = "BMW", Model = "320d", KategorieId = k3, Stav = StavAuta.Volne, CenaZaDen = 3500m, RokVyroby = 2023 },
                    new Auto { SPZ = "1J5 9900", Znacka = "Renault", Model = "Clio", KategorieId = k1, Stav = StavAuta.Volne, CenaZaDen = 700m, RokVyroby = 2018 }
                );
                SaveChanges();
            }

            // 4. ZÁKAZNÍCI
            // Pokud tabulka zákazníků zeje prázdnotou, je vloženo 10 testovacích osob.
            if (!Zakaznici.Any())
            {
                Zakaznici.AddRange(
                    new Zakaznik { Jmeno = "Jan", Prijmeni = "Novák", Email = "jan.novak@email.cz", Telefon = "777123456", CisloRP = "A1234567", DatumNarozeni = new DateTime(1990, 5, 20) },
                    new Zakaznik { Jmeno = "Eva", Prijmeni = "Dvořáková", Email = "eva.dvorakova@seznam.cz", Telefon = "608987654", CisloRP = "B9876543", DatumNarozeni = new DateTime(1985, 12, 1) },
                    new Zakaznik { Jmeno = "Petr", Prijmeni = "Svoboda", Email = "petr.svoboda@gmail.com", Telefon = "720111222", CisloRP = "C1122334", DatumNarozeni = new DateTime(1995, 3, 15) },
                    new Zakaznik { Jmeno = "Jana", Prijmeni = "Novotná", Email = "jana.novotna@post.cz", Telefon = "602333444", CisloRP = "D4455667", DatumNarozeni = new DateTime(1988, 7, 30) },
                    new Zakaznik { Jmeno = "Martin", Prijmeni = "Černý", Email = "martin.cerny@email.cz", Telefon = "775555666", CisloRP = "E8899001", DatumNarozeni = new DateTime(1979, 11, 10) },
                    new Zakaznik { Jmeno = "Lucie", Prijmeni = "Procházková", Email = "lucie.prochazkova@seznam.cz", Telefon = "731777888", CisloRP = "F2233445", DatumNarozeni = new DateTime(2000, 1, 5) },
                    new Zakaznik { Jmeno = "Tomáš", Prijmeni = "Kučera", Email = "tomas.kucera@centrum.cz", Telefon = "603999000", CisloRP = "G5566778", DatumNarozeni = new DateTime(1992, 9, 25) },
                    new Zakaznik { Jmeno = "Kateřina", Prijmeni = "Veselá", Email = "katka.vesela@gmail.com", Telefon = "722123789", CisloRP = "H8899112", DatumNarozeni = new DateTime(1998, 4, 18) },
                    new Zakaznik { Jmeno = "Jakub", Prijmeni = "Horák", Email = "jakub.horak@email.cz", Telefon = "776456123", CisloRP = "I3344556", DatumNarozeni = new DateTime(1983, 6, 12) },
                    new Zakaznik { Jmeno = "Michaela", Prijmeni = "Němcová", Email = "misa.nemcova@post.cz", Telefon = "605789123", CisloRP = "J6677889", DatumNarozeni = new DateTime(1991, 2, 28) }
                );
                SaveChanges();
            }
        }
    }
}