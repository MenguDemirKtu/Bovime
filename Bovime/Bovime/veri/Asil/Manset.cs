using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    public partial class Manset : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "ManşetKimlik")]
        [Required]
        public Int32 mansetkimlik { get; set; }

        [Display(Name = "Başlık Üst")]
        [Column(TypeName = "nvarchar(200)")]
        public string? baslikUst { get; set; }

        [Display(Name = "Başlık Orta")]
        [Column(TypeName = "nvarchar(200)")]
        public string? baslikOrta { get; set; }

        [Display(Name = "Alt Yazı")]
        [Column(TypeName = "nvarchar(1000)")]
        public string? altYazi { get; set; }

        [Display(Name = "Sırası")]
        public Int32? sirasi { get; set; }

        [Display(Name = "Foto")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = "Yayındamı")]
        public bool? e_yayindami { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

        [Display(Name = "Hedef Url")]
        [Column(TypeName = "nvarchar(200)")]
        public string? hedefUrl { get; set; }

    }
}
