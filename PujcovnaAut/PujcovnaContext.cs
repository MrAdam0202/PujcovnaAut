using Microsoft.EntityFrameworkCore;
using PujcovnaAut.Model;
using System;

namespace PujcovnaAut
{
    public class PujcovnaContext : DbContext
    {
        public virtual DbSet<Kategorie> Kategorie { get; set; }
        public virtual DbSet<Pojisteni> Pojisteni { get; set; }
        public virtual DbSet<Auto> Auta { get; set; }
        public virtual DbSet<Zakaznik> Zakaznici { get; set; }
        public virtual DbSet<Vypujcka> Vypujcky { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PujcovnaAutDB;Trusted_Connection=True;TrustServerCertificate=True;")
                              .UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Nastavení přesnosti
            modelBuilder.Entity<Kategorie>().Property(k => k.DenniSazba).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Pojisteni>().Property(p => p.CenaZaDen).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Vypujcka>().Property(v => v.CenaCelkem).HasColumnType("decimal(18,2)");

            // 1. KATEGORIE
            modelBuilder.Entity<Kategorie>().HasData(
                new Kategorie { KategorieId = 1, NazevKategorie = "Economy", DenniSazba = 800, KoeficientPoj = 1.0 },
                new Kategorie { KategorieId = 2, NazevKategorie = "Standard", DenniSazba = 1200, KoeficientPoj = 1.5 },
                new Kategorie { KategorieId = 3, NazevKategorie = "Premium", DenniSazba = 2000, KoeficientPoj = 2.0 }
            );

            // 2. POJIŠTĚNÍ
            modelBuilder.Entity<Pojisteni>().HasData(
                new Pojisteni { PojisteniId = 1, NazevPlanu = "Basic", CenaZaDen = 150 },
                new Pojisteni { PojisteniId = 2, NazevPlanu = "Standard", CenaZaDen = 250 },
                new Pojisteni { PojisteniId = 3, NazevPlanu = "Full", CenaZaDen = 400 }
            );

            // 3. AUTA (10 vozidel)
            modelBuilder.Entity<Auto>().HasData(
                // Původní 5
                new Auto { AutoId = 1, SPZ = "1P1 1234", Znacka = "Škoda", Model = "Fabia III", KategorieId = 1, Stav = StavAuta.Volne },
                new Auto { AutoId = 2, SPZ = "2B5 5678", Znacka = "Škoda", Model = "Octavia IV", KategorieId = 2, Stav = StavAuta.Volne },
                new Auto { AutoId = 3, SPZ = "3A2 9012", Znacka = "Hyundai", Model = "i30", KategorieId = 1, Stav = StavAuta.Volne },
                new Auto { AutoId = 4, SPZ = "4C8 3456", Znacka = "VW", Model = "Golf VIII", KategorieId = 2, Stav = StavAuta.Volne },
                new Auto { AutoId = 5, SPZ = "5E7 7890", Znacka = "Kia", Model = "Ceed", KategorieId = 1, Stav = StavAuta.Volne },

                // Nová 5
                new Auto { AutoId = 6, SPZ = "6F9 1122", Znacka = "Audi", Model = "A4", KategorieId = 3, Stav = StavAuta.Volne }, // Premium
                new Auto { AutoId = 7, SPZ = "7G8 3344", Znacka = "Toyota", Model = "Corolla", KategorieId = 2, Stav = StavAuta.Volne }, // Standard
                new Auto { AutoId = 8, SPZ = "8H7 5566", Znacka = "Ford", Model = "Focus", KategorieId = 1, Stav = StavAuta.Volne }, // Economy
                new Auto { AutoId = 9, SPZ = "9I6 7788", Znacka = "BMW", Model = "320d", KategorieId = 3, Stav = StavAuta.Volne }, // Premium
                new Auto { AutoId = 10, SPZ = "1J5 9900", Znacka = "Renault", Model = "Clio", KategorieId = 1, Stav = StavAuta.Volne } // Economy
            );

            // 4. ZÁKAZNÍCI (10 zákazníků)
            modelBuilder.Entity<Zakaznik>().HasData(
                // Původní 2
                new Zakaznik { ZakaznikId = 1, Jmeno = "Jan", Prijmeni = "Novák", Email = "jan.novak@email.cz", Telefon = "777123456", DatumNarozeni = new DateTime(1990, 5, 20) },
                new Zakaznik { ZakaznikId = 2, Jmeno = "Eva", Prijmeni = "Dvořáková", Email = "eva.dvorakova@seznam.cz", Telefon = "608987654", DatumNarozeni = new DateTime(1985, 12, 1) },

                // Nových 8
                new Zakaznik { ZakaznikId = 3, Jmeno = "Petr", Prijmeni = "Svoboda", Email = "petr.svoboda@gmail.com", Telefon = "720111222", DatumNarozeni = new DateTime(1995, 3, 15) },
                new Zakaznik { ZakaznikId = 4, Jmeno = "Jana", Prijmeni = "Novotná", Email = "jana.novotna@post.cz", Telefon = "602333444", DatumNarozeni = new DateTime(1988, 7, 30) },
                new Zakaznik { ZakaznikId = 5, Jmeno = "Martin", Prijmeni = "Černý", Email = "martin.cerny@email.cz", Telefon = "775555666", DatumNarozeni = new DateTime(1979, 11, 10) },
                new Zakaznik { ZakaznikId = 6, Jmeno = "Lucie", Prijmeni = "Procházková", Email = "lucie.prochazkova@seznam.cz", Telefon = "731777888", DatumNarozeni = new DateTime(2000, 1, 5) },
                new Zakaznik { ZakaznikId = 7, Jmeno = "Tomáš", Prijmeni = "Kučera", Email = "tomas.kucera@centrum.cz", Telefon = "603999000", DatumNarozeni = new DateTime(1992, 9, 25) },
                new Zakaznik { ZakaznikId = 8, Jmeno = "Kateřina", Prijmeni = "Veselá", Email = "katka.vesela@gmail.com", Telefon = "722123789", DatumNarozeni = new DateTime(1998, 4, 18) },
                new Zakaznik { ZakaznikId = 9, Jmeno = "Jakub", Prijmeni = "Horák", Email = "jakub.horak@email.cz", Telefon = "776456123", DatumNarozeni = new DateTime(1983, 6, 12) },
                new Zakaznik { ZakaznikId = 10, Jmeno = "Michaela", Prijmeni = "Němcová", Email = "misa.nemcova@post.cz", Telefon = "605789123", DatumNarozeni = new DateTime(1991, 2, 28) }
            );
        }
    }
}