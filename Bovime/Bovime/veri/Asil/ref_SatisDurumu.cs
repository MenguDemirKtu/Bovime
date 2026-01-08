using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 namespace Bovime.veri
 {

    public partial class ref_SatisDurumu : Bilesen
 {
 [Key]
[Required]
 public   Int32 SatisDurumuKimlik {get;set;}

[Required, Column(TypeName = "nvarchar(200)")]
 public   string SatisDurumuAdi {get;set;}

 } 
 }
