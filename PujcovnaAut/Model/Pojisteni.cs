using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PujcovnaAut.Model
{
    [Table("Pojisteni")]
    public class Pojisteni : BaseModel
    {
        [Key]
        public int PojisteniId { get; set; }

        [Required(ErrorMessage = "Název plánu je povinný.")]
        [StringLength(50)]
        public string NazevPlanu { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Cena pojištění musí být nezáporná.")]
        public decimal CenaZaDen { get; set; }

        // Vazba 1:N (Jeden typ pojištění je u více výpůjček)
        public virtual ObservableCollection<Vypujcka> Vypujcky { get; set; } = new ObservableCollection<Vypujcka>();
    }
}