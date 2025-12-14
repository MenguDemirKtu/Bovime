using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{
    public partial class ref_KullaniciTuru : Bilesen
    {
        [Key]
        [Required]
        public Int32 kullaniciTuruKimlik { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        public string kullaniciTuru { get; set; }

    }
}
