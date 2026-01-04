using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 namespace Bovime.veri
 {

 [Table("AramaMotoruZiyaretAYRINTI")]
    public partial class AramaMotoruZiyaretAYRINTI : Bilesen
 {
 [Key]
      [Display(Name = ".")] 
[Required]
 public   Int64 aramaMotoruZiyaretkimlik {get;set;}

      [Display(Name = ".")] 
[Required]
 public   Int32 i_aramaMotoruKimlik {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(200)")]
 public   string  ? sayfaAdresi {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "datetime")]
 public   DateTime  ? tarih {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(30)")]
 public   string  ? ipAdresi {get;set;}

      [Display(Name = ".")] 
 public   bool  ? varmi {get;set;}

 } 
 }
