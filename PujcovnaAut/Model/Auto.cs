using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PujcovnaAut.Model
{
    [Table("Auta")]
    public class Auto : BaseModel
    {
        [Key]
        public int AutoId { get; set; }

        [Required(ErrorMessage = "Značka je povinná")]
        [StringLength(50)]
        public string Znacka { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model je povinný")]
        [StringLength(50)]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "SPZ je povinná")]
        [StringLength(10)]
        public string SPZ { get; set; } = string.Empty;

        public StavAuta Stav { get; set; } = StavAuta.Volne;

        // Cizí klíč
        [Required(ErrorMessage = "Musíte vybrat kategorii")]
        public int KategorieId { get; set; }

        // Navigační vlastnost
        [ForeignKey(nameof(KategorieId))]
        public virtual Kategorie Kategorie { get; set; }

        // Vazba 1:N (Jedno auto má více výpůjček)
        public virtual ObservableCollection<Vypujcka> Vypujcky { get; set; } = new ObservableCollection<Vypujcka>();

        // --- UPRAVENO: Čistší výpis pro ComboBox ---
        [NotMapped]
        public string PopisAuta => $"{AutoId} | {SPZ} | {Znacka} {Model}";
    }
}