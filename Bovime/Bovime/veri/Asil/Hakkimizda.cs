using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    public partial class Hakkimizda : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "HakkımızdaKimlik")]
        [Required]
        public Int32 hakkimizdakimlik { get; set; }

        [Display(Name = "Firma Adı")]
        [Column(TypeName = "nvarchar(300)")]
        public string? firmaAdi { get; set; }

        [Display(Name = "Kısa Tanıtım")]
        [Column(TypeName = "nvarchar(500)")]
        public string? kisaTanitim { get; set; }

        [Display(Name = "Telefon")]
        [Column(TypeName = "nvarchar(11)")]
        public string? telefon { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(150)")]
        public string? ePosta { get; set; }

        [Display(Name = "Adres")]
        [Column(TypeName = "nvarchar(150)")]
        public string? adres { get; set; }

        [Display(Name = "Konum")]
        [Column(TypeName = "nvarchar(500)")]
        public string? konum { get; set; }

        [Display(Name = "Facebook")]
        [Column(TypeName = "nvarchar(100)")]
        public string? facebook { get; set; }

        [Display(Name = "Twitter")]
        [Column(TypeName = "nvarchar(100)")]
        public string? twitter { get; set; }

        [Display(Name = "Instagram")]
        [Column(TypeName = "nvarchar(100)")]
        public string? instagram { get; set; }

        [Display(Name = "Linkedin")]
        [Column(TypeName = "nvarchar(100)")]
        public string? linkedin { get; set; }

        [Display(Name = "Foto")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = "Uzun Tanıtım")]
        [Column(TypeName = "text")]
        public string? uzunTanitim { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
