using PropertyChanged;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataEntity
{
    /// <summary>
    /// Třída reprezentující entitu výpůjčky v databázovém modelu.
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    [Table("Vypujcky")]
    public class Vypujcka : BaseModel
    {
        [Key]
        public int VypujckaId { get; set; }

        // Jsou definovány časové parametry výpůjčky. Jako výchozí hodnoty je nastaven aktuální čas a následující den.
        [Required]
        public DateTime DatumOd { get; set; } = DateTime.Now;

        [Required]
        public DateTime DatumDo { get; set; } = DateTime.Now.AddDays(1);

        /// <summary>
        /// Je vypočítán celkový počet dní trvání výpůjčky jako rozdíl mezi koncovým a počátečním datem.
        /// Setter je ponechán prázdný z důvodu zajištění kompatibility s mechanismem datové vazby (binding).
        /// </summary>
        public int PocetDni
        {
            get => (DatumDo - DatumOd).Days > 0 ? (DatumDo - DatumOd).Days : 0;
            set { }
        }

        // Je evidována celková finanční částka výpůjčky s definovanou přesností na dvě desetinná místa.
        [Column(TypeName = "decimal(18,2)")]
        public decimal CelkovaCena { get; set; }

        /// <summary>
        /// Hodnota vlastnosti CenaCelkem je přímo provázána s vlastností CelkovaCena pro zajištění zpětné kompatibility.
        /// </summary>
        public decimal CenaCelkem { get => CelkovaCena; set => CelkovaCena = value; }

        // Je spravován aktuální stav výpůjčky pomocí definovaného výčtového typu.
        public StavVypujcky Stav { get; set; } = StavVypujcky.Zapujceno;

        // Jsou definovány cizí klíče a navigační vlastnosti pro vazbu na pojistné plány.
        [Required]
        public int PojisteniId { get; set; }

        [ForeignKey(nameof(PojisteniId))]
        public virtual Pojisteni Pojisteni { get; set; }

        // Je umožněno uložení doplňujících informací k záznamu formou textové poznámky.
        public string? Poznamka { get; set; }

        // Je vytvořena relace k entitě vozidla pomocí cizího klíče AutoId.
        public int AutoId { get; set; }
        [ForeignKey(nameof(AutoId))]
        public virtual Auto Auto { get; set; }

        // Je vytvořena relace k entitě zákazníka pomocí cizího klíče ZakaznikId.
        public int ZakaznikId { get; set; }
        [ForeignKey(nameof(ZakaznikId))]
        public virtual Zakaznik Zakaznik { get; set; }
    }
}