using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    public partial class Paket : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 paketKimlik { get; set; }

        [Display(Name = "Paket Adı")]
        [Column(TypeName = "nvarchar(100)")]
        public string? paketAdi { get; set; }

        [Display(Name = "Foto")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = "Tanıtım")]
        [Column(TypeName = "nvarchar(500)")]
        public string? tanitim { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

        [Display(Name = "Azami İlan Sayısı")]
        public Int32? azamiIlanSayisi { get; set; }

    }
}
