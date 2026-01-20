using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    public partial class AramaMotoruZiyaret : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Arama Motoru ZiyaretKimlik")]
        [Required]
        public Int64 aramaMotoruZiyaretkimlik { get; set; }

        [Display(Name = "Arama Motoru")]
        [Required]
        public Int32 i_aramaMotoruKimlik { get; set; }

        [Display(Name = "Tarih")]
        [Column(TypeName = "datetime")]
        public DateTime? tarih { get; set; }

        [Display(Name = "Ip Adresi")]
        [Column(TypeName = "nvarchar(30)")]
        public string? ipAdresi { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

    }
}
