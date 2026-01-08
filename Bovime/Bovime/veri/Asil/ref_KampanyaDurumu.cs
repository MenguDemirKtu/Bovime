using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 namespace Bovime.veri
 {

 [Table("ref_KampanyaDurumu")]
    public partial class ref_KampanyaDurumu : Bilesen
 {
 [Key]
[Required]
 public   Int32 KampanyaDurumuKimlik {get;set;}

[Required, Column(TypeName = "nvarchar(200)")]
 public   string KampanyaDurumuAdi {get;set;}

 } 
 }
