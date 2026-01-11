using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 namespace Bovime.veri
 {

 [Table("FirmaAYRINTI")]
    public partial class FirmaAYRINTI : Bilesen
 {
 [Key]
      [Display(Name = ".")] 
[Required]
 public   Int32 firmakimlik {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(400)")]
 public   string  ? firmaAdi {get;set;}

      [Display(Name = ".")] 
[Required]
 public   Int32 i_firmaDurumuKimlik {get;set;}

      [Display(Name = ".")] 
[Required, Column(TypeName = "nvarchar(200)")]
 public   string FirmaDurumuAdi {get;set;}

      [Display(Name = ".")] 
 public   Int64  ? i_fotoKimlik {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(256)")]
 public   string  ? fotosu {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(12)")]
 public   string  ? telefon {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(12)")]
 public   string  ? telefon2 {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(150)")]
 public   string  ? ePosta {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(150)")]
 public   string  ? adres {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(500)")]
 public   string  ? tanitim {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(200)")]
 public   string  ? konum {get;set;}

      [Display(Name = ".")] 
 public   Int32  ? sirasi {get;set;}

      [Display(Name = ".")] 
 public   Int32  ? puani {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "text")]
 public   string  ? aciklama {get;set;}

      [Display(Name = ".")] 
 public   bool  ? varmi {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(150)")]
 public   string  ? firmaUrl {get;set;}

      [Display(Name = ".")] 
 public   Int32  ? azamiIlanSayisi {get;set;}

      [Display(Name = ".")] 
 public   Int32  ? i_paketKimlik {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(100)")]
 public   string  ? sloganBaslik1 {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(500)")]
 public   string  ? slogan1 {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(100)")]
 public   string  ? sloganBaslik2 {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(500)")]
 public   string  ? slogan2 {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(100)")]
 public   string  ? sloganBaslik3 {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(500)")]
 public   string  ? slogan3 {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(100)")]
 public   string  ? sloganBaslik4 {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(500)")]
 public   string  ? slogan4 {get;set;}

      [Display(Name = ".")] 
 public   double   ? kullaniciOrtalamasi {get;set;}

 } 
 }
