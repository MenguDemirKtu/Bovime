using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    public partial class Uye : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "ÜyeKimlik")]
        [Required]
        public Int64 uyekimlik { get; set; }

        [Display(Name = "Adı")]
        [Column(TypeName = "nvarchar(100)")]
        public string? adi { get; set; }

        [Display(Name = "Soyadı")]
        [Column(TypeName = "nvarchar(100)")]
        public string? soyadi { get; set; }

        [Display(Name = "Telefon")]
        [Column(TypeName = "nvarchar(12)")]
        public string? telefon { get; set; }

        [Display(Name = "E Posta")]
        [Column(TypeName = "nvarchar(150)")]
        public string? ePosta { get; set; }

        [Display(Name = "Üyelik Tarihi")]
        [Column(TypeName = "datetime")]
        public DateTime? uyelikTarihi { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [Column(TypeName = "nvarchar(50)")]
        public string? kullaniciAdi { get; set; }

        [Display(Name = "Şifre")]
        [Column(TypeName = "nvarchar(20)")]
        public string? sifre { get; set; }

        [Display(Name = "Adres")]
        [Column(TypeName = "nvarchar(150)")]
        public string? adres { get; set; }

        [Display(Name = "Üye Durumu")]
        public Int32? i_uyeDurumuKimlik { get; set; }

    }
}
