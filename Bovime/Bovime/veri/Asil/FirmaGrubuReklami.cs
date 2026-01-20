using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    public partial class FirmaGrubuReklami : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Firma Grubu ReklamıKimlik")]
        [Required]
        public Int32 firmaGrubuReklamikimlik { get; set; }

        [Display(Name = "Firma Grubu Başlık")]
        [Column(TypeName = "nvarchar(400)")]
        public string? firmaGrubuBaslik { get; set; }

        [Display(Name = "Foto")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = "Kısa Tanıtım")]
        [Column(TypeName = "nvarchar(400)")]
        public string? kisaTanitim { get; set; }

        [Display(Name = "Sırası")]
        public Int32? sirasi { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

    }
}
