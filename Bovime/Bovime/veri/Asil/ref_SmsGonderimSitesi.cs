using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{
    public partial class ref_SmsGonderimSitesi : Bilesen
    {
        [Key]
        [Required]
        public Int32 SmsGonderimSitesiKimlik { get; set; }

        [Required, Column(TypeName = "nvarchar(200)")]
        public string SmsGonderimSitesiAdi { get; set; }

    }
}
