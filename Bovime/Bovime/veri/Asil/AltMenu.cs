using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    public partial class AltMenu : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Alt MenüKimlik")]
        [Required]
        public Int32 altMenukimlik { get; set; }

        [Display(Name = "Alt Menü Başlık")]
        [Column(TypeName = "nvarchar(300)")]
        public string? altMenuBaslik { get; set; }

        [Display(Name = "Menü Adı")]
        [Column(TypeName = "nvarchar(200)")]
        public string? menuAdi { get; set; }

        [Display(Name = "Menü URL")]
        [Column(TypeName = "nvarchar(200)")]
        public string? menuUrl { get; set; }

        [Display(Name = "Sırası")]
        public Int32? sirasi { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

    }
}
