using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PujcovnaAut.Model
{
    [AddINotifyPropertyChangedInterface]
    public abstract class BaseModel : IDataErrorInfo
    {
        // --- Implementace IDataErrorInfo pro WPF Validaci ---

        public string Error => null;

        public string this[string propertyName]
        {
            get
            {
                return OnValidate(propertyName);
            }
        }

        protected virtual string OnValidate(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return string.Empty;

            string error = string.Empty;
            var value = this.GetType().GetProperty(propertyName)?.GetValue(this, null);
            var results = new List<ValidationResult>(1);

            var context = new ValidationContext(this, null, null) { MemberName = propertyName };
            var result = Validator.TryValidateProperty(value, context, results);

            if (!result)
            {
                var validationResult = results.First();
                error = validationResult.ErrorMessage;
            }

            return error;
        }

        // --- Společné vlastnosti ---

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}