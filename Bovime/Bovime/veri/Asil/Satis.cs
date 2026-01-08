using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 namespace Bovime.veri
 {

    public partial class Satis : Bilesen
 {
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
 [Key]
      [Display(Name = ".")] 
[Required]
 public   Int64 satisKimlik {get;set;}

      [Display(Name = "Firma")] 
[Required]
 public   Int32 i_firmaKimlik {get;set;}

      [Display(Name = "Kodu")] 
[ Column(TypeName = "nvarchar(50)")]
 public   string  ? kodu {get;set;}

      [Display(Name = "Var mı")] 
 public   bool  ? varmi {get;set;}

      [Display(Name = "Üye")] 
 public   Int64  ? i_uyeKimlik {get;set;}

      [Display(Name = "Kampanya")] 
 public   Int64  ? y_firmaKampanyasiKimlik {get;set;}

      [Display(Name = "Satış Durumu")] 
 public   Int32  ? i_satisDurumuKimlik {get;set;}

      [Display(Name = "Tarih")] 
[ Column(TypeName = "datetime")]
 public   DateTime  ? tarih {get;set;}

 } 
 }
