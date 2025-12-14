using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bovime.veri;
using System; 
using LinqKit;
using System.Threading;
using System.Collections;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Bovime.veriTabani
{

public class SiteMenuArama
{
 public  Int32  ?  siteMenukimlik {get;set;}
 public  string  ?  siteMenuAdi {get;set;}
 public  string  ?  menuUrl {get;set;}
 public  bool  ?  e_altMenuMu {get;set;}
 public  Int32  ?  i_ustSiteMenuKimlik {get;set;}
 public  Int32  ?  sirasi {get;set;}
 public  bool  ?  varmi {get;set;}
 public SiteMenuArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<SiteMenu> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< SiteMenu>(P => P.varmi == true);
 if (siteMenukimlik  != null)
 predicate = predicate.And(x => x.siteMenukimlik == siteMenukimlik ); 
 if (siteMenuAdi  != null)
               predicate = predicate.And( x => x.siteMenuAdi != null &&    x.siteMenuAdi .Contains(siteMenuAdi));
 if (menuUrl  != null)
               predicate = predicate.And( x => x.menuUrl != null &&    x.menuUrl .Contains(menuUrl));
 if (e_altMenuMu  != null)
 predicate = predicate.And(x => x.e_altMenuMu == e_altMenuMu ); 
 if (i_ustSiteMenuKimlik  != null)
 predicate = predicate.And(x => x.i_ustSiteMenuKimlik == i_ustSiteMenuKimlik ); 
 if (sirasi  != null)
 predicate = predicate.And(x => x.sirasi == sirasi ); 
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
return  predicate;
 
}
      public async Task<List<   SiteMenu      >> cek(veri.Varlik vari)
   {
     List <SiteMenu> sonuc = await vari.SiteMenus
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<SiteMenu?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    SiteMenu ? sonuc = await vari.SiteMenus
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class SiteMenuCizelgesi
{




/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<SiteMenu>> ara(params Expression<Func<SiteMenu, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<SiteMenu>> ara(veri.Varlik vari, params Expression<Func<SiteMenu, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.SiteMenus 
                  .Where(kosul).OrderByDescending(p => p.siteMenukimlik) 
         .ToListAsync(); 
} 



public static async Task<SiteMenu?> tekliCekKos(Int32 kimlik, Varlik kime)
{
SiteMenu? kayit = await kime.SiteMenus.FirstOrDefaultAsync(p => p.siteMenukimlik == kimlik && p.varmi == true);
 return kayit;
}


public static async Task kaydetKos(SiteMenu yeni, Varlik vari, params bool[] yedekAlinsinmi) 
{ 
    if (yeni.siteMenukimlik <= 0) 
   { 
      Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");  
     if (yeni.varmi == null)  
         yeni.varmi = true;  
      await vari.SiteMenus.AddAsync(yeni);  
     await vari.SaveChangesAsync(); 
     await kay.kaydetKos(vari, yedekAlinsinmi); 
 } 
 else 
 { 
    Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi "); 
    SiteMenu? bulunan = await vari.SiteMenus.FirstOrDefaultAsync(p => p.siteMenukimlik == yeni.siteMenukimlik); 
    if (bulunan == null)  
        return;  
   vari.Entry(bulunan).CurrentValues.SetValues(yeni);  
    await vari.SaveChangesAsync();  
    await kay.kaydetKos(vari, yedekAlinsinmi);  
 } 
} 


   public static async Task silKos(SiteMenu kimi, Varlik vari, params bool[] yedekAlinsinmi) 
  {
    Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
  kimi.varmi = false; 
  var bulunan = await vari.SiteMenus.FirstOrDefaultAsync(p => p.siteMenukimlik == kimi.siteMenukimlik); 
  if (bulunan == null) 
      return; 
  kimi.varmi = false;
  await vari.SaveChangesAsync();
   await kay.kaydetKos(vari, yedekAlinsinmi);
 }


public static SiteMenu? tekliCek(Int32 kimlik, Varlik kime)
{
SiteMenu ? kayit = kime.SiteMenus.FirstOrDefault(p => p.siteMenukimlik == kimlik); 
 if (kayit != null)   
    if (kayit.varmi != true) 
      return null; 
return kayit;
}


/// <summary> 
 /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır. 
  /// </summary> 
  /// <param name="yeni"></param> 
  /// <param name="kime"></param> 
  /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param> 
 public static void kaydet(SiteMenu yeni, Varlik kime, params bool[] yedekAlinsinmi)
{
  if(yeni.siteMenukimlik <= 0  )
{
 if(yeni.varmi == null)  
   yeni.varmi = true ;       
      kime.SiteMenus.Add(yeni);
kime.SaveChanges();
Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
kay.kaydet(yedekAlinsinmi);
}
else
{

     var bulunan = kime.SiteMenus.FirstOrDefault(p => p.siteMenukimlik == yeni.siteMenukimlik);
     if (bulunan == null)
        return;
     kime.Entry(bulunan).CurrentValues.SetValues(yeni); 
kime.SaveChanges();

Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
kay.kaydet(yedekAlinsinmi);
}
}


  public static void sil(SiteMenu kimi, Varlik kime )
{
    kimi.varmi = false; 
 var bulunan = kime.SiteMenus.FirstOrDefault(p => p.siteMenukimlik == kimi.siteMenukimlik);  
 if (bulunan == null) 
     return; 
  kime.Entry(bulunan).CurrentValues.SetValues(kimi);  
  kime.SaveChanges(); 
  Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi"); 
  kay.kaydet(); 

}
}
}


