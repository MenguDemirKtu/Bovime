using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    [Table("UyeAYRINTI")]
    public partial class UyeAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int64 uyekimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(100)")]
        public string? adi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(100)")]
        public string? soyadi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(12)")]
        public string? telefon { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(150)")]
        public string? ePosta { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "datetime")]
        public DateTime? uyelikTarihi { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(50)")]
        public string? kullaniciAdi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(20)")]
        public string? sifre { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(150)")]
        public string? adres { get; set; }

        [Display(Name = ".")]
        public Int32? i_uyeDurumuKimlik { get; set; }

        [Display(Name = ".")]
        [Required, Column(TypeName = "nvarchar(200)")]
        public string UyeDurumuAdi { get; set; }

    }
}
