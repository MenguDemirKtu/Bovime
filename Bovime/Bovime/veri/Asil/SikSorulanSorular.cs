using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 namespace Bovime.veri
 {

    public partial class SikSorulanSorular : Bilesen
 {
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
 [Key]
      [Display(Name = "Sık Sorulan SorularKimlik")] 
[Required]
 public   Int32 sikSorulanSorularkimlik {get;set;}

      [Display(Name = "Soru")] 
[ Column(TypeName = "nvarchar(400)")]
 public   string  ? soru {get;set;}

      [Display(Name = "Yanıt")] 
[ Column(TypeName = "nvarchar(1000)")]
 public   string  ? yanit {get;set;}

      [Display(Name = "Sırası")] 
 public   Int32  ? sirasi {get;set;}

      [Display(Name = "varmi")] 
 public   bool  ? varmi {get;set;}

 } 
 }
