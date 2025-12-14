using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    [Table("SiteDuyuruAYRINTI")]
    public partial class SiteDuyuruAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 siteDuyurukimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? siteDuyuruBasligi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? siteDuyuruUrl { get; set; }

        [Display(Name = ".")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(256)")]
        public string? fotosu { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "datetime")]
        public DateTime? yayinBaslangici { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "datetime")]
        public DateTime? yayinBitisi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(1000)")]
        public string? tanitim { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "text")]
        public string? metin { get; set; }

        [Display(Name = ".")]
        public Int32? sirasi { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
