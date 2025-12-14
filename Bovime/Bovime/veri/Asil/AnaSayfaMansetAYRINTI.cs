using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    [Table("AnaSayfaMansetAYRINTI")]
    public partial class AnaSayfaMansetAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 anaSayfaMansetkimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(400)")]
        public string? baslik { get; set; }

        [Display(Name = "Metin")]
        [Column(TypeName = "nvarchar(1000)")]
        public string? metin { get; set; }

        [Display(Name = ".")]
        public Int32? sirasi { get; set; }

        [Display(Name = ".")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(256)")]
        public string? fotosu { get; set; }

        [Display(Name = "Yayın Başlangıç")]
        [Column(TypeName = "datetime")]
        public DateTime? yayinBaslangic { get; set; }

        [Display(Name = "Yayın Bitiş")]
        [Column(TypeName = "datetime")]
        public DateTime? yayinBitis { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
