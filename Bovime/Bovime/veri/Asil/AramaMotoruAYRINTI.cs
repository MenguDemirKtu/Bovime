using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    [Table("AramaMotoruAYRINTI")]
    public partial class AramaMotoruAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 aramaMotorukimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? sayfaAdresi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? title { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(140)")]
        public string? keywords { get; set; }

        [Display(Name = ".")]
        public Int32? goruntulenmeSayisi { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
