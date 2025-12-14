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

public class ReklamKusagi2Arama
{
 public  Int32  ?  reklamKusagi2kimlik {get;set;}
 public  string  ?  baslik {get;set;}
 public  string  ?  metin {get;set;}
 public  string  ?  hefefUrl {get;set;}
 public  Int64  ?  i_fotoKimlik {get;set;}
 public  Int32  ?  sirasi {get;set;}
 public  bool  ?  varmi {get;set;}
 public ReklamKusagi2Arama()
{
this.varmi = true;
}
 
        private ExpressionStarter<ReklamKusagi2> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< ReklamKusagi2>(P => P.varmi == true);
 if (reklamKusagi2kimlik  != null)
 predicate = predicate.And(x => x.reklamKusagi2kimlik == reklamKusagi2kimlik ); 
 if (baslik  != null)
               predicate = predicate.And( x => x.baslik != null &&    x.baslik .Contains(baslik));
 if (metin  != null)
               predicate = predicate.And( x => x.metin != null &&    x.metin .Contains(metin));
 if (hefefUrl  != null)
               predicate = predicate.And( x => x.hefefUrl != null &&    x.hefefUrl .Contains(hefefUrl));
 if (i_fotoKimlik  != null)
 predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik ); 
 if (sirasi  != null)
 predicate = predicate.And(x => x.sirasi == sirasi ); 
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
return  predicate;
 
}
      public async Task<List<   ReklamKusagi2      >> cek(veri.Varlik vari)
   {
     List <ReklamKusagi2> sonuc = await vari.ReklamKusagi2s
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<ReklamKusagi2?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    ReklamKusagi2 ? sonuc = await vari.ReklamKusagi2s
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class ReklamKusagi2Cizelgesi
{




/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<ReklamKusagi2>> ara(params Expression<Func<ReklamKusagi2, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<ReklamKusagi2>> ara(veri.Varlik vari, params Expression<Func<ReklamKusagi2, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.ReklamKusagi2s 
                  .Where(kosul).OrderByDescending(p => p.reklamKusagi2kimlik) 
         .ToListAsync(); 
} 



public static async Task<ReklamKusagi2?> tekliCekKos(Int32 kimlik, Varlik kime)
{
ReklamKusagi2? kayit = await kime.ReklamKusagi2s.FirstOrDefaultAsync(p => p.reklamKusagi2kimlik == kimlik && p.varmi == true);
 return kayit;
}


public static async Task kaydetKos(ReklamKusagi2 yeni, Varlik vari, params bool[] yedekAlinsinmi) 
{ 
    if (yeni.reklamKusagi2kimlik <= 0) 
   { 
      Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");  
     if (yeni.varmi == null)  
         yeni.varmi = true;  
      await vari.ReklamKusagi2s.AddAsync(yeni);  
     await vari.SaveChangesAsync(); 
     await kay.kaydetKos(vari, yedekAlinsinmi); 
 } 
 else 
 { 
    Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi "); 
    ReklamKusagi2? bulunan = await vari.ReklamKusagi2s.FirstOrDefaultAsync(p => p.reklamKusagi2kimlik == yeni.reklamKusagi2kimlik); 
    if (bulunan == null)  
        return;  
   vari.Entry(bulunan).CurrentValues.SetValues(yeni);  
    await vari.SaveChangesAsync();  
    await kay.kaydetKos(vari, yedekAlinsinmi);  
 } 
} 


   public static async Task silKos(ReklamKusagi2 kimi, Varlik vari, params bool[] yedekAlinsinmi) 
  {
    Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
  kimi.varmi = false; 
  var bulunan = await vari.ReklamKusagi2s.FirstOrDefaultAsync(p => p.reklamKusagi2kimlik == kimi.reklamKusagi2kimlik); 
  if (bulunan == null) 
      return; 
  kimi.varmi = false;
  await vari.SaveChangesAsync();
   await kay.kaydetKos(vari, yedekAlinsinmi);
 }


public static ReklamKusagi2? tekliCek(Int32 kimlik, Varlik kime)
{
ReklamKusagi2 ? kayit = kime.ReklamKusagi2s.FirstOrDefault(p => p.reklamKusagi2kimlik == kimlik); 
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
 public static void kaydet(ReklamKusagi2 yeni, Varlik kime, params bool[] yedekAlinsinmi)
{
  if(yeni.reklamKusagi2kimlik <= 0  )
{
 if(yeni.varmi == null)  
   yeni.varmi = true ;       
      kime.ReklamKusagi2s.Add(yeni);
kime.SaveChanges();
Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
kay.kaydet(yedekAlinsinmi);
}
else
{

     var bulunan = kime.ReklamKusagi2s.FirstOrDefault(p => p.reklamKusagi2kimlik == yeni.reklamKusagi2kimlik);
     if (bulunan == null)
        return;
     kime.Entry(bulunan).CurrentValues.SetValues(yeni); 
kime.SaveChanges();

Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
kay.kaydet(yedekAlinsinmi);
}
}


  public static void sil(ReklamKusagi2 kimi, Varlik kime )
{
    kimi.varmi = false; 
 var bulunan = kime.ReklamKusagi2s.FirstOrDefault(p => p.reklamKusagi2kimlik == kimi.reklamKusagi2kimlik);  
 if (bulunan == null) 
     return; 
  kime.Entry(bulunan).CurrentValues.SetValues(kimi);  
  kime.SaveChanges(); 
  Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi"); 
  kay.kaydet(); 

}
}
}


