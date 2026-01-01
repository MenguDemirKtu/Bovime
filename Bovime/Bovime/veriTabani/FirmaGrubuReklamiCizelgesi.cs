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

public class FirmaGrubuReklamiArama
{
 public  Int32  ?  firmaGrubuReklamikimlik {get;set;}
 public  string  ?  firmaGrubuBaslik {get;set;}
 public  Int64  ?  i_fotoKimlik {get;set;}
 public  string  ?  kisaTanitim {get;set;}
 public  Int32  ?  sirasi {get;set;}
 public  bool  ?  varmi {get;set;}
 public FirmaGrubuReklamiArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<FirmaGrubuReklami> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< FirmaGrubuReklami>(P => P.varmi == true);
 if (firmaGrubuReklamikimlik  != null)
 predicate = predicate.And(x => x.firmaGrubuReklamikimlik == firmaGrubuReklamikimlik ); 
 if (firmaGrubuBaslik  != null)
               predicate = predicate.And( x => x.firmaGrubuBaslik != null &&    x.firmaGrubuBaslik .Contains(firmaGrubuBaslik));
 if (i_fotoKimlik  != null)
 predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik ); 
 if (kisaTanitim  != null)
               predicate = predicate.And( x => x.kisaTanitim != null &&    x.kisaTanitim .Contains(kisaTanitim));
 if (sirasi  != null)
 predicate = predicate.And(x => x.sirasi == sirasi ); 
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
return  predicate;
 
}
      public async Task<List<   FirmaGrubuReklami      >> cek(veri.Varlik vari)
   {
     List <FirmaGrubuReklami> sonuc = await vari.FirmaGrubuReklamis
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<FirmaGrubuReklami?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    FirmaGrubuReklami ? sonuc = await vari.FirmaGrubuReklamis
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class FirmaGrubuReklamiCizelgesi
{




/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<FirmaGrubuReklami>> ara(params Expression<Func<FirmaGrubuReklami, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<FirmaGrubuReklami>> ara(veri.Varlik vari, params Expression<Func<FirmaGrubuReklami, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.FirmaGrubuReklamis 
                  .Where(kosul).OrderByDescending(p => p.firmaGrubuReklamikimlik) 
         .ToListAsync(); 
} 



public static async Task<FirmaGrubuReklami?> tekliCekKos(Int32 kimlik, Varlik kime)
{
FirmaGrubuReklami? kayit = await kime.FirmaGrubuReklamis.FirstOrDefaultAsync(p => p.firmaGrubuReklamikimlik == kimlik && p.varmi == true);
 return kayit;
}


public static async Task kaydetKos(FirmaGrubuReklami yeni, Varlik vari, params bool[] yedekAlinsinmi) 
{ 
    if (yeni.firmaGrubuReklamikimlik <= 0) 
   { 
      Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");  
     if (yeni.varmi == null)  
         yeni.varmi = true;  
      await vari.FirmaGrubuReklamis.AddAsync(yeni);  
     await vari.SaveChangesAsync(); 
     await kay.kaydetKos(vari, yedekAlinsinmi); 
 } 
 else 
 { 
    Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi "); 
    FirmaGrubuReklami? bulunan = await vari.FirmaGrubuReklamis.FirstOrDefaultAsync(p => p.firmaGrubuReklamikimlik == yeni.firmaGrubuReklamikimlik); 
    if (bulunan == null)  
        return;  
   vari.Entry(bulunan).CurrentValues.SetValues(yeni);  
    await vari.SaveChangesAsync();  
    await kay.kaydetKos(vari, yedekAlinsinmi);  
 } 
} 


   public static async Task silKos(FirmaGrubuReklami kimi, Varlik vari, params bool[] yedekAlinsinmi) 
  {
    Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
  kimi.varmi = false; 
  var bulunan = await vari.FirmaGrubuReklamis.FirstOrDefaultAsync(p => p.firmaGrubuReklamikimlik == kimi.firmaGrubuReklamikimlik); 
  if (bulunan == null) 
      return; 
  kimi.varmi = false;
  await vari.SaveChangesAsync();
   await kay.kaydetKos(vari, yedekAlinsinmi);
 }


public static FirmaGrubuReklami? tekliCek(Int32 kimlik, Varlik kime)
{
FirmaGrubuReklami ? kayit = kime.FirmaGrubuReklamis.FirstOrDefault(p => p.firmaGrubuReklamikimlik == kimlik); 
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
 public static void kaydet(FirmaGrubuReklami yeni, Varlik kime, params bool[] yedekAlinsinmi)
{
  if(yeni.firmaGrubuReklamikimlik <= 0  )
{
 if(yeni.varmi == null)  
   yeni.varmi = true ;       
      kime.FirmaGrubuReklamis.Add(yeni);
kime.SaveChanges();
Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
kay.kaydet(yedekAlinsinmi);
}
else
{

     var bulunan = kime.FirmaGrubuReklamis.FirstOrDefault(p => p.firmaGrubuReklamikimlik == yeni.firmaGrubuReklamikimlik);
     if (bulunan == null)
        return;
     kime.Entry(bulunan).CurrentValues.SetValues(yeni); 
kime.SaveChanges();

Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
kay.kaydet(yedekAlinsinmi);
}
}


  public static void sil(FirmaGrubuReklami kimi, Varlik kime )
{
    kimi.varmi = false; 
 var bulunan = kime.FirmaGrubuReklamis.FirstOrDefault(p => p.firmaGrubuReklamikimlik == kimi.firmaGrubuReklamikimlik);  
 if (bulunan == null) 
     return; 
  kime.Entry(bulunan).CurrentValues.SetValues(kimi);  
  kime.SaveChanges(); 
  Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi"); 
  kay.kaydet(); 

}
}
}


