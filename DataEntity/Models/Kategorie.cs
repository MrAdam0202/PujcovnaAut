using PropertyChanged;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataEntity
{
    [AddINotifyPropertyChangedInterface]
    [Table("Kategorie")]
    public class Kategorie : BaseModel
    {
        [Key]
        public int KategorieId { get; set; }

        [Required]
        [StringLength(50)]
        public string NazevKategorie { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")] // Peněžní formát
        public decimal DenniSazba { get; set; }

        public double KoeficientPoj { get; set; } = 1.0;

        public virtual ObservableCollection<Auto> Auta { get; set; } = new ObservableCollection<Auto>();

        public override string ToString() => NazevKategorie;
    }
}