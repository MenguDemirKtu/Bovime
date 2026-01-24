using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    public partial class FirmaBasvurusu : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Firma BasvurusuKimlik")]
        [Required]
        public Int32 firmaBasvurusukimlik { get; set; }

        [Display(Name = "Firma Adı")]
        [Column(TypeName = "nvarchar(200)")]
        public string? firmaAdi { get; set; }

        [Display(Name = "Tel")]
        [Column(TypeName = "nvarchar(12)")]
        public string? tel { get; set; }

        [Display(Name = "E Posta")]
        [Column(TypeName = "nvarchar(150)")]
        public string? ePosta { get; set; }

        [Display(Name = "Metin")]
        [Column(TypeName = "nvarchar(1000)")]
        public string? metin { get; set; }

        [Display(Name = "Görüldü mü")]
        public bool? e_gorulduMu { get; set; }

        [Display(Name = "Tarih")]
        [Column(TypeName = "datetime")]
        public DateTime? tarih { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

    }
}
