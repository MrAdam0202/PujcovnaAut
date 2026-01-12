using DataEntity.Models.Base;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataEntity
{
    [AddINotifyPropertyChangedInterface]
    [Table("Pojisteni")]
    public class Pojisteni : BaseModel
    {
        [Key]
        public int PojisteniId { get; set; }

        [Required]
        [StringLength(50)]
        public string NazevPlanu { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal CenaZaDen { get; set; }

        public virtual ObservableCollection<Vypujcka> Vypujcky { get; set; } = new ObservableCollection<Vypujcka>();

        public override string ToString() => NazevPlanu;
    }
}