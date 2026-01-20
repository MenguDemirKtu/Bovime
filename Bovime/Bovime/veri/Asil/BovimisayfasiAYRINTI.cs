using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    [Table("BovimisayfasiAYRINTI")]
    public partial class BovimisayfasiAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 bovimisayfasikimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? bovimiSayfasiBaslik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(140)")]
        public string? title { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(100)")]
        public string? url { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(500)")]
        public string? tanitim { get; set; }

        [Display(Name = ".")]
        public bool? e_yayindaMi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(20)")]
        public string? yayindaMi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "text")]
        public string? aciklama { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
