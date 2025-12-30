using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DataEntity
{
    // Abstraktní třída, ze které dědí všechny tabulky
    public abstract class BaseModel : IDataErrorInfo
    {
        #region "Validace (IDataErrorInfo)"
        // Toto je klíčová část pro profesora - validace přes Data Annotations
        string IDataErrorInfo.Error => null;

        string IDataErrorInfo.this[string propertyName]
        {
            get => OnValidate(propertyName);
        }

        protected virtual string OnValidate(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Neplatný název vlastnosti", propertyName);

            string error = string.Empty;
            // Získání hodnoty vlastnosti
            var value = this.GetType().GetProperty(propertyName).GetValue(this, null);
            var results = new List<ValidationResult>(1);
            var context = new ValidationContext(this, null, null) { MemberName = propertyName };

            // Ověření (např. zda je vyplněno povinné pole)
            var result = Validator.TryValidateProperty(value, context, results);

            if (!result)
            {
                var validationResult = results.First();
                error = validationResult.ErrorMessage;
            }
            return error;
        }
        #endregion

        // Technické sloupce
        [Timestamp]
        public byte[] RowVersion { get; set; } // Proti přepsání dat více uživateli

        public DateTime DatumVytvoreni { get; set; } = DateTime.Now;
    }
}