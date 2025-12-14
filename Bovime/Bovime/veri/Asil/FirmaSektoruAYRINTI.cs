using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    [Table("FirmaSektoruAYRINTI")]
    public partial class FirmaSektoruAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 firmaSektorukimlik { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_firmaKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(400)")]
        public string? firmaAdi { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_sektorKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? sektorAdi { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
