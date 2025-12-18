using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PujcovnaAut.Model
{
    [Table("Zakaznici")]
    public class Zakaznik : BaseModel
    {
        [Key]
        public int ZakaznikId { get; set; }

        [Required]
        [StringLength(50)]
        public string Jmeno { get; set; }

        [Required]
        [StringLength(50)]
        public string Prijmeni { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Telefon { get; set; }

        public DateTime DatumNarozeni { get; set; }

        // --- NOVÉ: Vlastnost pro zobrazení v roletce ---
        [NotMapped]
        public string CeleJmeno => $"{Prijmeni} {Jmeno}";
    }
}