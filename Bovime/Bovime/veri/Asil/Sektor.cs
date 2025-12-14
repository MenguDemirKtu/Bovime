using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    public partial class Sektor : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "SektörKimlik")]
        [Required]
        public Int32 sektorkimlik { get; set; }

        [Display(Name = "Sektör Adı")]
        [Column(TypeName = "nvarchar(200)")]
        public string? sektorAdi { get; set; }

        [Display(Name = "Foto")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = "Tanıtım")]
        [Column(TypeName = "nvarchar(500)")]
        public string? tanitim { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

    }
}
