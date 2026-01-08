using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    public partial class FirmaKampanyaTalebi : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 firmaKampanyaTalebikimlik { get; set; }

        [Display(Name = "Firma")]
        [Required]
        public Int32 i_firmaKimlik { get; set; }

        [Display(Name = "Foto")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = "Metin")]
        [Column(TypeName = "nvarchar(500)")]
        public string? metin { get; set; }

        [Display(Name = "Sırası")]
        public Int32? sirasi { get; set; }

        [Display(Name = "Görüldü mü")]
        public bool? e_gorulduMu { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

        [Display(Name = "Giriş Tarihj")]
        [Column(TypeName = "datetime")]
        public DateTime? girisTarihi { get; set; }

        [Display(Name = "Başlık")]
        [Column(TypeName = "nvarchar(200)")]
        public string? baslik { get; set; }

        [Display(Name = ".")]
        public Int32? i_kampanyaDurumuKimlik { get; set; }

    }
}
