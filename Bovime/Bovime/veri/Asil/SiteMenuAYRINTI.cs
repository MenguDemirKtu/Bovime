using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    [Table("SiteMenuAYRINTI")]
    public partial class SiteMenuAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 siteMenukimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(250)")]
        public string? siteMenuAdi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? menuUrl { get; set; }

        [Display(Name = ".")]
        public bool? e_altMenuMu { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(20)")]
        public string? altMenuMu { get; set; }

        [Display(Name = ".")]
        public Int32? i_ustSiteMenuKimlik { get; set; }

        [Display(Name = ".")]
        public Int32? sirasi { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
