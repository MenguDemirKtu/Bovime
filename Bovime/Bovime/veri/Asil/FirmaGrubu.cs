using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 namespace Bovime.veri
 {

    public partial class FirmaGrubu : Bilesen
 {
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
 [Key]
      [Display(Name = "Firma GrubuKimlik")] 
[Required]
 public   Int32 firmaGrubukimlik {get;set;}

      [Display(Name = "Firma Grup Adı")] 
[ Column(TypeName = "nvarchar(200)")]
 public   string  ? firmaGrupAdi {get;set;}

      [Display(Name = "Sırası")] 
 public   Int32  ? sirasi {get;set;}

      [Display(Name = "Açıklama")] 
[ Column(TypeName = "nvarchar(300)")]
 public   string  ? aciklama {get;set;}

      [Display(Name = "varmi")] 
 public   bool  ? varmi {get;set;}

 } 
 }
