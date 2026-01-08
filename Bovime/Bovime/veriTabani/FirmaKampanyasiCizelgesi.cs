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

public class FirmaKampanyasiArama
{
 public  Int64  ?  firmaKampanyasikimlik {get;set;}
 public  Int32  ?  i_firmaKimlik {get;set;}
 public  Int64  ?  i_fotoKimlik {get;set;}
 public  string  ?  aciklama {get;set;}
 public  DateTime  ?  yayinBaslangic {get;set;}
 public  DateTime  ?  yayinBitis {get;set;}
 public  bool  ?  e_onaylandiMi {get;set;}
 public  string  ?  hedefUrl {get;set;}
 public  string  ?  onKod {get;set;}
 public  Int32  ?  sirasi {get;set;}
 public  bool  ?  e_yayindaMi {get;set;}
 public  bool  ?  varmi {get;set;}
 public  string  ?  baslik {get;set;}
 public  string  ?  metin {get;set;}
 public FirmaKampanyasiArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<FirmaKampanyasi> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< FirmaKampanyasi>(P => P.varmi == true);
 if (firmaKampanyasikimlik  != null)
 predicate = predicate.And(x => x.firmaKampanyasikimlik == firmaKampanyasikimlik ); 
 if (i_firmaKimlik  != null)
 predicate = predicate.And(x => x.i_firmaKimlik == i_firmaKimlik ); 
 if (i_fotoKimlik  != null)
 predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik ); 
 if (aciklama  != null)
               predicate = predicate.And( x => x.aciklama != null &&    x.aciklama .Contains(aciklama));
 if (yayinBaslangic  != null)
 predicate = predicate.And(x => x.yayinBaslangic == yayinBaslangic ); 
 if (yayinBitis  != null)
 predicate = predicate.And(x => x.yayinBitis == yayinBitis ); 
 if (e_onaylandiMi  != null)
 predicate = predicate.And(x => x.e_onaylandiMi == e_onaylandiMi ); 
 if (hedefUrl  != null)
               predicate = predicate.And( x => x.hedefUrl != null &&    x.hedefUrl .Contains(hedefUrl));
 if (onKod  != null)
               predicate = predicate.And( x => x.onKod != null &&    x.onKod .Contains(onKod));
 if (sirasi  != null)
 predicate = predicate.And(x => x.sirasi == sirasi ); 
 if (e_yayindaMi  != null)
 predicate = predicate.And(x => x.e_yayindaMi == e_yayindaMi ); 
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
 if (baslik  != null)
               predicate = predicate.And( x => x.baslik != null &&    x.baslik .Contains(baslik));
 if (metin  != null)
               predicate = predicate.And( x => x.metin != null &&    x.metin .Contains(metin));
return  predicate;
 
}
      public async Task<List<   FirmaKampanyasi      >> cek(veri.Varlik vari)
   {
     List <FirmaKampanyasi> sonuc = await vari.FirmaKampanyasis
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<FirmaKampanyasi?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    FirmaKampanyasi ? sonuc = await vari.FirmaKampanyasis
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class FirmaKampanyasiCizelgesi
{




/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<FirmaKampanyasi>> ara(params Expression<Func<FirmaKampanyasi, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<FirmaKampanyasi>> ara(veri.Varlik vari, params Expression<Func<FirmaKampanyasi, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.FirmaKampanyasis 
                  .Where(kosul).OrderByDescending(p => p.firmaKampanyasikimlik) 
         .ToListAsync(); 
} 
      public static async Task< FirmaKampanyasi ?> bul(veri.Varlik vari, params Expression<Func<FirmaKampanyasi, bool>>[] kosullar)
    {
      var kosul = Vt.Birlestir(kosullar);
       return await vari.FirmaKampanyasis.FirstOrDefaultAsync(kosul);
   }



public static async Task<FirmaKampanyasi?> tekliCekKos(Int64 kimlik, Varlik kime)
{
FirmaKampanyasi? kayit = await kime.FirmaKampanyasis.FirstOrDefaultAsync(p => p.firmaKampanyasikimlik == kimlik && p.varmi == true);
 return kayit;
}


public static async Task kaydetKos(FirmaKampanyasi yeni, Varlik vari, params bool[] yedekAlinsinmi) 
{ 
    if (yeni.firmaKampanyasikimlik <= 0) 
   { 
      Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");  
     if (yeni.varmi == null)  
         yeni.varmi = true;  
      await vari.FirmaKampanyasis.AddAsync(yeni);  
     await vari.SaveChangesAsync(); 
     await kay.kaydetKos(vari, yedekAlinsinmi); 
 } 
 else 
 { 
    Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi "); 
    FirmaKampanyasi? bulunan = await vari.FirmaKampanyasis.FirstOrDefaultAsync(p => p.firmaKampanyasikimlik == yeni.firmaKampanyasikimlik); 
    if (bulunan == null)  
        return;  
   vari.Entry(bulunan).CurrentValues.SetValues(yeni);  
    await vari.SaveChangesAsync();  
    await kay.kaydetKos(vari, yedekAlinsinmi);  
 } 
} 


   public static async Task silKos(FirmaKampanyasi kimi, Varlik vari, params bool[] yedekAlinsinmi) 
  {
    Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
  kimi.varmi = false; 
  var bulunan = await vari.FirmaKampanyasis.FirstOrDefaultAsync(p => p.firmaKampanyasikimlik == kimi.firmaKampanyasikimlik); 
  if (bulunan == null) 
      return; 
  kimi.varmi = false;
  await vari.SaveChangesAsync();
   await kay.kaydetKos(vari, yedekAlinsinmi);
 }


public static FirmaKampanyasi? tekliCek(Int64 kimlik, Varlik kime)
{
FirmaKampanyasi ? kayit = kime.FirmaKampanyasis.FirstOrDefault(p => p.firmaKampanyasikimlik == kimlik); 
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
 public static void kaydet(FirmaKampanyasi yeni, Varlik kime, params bool[] yedekAlinsinmi)
{
  if(yeni.firmaKampanyasikimlik <= 0  )
{
 if(yeni.varmi == null)  
   yeni.varmi = true ;       
      kime.FirmaKampanyasis.Add(yeni);
kime.SaveChanges();
Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
kay.kaydet(yedekAlinsinmi);
}
else
{

     var bulunan = kime.FirmaKampanyasis.FirstOrDefault(p => p.firmaKampanyasikimlik == yeni.firmaKampanyasikimlik);
     if (bulunan == null)
        return;
     kime.Entry(bulunan).CurrentValues.SetValues(yeni); 
kime.SaveChanges();

Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
kay.kaydet(yedekAlinsinmi);
}
}


  public static void sil(FirmaKampanyasi kimi, Varlik kime )
{
    kimi.varmi = false; 
 var bulunan = kime.FirmaKampanyasis.FirstOrDefault(p => p.firmaKampanyasikimlik == kimi.firmaKampanyasikimlik);  
 if (bulunan == null) 
     return; 
  kime.Entry(bulunan).CurrentValues.SetValues(kimi);  
  kime.SaveChanges(); 
  Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi"); 
  kay.kaydet(); 

}
}
}


