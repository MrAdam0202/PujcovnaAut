using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DataEntity.Models.Base
{
    /// <summary>
    /// Abstraktní základní třída pro všechny datové entity.
    /// Je implementováno rozhraní IDataErrorInfo pro zajištění validace dat na úrovni modelu.
    /// </summary>
    public abstract class BaseModel : IDataErrorInfo
    {
        #region "Validace (IDataErrorInfo)"

        // Rozhraní IDataErrorInfo nevyžaduje implementaci celkové chyby objektu, vrací se tedy null.
        string IDataErrorInfo.Error => null;

        /// <summary>
        /// Indexer slouží k vyvolání validační logiky pro konkrétní vlastnost objektu.
        /// </summary>
        /// <param name="propertyName">Název vlastnosti, která je validována.</param>
        /// <returns>Chybová zpráva v případě neplatných dat, jinak prázdný řetězec.</returns>
        string IDataErrorInfo.this[string propertyName]
        {
            get => OnValidate(propertyName);
        }

        /// <summary>
        /// Metoda zajišťuje validaci vlastnosti pomocí atributů Data Annotations (např. [Required]).
        /// </summary>
        /// <param name="propertyName">Název validované vlastnosti.</param>
        /// <returns>Text chybového hlášení.</returns>
        protected virtual string OnValidate(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Neplatný název vlastnosti", propertyName);

            string error = string.Empty;

            // Je získána hodnota vlastnosti pomocí reflexe.
            var value = GetType().GetProperty(propertyName).GetValue(this, null);
            var results = new List<ValidationResult>(1);
            var context = new ValidationContext(this, null, null) { MemberName = propertyName };

            // Je provedeno ověření platnosti vlastnosti oproti definovaným validačním pravidlům.
            var result = Validator.TryValidateProperty(value, context, results);

            if (!result)
            {
                // V případě selhání validace je získána první chybová zpráva.
                var validationResult = results.First();
                error = validationResult.ErrorMessage;
            }
            return error;
        }
        #endregion

        // Je definován časový otisk (Timestamp) pro řešení konfliktů při souběžném přístupu více uživatelů k datům.
        [Timestamp]
        public byte[] RowVersion { get; set; }

        // Je automaticky evidováno datum a čas vytvoření záznamu.
        public DateTime DatumVytvoreni { get; set; } = DateTime.Now;
    }
}