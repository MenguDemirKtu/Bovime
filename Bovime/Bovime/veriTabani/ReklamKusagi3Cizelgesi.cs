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

public class ReklamKusagi3Arama
{
 public  Int32  ?  reklamKusagi3kimlik {get;set;}
 public  string  ?  baslik {get;set;}
 public  string  ?  metin {get;set;}
 public  string  ?  hedefUrl {get;set;}
 public  Int64  ?  i_fotoKimlik {get;set;}
 public  Int32  ?  sirasi {get;set;}
 public  bool  ?  varmi {get;set;}
 public ReklamKusagi3Arama()
{
this.varmi = true;
}
 
        private ExpressionStarter<ReklamKusagi3> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< ReklamKusagi3>(P => P.varmi == true);
 if (reklamKusagi3kimlik  != null)
 predicate = predicate.And(x => x.reklamKusagi3kimlik == reklamKusagi3kimlik ); 
 if (baslik  != null)
               predicate = predicate.And( x => x.baslik != null &&    x.baslik .Contains(baslik));
 if (metin  != null)
               predicate = predicate.And( x => x.metin != null &&    x.metin .Contains(metin));
 if (hedefUrl  != null)
               predicate = predicate.And( x => x.hedefUrl != null &&    x.hedefUrl .Contains(hedefUrl));
 if (i_fotoKimlik  != null)
 predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik ); 
 if (sirasi  != null)
 predicate = predicate.And(x => x.sirasi == sirasi ); 
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
return  predicate;
 
}
      public async Task<List<   ReklamKusagi3      >> cek(veri.Varlik vari)
   {
     List <ReklamKusagi3> sonuc = await vari.ReklamKusagi3s
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<ReklamKusagi3?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    ReklamKusagi3 ? sonuc = await vari.ReklamKusagi3s
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class ReklamKusagi3Cizelgesi
{




/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<ReklamKusagi3>> ara(params Expression<Func<ReklamKusagi3, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<ReklamKusagi3>> ara(veri.Varlik vari, params Expression<Func<ReklamKusagi3, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.ReklamKusagi3s 
                  .Where(kosul).OrderByDescending(p => p.reklamKusagi3kimlik) 
         .ToListAsync(); 
} 



public static async Task<ReklamKusagi3?> tekliCekKos(Int32 kimlik, Varlik kime)
{
ReklamKusagi3? kayit = await kime.ReklamKusagi3s.FirstOrDefaultAsync(p => p.reklamKusagi3kimlik == kimlik && p.varmi == true);
 return kayit;
}


public static async Task kaydetKos(ReklamKusagi3 yeni, Varlik vari, params bool[] yedekAlinsinmi) 
{ 
    if (yeni.reklamKusagi3kimlik <= 0) 
   { 
      Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");  
     if (yeni.varmi == null)  
         yeni.varmi = true;  
      await vari.ReklamKusagi3s.AddAsync(yeni);  
     await vari.SaveChangesAsync(); 
     await kay.kaydetKos(vari, yedekAlinsinmi); 
 } 
 else 
 { 
    Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi "); 
    ReklamKusagi3? bulunan = await vari.ReklamKusagi3s.FirstOrDefaultAsync(p => p.reklamKusagi3kimlik == yeni.reklamKusagi3kimlik); 
    if (bulunan == null)  
        return;  
   vari.Entry(bulunan).CurrentValues.SetValues(yeni);  
    await vari.SaveChangesAsync();  
    await kay.kaydetKos(vari, yedekAlinsinmi);  
 } 
} 


   public static async Task silKos(ReklamKusagi3 kimi, Varlik vari, params bool[] yedekAlinsinmi) 
  {
    Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
  kimi.varmi = false; 
  var bulunan = await vari.ReklamKusagi3s.FirstOrDefaultAsync(p => p.reklamKusagi3kimlik == kimi.reklamKusagi3kimlik); 
  if (bulunan == null) 
      return; 
  kimi.varmi = false;
  await vari.SaveChangesAsync();
   await kay.kaydetKos(vari, yedekAlinsinmi);
 }


public static ReklamKusagi3? tekliCek(Int32 kimlik, Varlik kime)
{
ReklamKusagi3 ? kayit = kime.ReklamKusagi3s.FirstOrDefault(p => p.reklamKusagi3kimlik == kimlik); 
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
 public static void kaydet(ReklamKusagi3 yeni, Varlik kime, params bool[] yedekAlinsinmi)
{
  if(yeni.reklamKusagi3kimlik <= 0  )
{
 if(yeni.varmi == null)  
   yeni.varmi = true ;       
      kime.ReklamKusagi3s.Add(yeni);
kime.SaveChanges();
Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
kay.kaydet(yedekAlinsinmi);
}
else
{

     var bulunan = kime.ReklamKusagi3s.FirstOrDefault(p => p.reklamKusagi3kimlik == yeni.reklamKusagi3kimlik);
     if (bulunan == null)
        return;
     kime.Entry(bulunan).CurrentValues.SetValues(yeni); 
kime.SaveChanges();

Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
kay.kaydet(yedekAlinsinmi);
}
}


  public static void sil(ReklamKusagi3 kimi, Varlik kime )
{
    kimi.varmi = false; 
 var bulunan = kime.ReklamKusagi3s.FirstOrDefault(p => p.reklamKusagi3kimlik == kimi.reklamKusagi3kimlik);  
 if (bulunan == null) 
     return; 
  kime.Entry(bulunan).CurrentValues.SetValues(kimi);  
  kime.SaveChanges(); 
  Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi"); 
  kay.kaydet(); 

}
}
}


