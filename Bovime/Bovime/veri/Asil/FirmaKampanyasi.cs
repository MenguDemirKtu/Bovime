using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    public partial class FirmaKampanyasi : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Firma KampanyasıKimlik")]
        [Required]
        public Int64 firmaKampanyasikimlik { get; set; }

        [Display(Name = "Firma")]
        [Required]
        public Int32 i_firmaKimlik { get; set; }

        [Display(Name = "Foto")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = "Açıklama")]
        [Column(TypeName = "nvarchar(500)")]
        public string? aciklama { get; set; }

        [Display(Name = "Yayın Başlangıç")]
        [Column(TypeName = "datetime")]
        public DateTime? yayinBaslangic { get; set; }

        [Display(Name = "Yayın Bitiş")]
        [Column(TypeName = "datetime")]
        public DateTime? yayinBitis { get; set; }

        [Display(Name = "Hedef Url")]
        [Column(TypeName = "nvarchar(400)")]
        public string? hedefUrl { get; set; }

        [Display(Name = "Ön Kod")]
        [Column(TypeName = "nvarchar(5)")]
        public string? onKod { get; set; }

        [Display(Name = "Sırası")]
        public Int32? sirasi { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

        [Display(Name = "Başlık")]
        [Column(TypeName = "nvarchar(200)")]
        public string? baslik { get; set; }

        [Display(Name = "Metin")]
        [Column(TypeName = "text")]
        public string? metin { get; set; }

        [Display(Name = ".")]
        public Int32? y_firmaKampanyaTalebiKimlik { get; set; }

        [Display(Name = "Durum")]
        public Int32? i_kampanyaDurumuKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(40)")]
        public string? kampanyaKodu { get; set; }

    }
}
