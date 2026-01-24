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

public class FirmaBasvurusuArama
{
 public  Int32  ?  firmaBasvurusukimlik {get;set;}
 public  string  ?  firmaAdi {get;set;}
 public  string  ?  tel {get;set;}
 public  string  ?  ePosta {get;set;}
 public  string  ?  metin {get;set;}
 public  bool  ?  e_gorulduMu {get;set;}
 public  DateTime  ?  tarih {get;set;}
 public  bool  ?  varmi {get;set;}
 public FirmaBasvurusuArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<FirmaBasvurusu> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< FirmaBasvurusu>(P => P.varmi == true);
 if (firmaBasvurusukimlik  != null)
 predicate = predicate.And(x => x.firmaBasvurusukimlik == firmaBasvurusukimlik ); 
 if (firmaAdi  != null)
               predicate = predicate.And( x => x.firmaAdi != null &&    x.firmaAdi .Contains(firmaAdi));
 if (tel  != null)
               predicate = predicate.And( x => x.tel != null &&    x.tel .Contains(tel));
 if (ePosta  != null)
               predicate = predicate.And( x => x.ePosta != null &&    x.ePosta .Contains(ePosta));
 if (metin  != null)
               predicate = predicate.And( x => x.metin != null &&    x.metin .Contains(metin));
 if (e_gorulduMu  != null)
 predicate = predicate.And(x => x.e_gorulduMu == e_gorulduMu ); 
 if (tarih  != null)
 predicate = predicate.And(x => x.tarih == tarih ); 
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
return  predicate;
 
}
      public async Task<List<   FirmaBasvurusu      >> cek(veri.Varlik vari)
   {
     List <FirmaBasvurusu> sonuc = await vari.FirmaBasvurusus
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<FirmaBasvurusu?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    FirmaBasvurusu ? sonuc = await vari.FirmaBasvurusus
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class FirmaBasvurusuCizelgesi
{




/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<FirmaBasvurusu>> ara(params Expression<Func<FirmaBasvurusu, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<FirmaBasvurusu>> ara(veri.Varlik vari, params Expression<Func<FirmaBasvurusu, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.FirmaBasvurusus 
                  .Where(kosul).OrderByDescending(p => p.firmaBasvurusukimlik) 
         .ToListAsync(); 
} 
      public static async Task< FirmaBasvurusu ?> bul(veri.Varlik vari, params Expression<Func<FirmaBasvurusu, bool>>[] kosullar)
    {
      var kosul = Vt.Birlestir(kosullar);
       return await vari.FirmaBasvurusus.FirstOrDefaultAsync(kosul);
   }



public static async Task<FirmaBasvurusu?> tekliCekKos(Int32 kimlik, Varlik kime)
{
FirmaBasvurusu? kayit = await kime.FirmaBasvurusus.FirstOrDefaultAsync(p => p.firmaBasvurusukimlik == kimlik && p.varmi == true);
 return kayit;
}


public static async Task kaydetKos(FirmaBasvurusu yeni, Varlik vari, params bool[] yedekAlinsinmi) 
{ 
    if (yeni.firmaBasvurusukimlik <= 0) 
   { 
      Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");  
     if (yeni.varmi == null)  
         yeni.varmi = true;  
      await vari.FirmaBasvurusus.AddAsync(yeni);  
     await vari.SaveChangesAsync(); 
     await kay.kaydetKos(vari, yedekAlinsinmi); 
 } 
 else 
 { 
    Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi "); 
    FirmaBasvurusu? bulunan = await vari.FirmaBasvurusus.FirstOrDefaultAsync(p => p.firmaBasvurusukimlik == yeni.firmaBasvurusukimlik); 
    if (bulunan == null)  
        return;  
   vari.Entry(bulunan).CurrentValues.SetValues(yeni);  
    await vari.SaveChangesAsync();  
    await kay.kaydetKos(vari, yedekAlinsinmi);  
 } 
} 


   public static async Task silKos(FirmaBasvurusu kimi, Varlik vari, params bool[] yedekAlinsinmi) 
  {
    Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
  kimi.varmi = false; 
  var bulunan = await vari.FirmaBasvurusus.FirstOrDefaultAsync(p => p.firmaBasvurusukimlik == kimi.firmaBasvurusukimlik); 
  if (bulunan == null) 
      return; 
  kimi.varmi = false;
  await vari.SaveChangesAsync();
   await kay.kaydetKos(vari, yedekAlinsinmi);
 }


public static FirmaBasvurusu? tekliCek(Int32 kimlik, Varlik kime)
{
FirmaBasvurusu ? kayit = kime.FirmaBasvurusus.FirstOrDefault(p => p.firmaBasvurusukimlik == kimlik); 
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
 public static void kaydet(FirmaBasvurusu yeni, Varlik kime, params bool[] yedekAlinsinmi)
{
  if(yeni.firmaBasvurusukimlik <= 0  )
{
 if(yeni.varmi == null)  
   yeni.varmi = true ;       
      kime.FirmaBasvurusus.Add(yeni);
kime.SaveChanges();
Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
kay.kaydet(yedekAlinsinmi);
}
else
{

     var bulunan = kime.FirmaBasvurusus.FirstOrDefault(p => p.firmaBasvurusukimlik == yeni.firmaBasvurusukimlik);
     if (bulunan == null)
        return;
     kime.Entry(bulunan).CurrentValues.SetValues(yeni); 
kime.SaveChanges();

Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
kay.kaydet(yedekAlinsinmi);
}
}


  public static void sil(FirmaBasvurusu kimi, Varlik kime )
{
    kimi.varmi = false; 
 var bulunan = kime.FirmaBasvurusus.FirstOrDefault(p => p.firmaBasvurusukimlik == kimi.firmaBasvurusukimlik);  
 if (bulunan == null) 
     return; 
  kime.Entry(bulunan).CurrentValues.SetValues(kimi);  
  kime.SaveChanges(); 
  Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi"); 
  kay.kaydet(); 

}
}
}


