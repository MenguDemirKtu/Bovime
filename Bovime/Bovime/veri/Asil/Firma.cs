using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    public partial class Firma : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "FirmaKimlik")]
        [Required]
        public Int32 firmakimlik { get; set; }

        [Display(Name = "Firma Adı")]
        [Column(TypeName = "nvarchar(400)")]
        public string? firmaAdi { get; set; }

        [Display(Name = "Firma Durumu")]
        [Required]
        public Int32 i_firmaDurumuKimlik { get; set; }

        [Display(Name = "Foto")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = "Telefon")]
        [Column(TypeName = "nvarchar(12)")]
        public string? telefon { get; set; }

        [Display(Name = "Telefon2")]
        [Column(TypeName = "nvarchar(12)")]
        public string? telefon2 { get; set; }

        [Display(Name = "E Posta")]
        [Column(TypeName = "nvarchar(150)")]
        public string? ePosta { get; set; }

        [Display(Name = "Adres")]
        [Column(TypeName = "nvarchar(150)")]
        public string? adres { get; set; }

        [Display(Name = "Tanıtım")]
        [Column(TypeName = "nvarchar(500)")]
        public string? tanitim { get; set; }

        [Display(Name = "Konum")]
        [Column(TypeName = "nvarchar(200)")]
        public string? konum { get; set; }

        [Display(Name = "Sırası")]
        public Int32? sirasi { get; set; }

        [Display(Name = "Puanı")]
        public Int32? puani { get; set; }

        [Display(Name = "Açıklama")]
        [Column(TypeName = "text")]
        public string? aciklama { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(150)")]
        public string? firmaUrl { get; set; }

    }
}
