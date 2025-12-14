using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    [Table("AnaSayfaManset")]
    public partial class AnaSayfaManset : Bilesen
    {
        [Key]
        [Display(Name = "Ana Sayfa ManşetKimlik")]
        [Required]
        public Int32 anaSayfaMansetkimlik { get; set; }

        [Display(Name = "Başlık")]
        [Column(TypeName = "nvarchar(400)")]
        public string? baslik { get; set; }

        [Display(Name = "Metin")]
        [Column(TypeName = "nvarchar(1000)")]
        public string? metin { get; set; }

        [Display(Name = "Sırası")]
        public Int32? sirasi { get; set; }

        [Display(Name = "Foto")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = "Yayın Başlangıç")]
        [Column(TypeName = "datetime")]
        public DateTime? yayinBaslangic { get; set; }

        [Display(Name = "Yayın Bitiş")]
        [Column(TypeName = "datetime")]
        public DateTime? yayinBitis { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

        [Display(Name = "Hedef URL")]
        [Column(TypeName = "nvarchar(200)")]
        public string? hedefURL { get; set; }

    }
}
