using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataEntity
{
    [AddINotifyPropertyChangedInterface]
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

        [Required]
        public DateTime DatumNarozeni { get; set; }

        [Required]
        [StringLength(20)]
        public string CisloRP { get; set; }

        [Required]
        public string Email { get; set; }

        public string? Telefon { get; set; }

        public virtual ObservableCollection<Vypujcka> Vypujcky { get; set; } = new ObservableCollection<Vypujcka>();

        public override string ToString()
        {
            return $"{Jmeno} {Prijmeni} ({CisloRP})";
        }
    }
}