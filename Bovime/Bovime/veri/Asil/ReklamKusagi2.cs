using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    public partial class ReklamKusagi2 : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Reklam Kuşağı 2Kimlik")]
        [Required]
        public Int32 reklamKusagi2kimlik { get; set; }

        [Display(Name = "Başlık")]
        [Column(TypeName = "nvarchar(200)")]
        public string? baslik { get; set; }

        [Display(Name = "Metin")]
        [Column(TypeName = "nvarchar(400)")]
        public string? metin { get; set; }

        [Display(Name = "Hefef URL")]
        [Column(TypeName = "nvarchar(200)")]
        public string? hefefUrl { get; set; }

        [Display(Name = "Foto")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = "Sırası")]
        public Int32? sirasi { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

    }
}
