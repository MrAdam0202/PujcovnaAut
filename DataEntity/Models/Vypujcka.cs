using PropertyChanged;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataEntity
{
    [AddINotifyPropertyChangedInterface]
    [Table("Vypujcky")]
    public class Vypujcka : BaseModel
    {
        [Key]
        public int VypujckaId { get; set; }

        [Required]
        public DateTime DatumOd { get; set; } = DateTime.Now;

        [Required]
        public DateTime DatumDo { get; set; } = DateTime.Now.AddDays(1);

        // Oprava: Přidán prázdný set, aby to neházelo chybu "Read Only"
        public int PocetDni
        {
            get => (DatumDo - DatumOd).Days > 0 ? (DatumDo - DatumOd).Days : 0;
            set { }
        }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CelkovaCena { get; set; }

        // Alias pro starý kód
        public decimal CenaCelkem { get => CelkovaCena; set => CelkovaCena = value; }

        public StavVypujcky Stav { get; set; } = StavVypujcky.Zapujceno;

        [Required]
        public int PojisteniId { get; set; }

        [ForeignKey(nameof(PojisteniId))]
        public virtual Pojisteni Pojisteni { get; set; }

        public string? Poznamka { get; set; }

        public int AutoId { get; set; }
        [ForeignKey(nameof(AutoId))]
        public virtual Auto Auto { get; set; }

        public int ZakaznikId { get; set; }
        [ForeignKey(nameof(ZakaznikId))]
        public virtual Zakaznik Zakaznik { get; set; }
    }
}