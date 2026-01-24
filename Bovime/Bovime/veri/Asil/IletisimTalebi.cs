using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    public partial class IletisimTalebi : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "İletişim TalebiKimlik")]
        [Required]
        public Int32 iletisimTalebikimlik { get; set; }

        [Display(Name = "Ad")]
        [Column(TypeName = "nvarchar(100)")]
        public string? ad { get; set; }

        [Display(Name = "E Posta")]
        [Column(TypeName = "nvarchar(150)")]
        public string? ePosta { get; set; }

        [Display(Name = "Telefon")]
        [Column(TypeName = "nvarchar(12)")]
        public string? telefon { get; set; }

        [Display(Name = "Konu")]
        [Column(TypeName = "nvarchar(150)")]
        public string? konu { get; set; }

        [Display(Name = "İleti")]
        [Column(TypeName = "nvarchar(2000)")]
        public string? ileti { get; set; }

        [Display(Name = "Tarih")]
        [Column(TypeName = "datetime")]
        public DateTime? tarih { get; set; }

        [Display(Name = "Görüldü mü")]
        public bool? e_gorulduMu { get; set; }

        [Display(Name = "IP Adresi")]
        [Column(TypeName = "nvarchar(26)")]
        public string? ipAdresi { get; set; }

        [Display(Name = "Tarih datetime varmi")]
        public bool? tarihDatetimeVarmi { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

    }
}
