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

public class SikSorulanSorularArama
{
 public  Int32  ?  sikSorulanSorularkimlik {get;set;}
 public  string  ?  soru {get;set;}
 public  string  ?  yanit {get;set;}
 public  Int32  ?  sirasi {get;set;}
 public  bool  ?  varmi {get;set;}
 public SikSorulanSorularArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<SikSorulanSorular> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< SikSorulanSorular>(P => P.varmi == true);
 if (sikSorulanSorularkimlik  != null)
 predicate = predicate.And(x => x.sikSorulanSorularkimlik == sikSorulanSorularkimlik ); 
 if (soru  != null)
               predicate = predicate.And( x => x.soru != null &&    x.soru .Contains(soru));
 if (yanit  != null)
               predicate = predicate.And( x => x.yanit != null &&    x.yanit .Contains(yanit));
 if (sirasi  != null)
 predicate = predicate.And(x => x.sirasi == sirasi ); 
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
return  predicate;
 
}
      public async Task<List<   SikSorulanSorular      >> cek(veri.Varlik vari)
   {
     List <SikSorulanSorular> sonuc = await vari.SikSorulanSorulars
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<SikSorulanSorular?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    SikSorulanSorular ? sonuc = await vari.SikSorulanSorulars
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class SikSorulanSorularCizelgesi
{




/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<SikSorulanSorular>> ara(params Expression<Func<SikSorulanSorular, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<SikSorulanSorular>> ara(veri.Varlik vari, params Expression<Func<SikSorulanSorular, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.SikSorulanSorulars 
                  .Where(kosul).OrderByDescending(p => p.sikSorulanSorularkimlik) 
         .ToListAsync(); 
} 
      public static async Task< SikSorulanSorular ?> bul(veri.Varlik vari, params Expression<Func<SikSorulanSorular, bool>>[] kosullar)
    {
      var kosul = Vt.Birlestir(kosullar);
       return await vari.SikSorulanSorulars.FirstOrDefaultAsync(kosul);
   }



public static async Task<SikSorulanSorular?> tekliCekKos(Int32 kimlik, Varlik kime)
{
SikSorulanSorular? kayit = await kime.SikSorulanSorulars.FirstOrDefaultAsync(p => p.sikSorulanSorularkimlik == kimlik && p.varmi == true);
 return kayit;
}


public static async Task kaydetKos(SikSorulanSorular yeni, Varlik vari, params bool[] yedekAlinsinmi) 
{ 
    if (yeni.sikSorulanSorularkimlik <= 0) 
   { 
      Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");  
     if (yeni.varmi == null)  
         yeni.varmi = true;  
      await vari.SikSorulanSorulars.AddAsync(yeni);  
     await vari.SaveChangesAsync(); 
     await kay.kaydetKos(vari, yedekAlinsinmi); 
 } 
 else 
 { 
    Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi "); 
    SikSorulanSorular? bulunan = await vari.SikSorulanSorulars.FirstOrDefaultAsync(p => p.sikSorulanSorularkimlik == yeni.sikSorulanSorularkimlik); 
    if (bulunan == null)  
        return;  
   vari.Entry(bulunan).CurrentValues.SetValues(yeni);  
    await vari.SaveChangesAsync();  
    await kay.kaydetKos(vari, yedekAlinsinmi);  
 } 
} 


   public static async Task silKos(SikSorulanSorular kimi, Varlik vari, params bool[] yedekAlinsinmi) 
  {
    Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
  kimi.varmi = false; 
  var bulunan = await vari.SikSorulanSorulars.FirstOrDefaultAsync(p => p.sikSorulanSorularkimlik == kimi.sikSorulanSorularkimlik); 
  if (bulunan == null) 
      return; 
  kimi.varmi = false;
  await vari.SaveChangesAsync();
   await kay.kaydetKos(vari, yedekAlinsinmi);
 }


public static SikSorulanSorular? tekliCek(Int32 kimlik, Varlik kime)
{
SikSorulanSorular ? kayit = kime.SikSorulanSorulars.FirstOrDefault(p => p.sikSorulanSorularkimlik == kimlik); 
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
 public static void kaydet(SikSorulanSorular yeni, Varlik kime, params bool[] yedekAlinsinmi)
{
  if(yeni.sikSorulanSorularkimlik <= 0  )
{
 if(yeni.varmi == null)  
   yeni.varmi = true ;       
      kime.SikSorulanSorulars.Add(yeni);
kime.SaveChanges();
Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
kay.kaydet(yedekAlinsinmi);
}
else
{

     var bulunan = kime.SikSorulanSorulars.FirstOrDefault(p => p.sikSorulanSorularkimlik == yeni.sikSorulanSorularkimlik);
     if (bulunan == null)
        return;
     kime.Entry(bulunan).CurrentValues.SetValues(yeni); 
kime.SaveChanges();

Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
kay.kaydet(yedekAlinsinmi);
}
}


  public static void sil(SikSorulanSorular kimi, Varlik kime )
{
    kimi.varmi = false; 
 var bulunan = kime.SikSorulanSorulars.FirstOrDefault(p => p.sikSorulanSorularkimlik == kimi.sikSorulanSorularkimlik);  
 if (bulunan == null) 
     return; 
  kime.Entry(bulunan).CurrentValues.SetValues(kimi);  
  kime.SaveChanges(); 
  Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi"); 
  kay.kaydet(); 

}
}
}


