using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    [Table("SikSorulanSorularAYRINTI")]
    public partial class SikSorulanSorularAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 sikSorulanSorularkimlik { get; set; }

        [Display(Name = "Soru ")]
        [Column(TypeName = "nvarchar(400)")]
        public string? soru { get; set; }

        [Display(Name = "Yanıt")]
        [Column(TypeName = "nvarchar(1000)")]
        public string? yanit { get; set; }

        [Display(Name = ".")]
        public Int32? sirasi { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
