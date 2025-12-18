using Microsoft.EntityFrameworkCore;
using PujcovnaAut.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;

namespace PujcovnaAut
{
    public static class Globals
    {
        // Tady držíme připojení k databázi
        public static PujcovnaContext context { get; set; } = null!;

        public static void Initialize()
        {
            context = new PujcovnaContext();
            context.Database.EnsureCreated();
            context.Kategorie.Load();
        }

        // --- NOVÁ METODA: Vrací změny (Rollback) ---
        // Volá se při stisku STORNO nebo při zavření okna bez uložení
        public static void Vratit()
        {
            if (context == null) return;

            // Projdeme všechny entity, které Entity Framework sleduje a mají změny
            foreach (var entity in context.ChangeTracker.Entries())
            {
                // Pokud byla entita změněna (Modified), načteme ji znovu z DB (zahodíme změny)
                if (entity.State == EntityState.Modified)
                {
                    entity.Reload();
                }
                // Pokud byla entita nově přidána (Added), ale neuložena, vyhodíme ji ze sledování (Detached)
                else if (entity.State == EntityState.Added)
                {
                    entity.State = EntityState.Detached;
                }
                // Pokud byla smazána (Deleted), ale neuloženo, reload ji vrátí zpět
                else if (entity.State == EntityState.Deleted)
                {
                    entity.Reload();
                }
            }
        }

        // --- NOVÁ METODA: Kontrola neuložených změn ---
        public static bool HasUnsavedChanges()
        {
            if (context == null) return false;

            return context.ChangeTracker.Entries().Any(e =>
                e.State == EntityState.Added ||
                e.State == EntityState.Modified ||
                e.State == EntityState.Deleted);
        }

        // Tuto metodu voláme, když chceme uložit změny (tlačítko Uložit)
        public static void UlozitData()
        {
            if (context == null) return;

            // 1. Získáme seznam změněných věcí
            var changedEntities = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e => e.Entity)
                .ToList();

            // 2. Zkontrolujeme, zda jsou vyplněná povinná pole
            var validationErrors = new List<ValidationResult>();
            foreach (var entity in changedEntities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.TryValidateObject(entity, validationContext, validationErrors, validateAllProperties: true);
            }

            // 3. Pokud jsou chyby, zobrazíme je a neukládáme
            if (validationErrors.Any())
            {
                string text = "Nalezeny chyby:\n";
                foreach (var error in validationErrors)
                {
                    text += $"- {error.ErrorMessage}\n";
                }
                MessageBox.Show(text, "Chyba validace", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // 4. Pokud je vše OK, uložíme do databáze
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při ukládání: {ex.Message}", "Chyba DB", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}