using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovime.veri
{

    [Table("KampanyaSatisAYRINTI")]
    public partial class KampanyaSatisAYRINTI : Bilesen
    {
        [Key]
        public Int64 kimlik { get; set; }

        [Column(TypeName = "nvarchar(400)")]
        public string? firmaAdi { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? kampanyaAdi { get; set; }

        public Int64? y_firmakampanyasikimlik { get; set; }

        public Int32? sayisi { get; set; }

        [Required]
        public Int32 i_firmaKimlik { get; set; }

    }
}
