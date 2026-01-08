using System;
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
 public   Int64 uyekimlik {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(100)")]
 public   string  ? adi {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(100)")]
 public   string  ? soyadi {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(12)")]
 public   string  ? telefon {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(150)")]
 public   string  ? ePosta {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "datetime")]
 public   DateTime  ? uyelikTarihi {get;set;}

      [Display(Name = ".")] 
 public   bool  ? varmi {get;set;}

 } 
 }
