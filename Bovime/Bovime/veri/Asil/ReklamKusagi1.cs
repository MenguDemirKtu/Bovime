using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    public partial class ReklamKusagi1 : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Reklam Kuşağı 1Kimlik")]
        [Required]
        public Int32 reklamKusagi1kimlik { get; set; }

        [Display(Name = "Başlık")]
        [Column(TypeName = "nvarchar(300)")]
        public string? baslik { get; set; }

        [Display(Name = "Metin")]
        [Column(TypeName = "nvarchar(1000)")]
        public string? metin { get; set; }

        [Display(Name = "Hedef URL")]
        [Column(TypeName = "nvarchar(200)")]
        public string? hedefUrl { get; set; }

        [Display(Name = "Foto")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

    }
}
