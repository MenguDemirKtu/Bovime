using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 namespace Bovime.veri
 {

    public partial class SiteMenu : Bilesen
 {
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
 [Key]
      [Display(Name = "Site MenüKimlik")] 
[Required]
 public   Int32 siteMenukimlik {get;set;}

      [Display(Name = "Site Menü Adı")] 
[ Column(TypeName = "nvarchar(250)")]
 public   string  ? siteMenuAdi {get;set;}

      [Display(Name = "Menü URL")] 
[ Column(TypeName = "nvarchar(200)")]
 public   string  ? menuUrl {get;set;}

      [Display(Name = "Alt Menü mü")] 
 public   bool  ? e_altMenuMu {get;set;}

      [Display(Name = "Üst Site Menü")] 
 public   Int32  ? i_ustSiteMenuKimlik {get;set;}

      [Display(Name = "Sırası")] 
 public   Int32  ? sirasi {get;set;}

      [Display(Name = "varmi")] 
 public   bool  ? varmi {get;set;}

 } 
 }
