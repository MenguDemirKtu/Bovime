using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    [Table("PaketAYRINTI")]
    public partial class PaketAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 paketKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(100)")]
        public string? paketAdi { get; set; }

        [Display(Name = ".")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(256)")]
        public string? fotosu { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(500)")]
        public string? tanitim { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

        [Display(Name = ".")]
        public Int32? azamiIlanSayisi { get; set; }

    }
}
