using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PujcovnaAut.Model
{
    [Table("Vypujcky")]
    public class Vypujcka : BaseModel
    {
        [Key]
        public int VypujckaId { get; set; }

        // Vazby
        public int ZakaznikId { get; set; }
        public virtual Zakaznik Zakaznik { get; set; }

        public int AutoId { get; set; }
        public virtual Auto Auto { get; set; }

        public int PojisteniId { get; set; }
        public virtual Pojisteni Pojisteni { get; set; }

        // Data
        public DateTime DatumOd { get; set; }
        public DateTime DatumDo { get; set; }

        // OPRAVA: Změněno na { get; set; }, aby šlo hodnotu uložit
        public int PocetDni { get; set; }

        public decimal CenaCelkem { get; set; }

        // OPRAVA: Typ je nyní Enum, nikoliv string
        public StavVypujcky Stav { get; set; }
    }
}