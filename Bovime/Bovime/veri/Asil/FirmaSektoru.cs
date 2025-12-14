using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    public partial class FirmaSektoru : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Firma SektörüKimlik")]
        [Required]
        public Int32 firmaSektorukimlik { get; set; }

        [Display(Name = "Firma")]
        [Required]
        public Int32 i_firmaKimlik { get; set; }

        [Display(Name = "Sektör")]
        [Required]
        public Int32 i_sektorKimlik { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

    }
}
