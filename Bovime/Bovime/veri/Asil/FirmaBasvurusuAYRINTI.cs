using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 namespace Bovime.veri
 {

 [Table("FirmaBasvurusuAYRINTI")]
    public partial class FirmaBasvurusuAYRINTI : Bilesen
 {
 [Key]
      [Display(Name = ".")] 
[Required]
 public   Int32 firmaBasvurusukimlik {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(200)")]
 public   string  ? firmaAdi {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(12)")]
 public   string  ? tel {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(150)")]
 public   string  ? ePosta {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(1000)")]
 public   string  ? metin {get;set;}

      [Display(Name = ".")] 
 public   bool  ? e_gorulduMu {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(20)")]
 public   string  ? gorulduMu {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "datetime")]
 public   DateTime  ? tarih {get;set;}

      [Display(Name = ".")] 
 public   bool  ? varmi {get;set;}

 } 
 }
