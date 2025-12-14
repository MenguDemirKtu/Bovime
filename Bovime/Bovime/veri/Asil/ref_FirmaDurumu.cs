using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    [Table("ref_FirmaDurumu")]
    public partial class ref_FirmaDurumu : Bilesen
    {
        [Key]
        [Required]
        public Int32 FirmaDurumuKimlik { get; set; }

        [Required, Column(TypeName = "nvarchar(200)")]
        public string FirmaDurumuAdi { get; set; }

    }
}
