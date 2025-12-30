using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using DataEntity;

namespace PujcovnaAut
{
    public static class Globals
    {
        public static PujcovnaContext context { get; set; }

        public static void Initialize()
        {
            context = new PujcovnaContext();

            // 1. Je ověřeno, zda databáze existuje. Pokud ne, je vytvořena.
            context.Database.EnsureCreated();

            // 2. Je zavolána metoda Seed pro vložení výchozích dat, pokud je databáze prázdná.
            context.Seed();

            // 3. Data jsou načtena do lokální paměti (Local) pro použití v aplikaci.
            context.Auta.Load();
            context.Zakaznici.Load();
            context.Vypujcky.Load();
            context.Kategorie.Load();
            context.Pojisteni.Load();
        }

        /// <summary>
        /// Metoda vrátí neuložené změny v kontextu.
        /// </summary>
        public static void Vratit()
        {
            if (context == null) return;
            foreach (var entity in context.ChangeTracker.Entries())
            {
                if (entity.State == EntityState.Modified) entity.Reload();
                else if (entity.State == EntityState.Added) entity.State = EntityState.Detached;
                else if (entity.State == EntityState.Deleted) entity.Reload();
            }
        }

        public static bool HasUnsavedChanges()
        {
            if (context == null) return false;
            return context.ChangeTracker.Entries().Any(e =>
                e.State == EntityState.Added ||
                e.State == EntityState.Modified ||
                e.State == EntityState.Deleted);
        }

        /// <summary>
        /// Metoda provede validaci entit a následně uloží změny do databáze.
        /// </summary>
        public static void UlozitData()
        {
            if (context == null) return;

            var changedEntities = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e => e.Entity);

            var validationErrors = new List<ValidationResult>();
            foreach (var entity in changedEntities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.TryValidateObject(entity, validationContext, validationErrors, validateAllProperties: true);
            }

            if (validationErrors.Any())
            {
                string text = "Chyby validace:\n";
                foreach (var error in validationErrors)
                {
                    text += $"- {error.ErrorMessage}\n";
                }
                MessageBox.Show(text, "Nelze uložit", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Změny jsou odeslány do databáze.
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba DB: {ex.Message}", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}