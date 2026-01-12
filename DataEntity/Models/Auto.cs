using DataEntity.Models.Base;
using DataEntity.Models.Enums;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataEntity
{
    [AddINotifyPropertyChangedInterface]
    [Table("Auta")]
    public class Auto : BaseModel
    {
        [Key]
        public int AutoId { get; set; }

        [Required]
        [StringLength(50)]
        public string Znacka { get; set; }

        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        [StringLength(10)]
        public string SPZ { get; set; } = "";

        public string PopisAuta => $"{Znacka} {Model} ({SPZ})";

        [Required]
        public int RokVyroby { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal CenaZaDen { get; set; }

        [Required]
        public StavAuta Stav { get; set; } = StavAuta.Volne;

        [Required]
        public int KategorieId { get; set; }

        [ForeignKey(nameof(KategorieId))]
        public virtual Kategorie Kategorie { get; set; }

        public byte[]? Obrazek { get; set; }

        public virtual ObservableCollection<Vypujcka> Vypujcky { get; set; } = new ObservableCollection<Vypujcka>();
    }
}