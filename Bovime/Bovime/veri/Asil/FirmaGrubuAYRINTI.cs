using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    [Table("FirmaGrubuAYRINTI")]
    public partial class FirmaGrubuAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 firmaGrubukimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? firmaGrupAdi { get; set; }

        [Display(Name = ".")]
        public Int32? sirasi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(300)")]
        public string? aciklama { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
