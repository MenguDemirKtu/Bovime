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

public class SatisArama
{
 public  Int64  ?  satisKimlik {get;set;}
 public  Int32  ?  i_firmaKimlik {get;set;}
 public  string  ?  kodu {get;set;}
 public  bool  ?  varmi {get;set;}
 public  Int64  ?  i_uyeKimlik {get;set;}
 public  Int64  ?  y_firmaKampanyasiKimlik {get;set;}
 public  Int32  ?  i_satisDurumuKimlik {get;set;}
 public  DateTime  ?  tarih {get;set;}
 public SatisArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<Satis> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< Satis>(P => P.varmi == true);
 if (satisKimlik  != null)
 predicate = predicate.And(x => x.satisKimlik == satisKimlik ); 
 if (i_firmaKimlik  != null)
 predicate = predicate.And(x => x.i_firmaKimlik == i_firmaKimlik ); 
 if (kodu  != null)
               predicate = predicate.And( x => x.kodu != null &&    x.kodu .Contains(kodu));
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
 if (i_uyeKimlik  != null)
 predicate = predicate.And(x => x.i_uyeKimlik == i_uyeKimlik ); 
 if (y_firmaKampanyasiKimlik  != null)
 predicate = predicate.And(x => x.y_firmaKampanyasiKimlik == y_firmaKampanyasiKimlik ); 
 if (i_satisDurumuKimlik  != null)
 predicate = predicate.And(x => x.i_satisDurumuKimlik == i_satisDurumuKimlik ); 
 if (tarih  != null)
 predicate = predicate.And(x => x.tarih == tarih ); 
return  predicate;
 
}
      public async Task<List<   Satis      >> cek(veri.Varlik vari)
   {
     List <Satis> sonuc = await vari.Satiss
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<Satis?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    Satis ? sonuc = await vari.Satiss
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class SatisCizelgesi
{




/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<Satis>> ara(params Expression<Func<Satis, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<Satis>> ara(veri.Varlik vari, params Expression<Func<Satis, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.Satiss 
                  .Where(kosul).OrderByDescending(p => p.satisKimlik) 
         .ToListAsync(); 
} 
      public static async Task< Satis ?> bul(veri.Varlik vari, params Expression<Func<Satis, bool>>[] kosullar)
    {
      var kosul = Vt.Birlestir(kosullar);
       return await vari.Satiss.FirstOrDefaultAsync(kosul);
   }



public static async Task<Satis?> tekliCekKos(Int64 kimlik, Varlik kime)
{
Satis? kayit = await kime.Satiss.FirstOrDefaultAsync(p => p.satisKimlik == kimlik && p.varmi == true);
 return kayit;
}


public static async Task kaydetKos(Satis yeni, Varlik vari, params bool[] yedekAlinsinmi) 
{ 
    if (yeni.satisKimlik <= 0) 
   { 
      Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");  
     if (yeni.varmi == null)  
         yeni.varmi = true;  
      await vari.Satiss.AddAsync(yeni);  
     await vari.SaveChangesAsync(); 
     await kay.kaydetKos(vari, yedekAlinsinmi); 
 } 
 else 
 { 
    Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi "); 
    Satis? bulunan = await vari.Satiss.FirstOrDefaultAsync(p => p.satisKimlik == yeni.satisKimlik); 
    if (bulunan == null)  
        return;  
   vari.Entry(bulunan).CurrentValues.SetValues(yeni);  
    await vari.SaveChangesAsync();  
    await kay.kaydetKos(vari, yedekAlinsinmi);  
 } 
} 


   public static async Task silKos(Satis kimi, Varlik vari, params bool[] yedekAlinsinmi) 
  {
    Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
  kimi.varmi = false; 
  var bulunan = await vari.Satiss.FirstOrDefaultAsync(p => p.satisKimlik == kimi.satisKimlik); 
  if (bulunan == null) 
      return; 
  kimi.varmi = false;
  await vari.SaveChangesAsync();
   await kay.kaydetKos(vari, yedekAlinsinmi);
 }


public static Satis? tekliCek(Int64 kimlik, Varlik kime)
{
Satis ? kayit = kime.Satiss.FirstOrDefault(p => p.satisKimlik == kimlik); 
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
 public static void kaydet(Satis yeni, Varlik kime, params bool[] yedekAlinsinmi)
{
  if(yeni.satisKimlik <= 0  )
{
 if(yeni.varmi == null)  
   yeni.varmi = true ;       
      kime.Satiss.Add(yeni);
kime.SaveChanges();
Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
kay.kaydet(yedekAlinsinmi);
}
else
{

     var bulunan = kime.Satiss.FirstOrDefault(p => p.satisKimlik == yeni.satisKimlik);
     if (bulunan == null)
        return;
     kime.Entry(bulunan).CurrentValues.SetValues(yeni); 
kime.SaveChanges();

Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
kay.kaydet(yedekAlinsinmi);
}
}


  public static void sil(Satis kimi, Varlik kime )
{
    kimi.varmi = false; 
 var bulunan = kime.Satiss.FirstOrDefault(p => p.satisKimlik == kimi.satisKimlik);  
 if (bulunan == null) 
     return; 
  kime.Entry(bulunan).CurrentValues.SetValues(kimi);  
  kime.SaveChanges(); 
  Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi"); 
  kay.kaydet(); 

}
}
}


