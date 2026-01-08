using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 namespace Bovime.veri
 {

 [Table("SatisAYRINTI")]
    public partial class SatisAYRINTI : Bilesen
 {
 [Key]
      [Display(Name = ".")] 
[Required]
 public   Int64 satisKimlik {get;set;}

      [Display(Name = ".")] 
[Required]
 public   Int32 i_firmaKimlik {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(400)")]
 public   string  ? firmaAdi {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(50)")]
 public   string  ? kodu {get;set;}

      [Display(Name = ".")] 
 public   bool  ? varmi {get;set;}

      [Display(Name = ".")] 
 public   Int64  ? i_uyeKimlik {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(100)")]
 public   string  ? adi {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(100)")]
 public   string  ? soyadi {get;set;}

      [Display(Name = ".")] 
 public   Int64  ? y_firmaKampanyasiKimlik {get;set;}

      [Display(Name = ".")] 
 public   Int32  ? i_satisDurumuKimlik {get;set;}

      [Display(Name = ".")] 
[Required, Column(TypeName = "nvarchar(200)")]
 public   string SatisDurumuAdi {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "datetime")]
 public   DateTime  ? tarih {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(200)")]
 public   string  ? kampanyaBasligi {get;set;}

 } 
 }
