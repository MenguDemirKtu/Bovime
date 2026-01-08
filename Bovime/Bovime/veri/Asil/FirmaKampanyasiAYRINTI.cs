using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    [Table("FirmaKampanyasiAYRINTI")]
    public partial class FirmaKampanyasiAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int64 firmaKampanyasikimlik { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_firmaKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(400)")]
        public string? firmaAdi { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_firmaDurumuKimlik { get; set; }

        [Display(Name = ".")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(500)")]
        public string? aciklama { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "datetime")]
        public DateTime? yayinBaslangic { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "datetime")]
        public DateTime? yayinBitis { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(400)")]
        public string? hedefUrl { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(5)")]
        public string? onKod { get; set; }

        [Display(Name = ".")]
        public Int32? sirasi { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(256)")]
        public string? fotosu { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? baslik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(150)")]
        public string? firmaUrl { get; set; }

        [Display(Name = ".")]
        public Int32? firmaPuani { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "text")]
        public string? metin { get; set; }

        [Display(Name = ".")]
        public Int32? y_firmaKampanyaTalebiKimlik { get; set; }

        [Display(Name = ".")]
        public Int32? i_kampanyaDurumuKimlik { get; set; }

        [Display(Name = ".")]
        [Required, Column(TypeName = "nvarchar(200)")]
        public string KampanyaDurumuAdi { get; set; }

    }
}
