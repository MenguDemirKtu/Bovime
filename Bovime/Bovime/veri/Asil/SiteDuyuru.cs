using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    public partial class SiteDuyuru : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Site DuyuruKimlik")]
        [Required]
        public Int32 siteDuyurukimlik { get; set; }

        [Display(Name = "Site Duyuru Başlığı")]
        [Column(TypeName = "nvarchar(200)")]
        public string? siteDuyuruBasligi { get; set; }

        [Display(Name = "Site Duyuru URL")]
        [Column(TypeName = "nvarchar(200)")]
        public string? siteDuyuruUrl { get; set; }

        [Display(Name = "Foto")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = "Yayın Başlangıcı")]
        [Column(TypeName = "datetime")]
        public DateTime? yayinBaslangici { get; set; }

        [Display(Name = "Yayın Bitişi")]
        [Column(TypeName = "datetime")]
        public DateTime? yayinBitisi { get; set; }

        [Display(Name = "Tanıtım")]
        [Column(TypeName = "nvarchar(1000)")]
        public string? tanitim { get; set; }

        [Display(Name = "Metin")]
        [Column(TypeName = "text")]
        public string? metin { get; set; }

        [Display(Name = "Sırası")]
        public Int32? sirasi { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

    }
}
