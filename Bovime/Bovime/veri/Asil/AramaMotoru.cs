using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 namespace Bovime.veri
 {

    public partial class AramaMotoru : Bilesen
 {
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
 [Key]
      [Display(Name = "Arama MotoruKimlik")] 
[Required]
 public   Int32 aramaMotorukimlik {get;set;}

      [Display(Name = "Sayfa Adresi")] 
[ Column(TypeName = "nvarchar(200)")]
 public   string  ? sayfaAdresi {get;set;}

      [Display(Name = "Title")] 
[ Column(TypeName = "nvarchar(200)")]
 public   string  ? title {get;set;}

      [Display(Name = "Keywords")] 
[ Column(TypeName = "nvarchar(140)")]
 public   string  ? keywords {get;set;}

      [Display(Name = "Görüntülenme Sayısı")] 
 public   Int32  ? goruntulenmeSayisi {get;set;}

      [Display(Name = "varmi")] 
 public   bool  ? varmi {get;set;}

 } 
 }
