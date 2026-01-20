using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    [Table("FirmaGrubuReklamiAYRINTI")]
    public partial class FirmaGrubuReklamiAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 firmaGrubuReklamikimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(400)")]
        public string? firmaGrubuBaslik { get; set; }

        [Display(Name = ".")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(256)")]
        public string? fotosu { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(400)")]
        public string? kisaTanitim { get; set; }

        [Display(Name = ".")]
        public Int32? sirasi { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
