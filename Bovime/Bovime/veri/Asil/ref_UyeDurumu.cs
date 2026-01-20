using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    [Table("ref_UyeDurumu")]
    public partial class ref_UyeDurumu : Bilesen
    {
        [Key]
        [Required]
        public Int32 UyeDurumuKimlik { get; set; }

        [Required, Column(TypeName = "nvarchar(200)")]
        public string UyeDurumuAdi { get; set; }

    }
}
