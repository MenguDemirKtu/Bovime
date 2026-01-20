using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    [Table("FirmaKampanyaTalebiAYRINTI")]
    public partial class FirmaKampanyaTalebiAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 firmaKampanyaTalebikimlik { get; set; }

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
        public string? metin { get; set; }

        [Display(Name = ".")]
        public Int32? sirasi { get; set; }

        [Display(Name = ".")]
        public bool? e_gorulduMu { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "datetime")]
        public DateTime? girisTarihi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(256)")]
        public string? fotosu { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? baslik { get; set; }

        [Display(Name = ".")]
        public Int32? i_kampanyaDurumuKimlik { get; set; }

        [Display(Name = ".")]
        [Required, Column(TypeName = "nvarchar(200)")]
        public string KampanyaDurumuAdi { get; set; }

    }
}
