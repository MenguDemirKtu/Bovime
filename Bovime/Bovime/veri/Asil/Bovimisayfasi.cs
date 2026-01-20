using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    public partial class Bovimisayfasi : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "BovimiSayfasiKimlik")]
        [Required]
        public Int32 bovimisayfasikimlik { get; set; }

        [Display(Name = "Bovimi Sayfası Başlık")]
        [Column(TypeName = "nvarchar(200)")]
        public string? bovimiSayfasiBaslik { get; set; }

        [Display(Name = "Title")]
        [Column(TypeName = "nvarchar(140)")]
        public string? title { get; set; }

        [Display(Name = "URL")]
        [Column(TypeName = "nvarchar(100)")]
        public string? url { get; set; }

        [Display(Name = "Tanıtım")]
        [Column(TypeName = "nvarchar(500)")]
        public string? tanitim { get; set; }

        [Display(Name = "Yayında mı")]
        public bool? e_yayindaMi { get; set; }

        [Display(Name = "Açıklama")]
        [Column(TypeName = "text")]
        public string? aciklama { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

    }
}
