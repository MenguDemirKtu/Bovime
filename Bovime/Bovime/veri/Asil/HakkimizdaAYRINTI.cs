using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    [Table("HakkimizdaAYRINTI")]
    public partial class HakkimizdaAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 hakkimizdakimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(300)")]
        public string? firmaAdi { get; set; }

        [Display(Name = "Kısa Tanıtım")]
        [Column(TypeName = "nvarchar(500)")]
        public string? kisaTanitim { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(11)")]
        public string? telefon { get; set; }

        [Display(Name = "E Posta")]
        [Column(TypeName = "nvarchar(150)")]
        public string? ePosta { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(150)")]
        public string? adres { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(500)")]
        public string? konum { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(100)")]
        public string? facebook { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(100)")]
        public string? twitter { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(100)")]
        public string? instagram { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(100)")]
        public string? linkedin { get; set; }

        [Display(Name = ".")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(256)")]
        public string? fotosu { get; set; }

        [Display(Name = "Uzun Tanıtım")]
        [Column(TypeName = "text")]
        public string? uzunTanitim { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
