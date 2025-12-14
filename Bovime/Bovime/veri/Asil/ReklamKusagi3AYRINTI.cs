using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 namespace Bovime.veri
 {

 [Table("ReklamKusagi3AYRINTI")]
    public partial class ReklamKusagi3AYRINTI : Bilesen
 {
 [Key]
      [Display(Name = ".")] 
[Required]
 public   Int32 reklamKusagi3kimlik {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(200)")]
 public   string  ? baslik {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(400)")]
 public   string  ? metin {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(200)")]
 public   string  ? hedefUrl {get;set;}

      [Display(Name = ".")] 
 public   Int64  ? i_fotoKimlik {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(256)")]
 public   string  ? fotosu {get;set;}

      [Display(Name = ".")] 
 public   Int32  ? sirasi {get;set;}

      [Display(Name = ".")] 
 public   bool  ? varmi {get;set;}

 } 
 }
