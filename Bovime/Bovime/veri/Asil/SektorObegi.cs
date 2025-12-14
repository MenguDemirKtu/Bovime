using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    public partial class SektorObegi : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Sektör ÖbeğiKimlik")]
        [Required]
        public Int32 sektorObegikimlik { get; set; }

        [Display(Name = "Sektör Öbeği Adı")]
        [Column(TypeName = "nvarchar(200)")]
        public string? sektorObegiAdi { get; set; }

        [Display(Name = "Sırası")]
        public Int32? sirasi { get; set; }

        [Display(Name = "Ana Sayfada Görünsün mü")]
        public bool? e_anaSayfadaGorunsunMu { get; set; }

        [Display(Name = "Foto")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = "Tanıtım")]
        [Column(TypeName = "nvarchar(500)")]
        public string? tanitim { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

    }
}
