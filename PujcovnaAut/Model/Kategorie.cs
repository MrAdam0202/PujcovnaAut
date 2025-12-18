using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PujcovnaAut.Model
{
    [Table("Kategorie")]
    public class Kategorie
    {
        [Key]
        public int KategorieId { get; set; }

        [Required]
        [StringLength(50)]
        public string NazevKategorie { get; set; }

        // Cena za pronájem (např. 800, 1200, 2000)
        public decimal DenniSazba { get; set; }

        // --- TOTO JSME VRÁTILI ZPĚT (Koeficient pro výpočet pojištění) ---
        public double KoeficientPoj { get; set; }

        public virtual ObservableCollection<Auto> Auta { get; set; } = new ObservableCollection<Auto>();
    }
}