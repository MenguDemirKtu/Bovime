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

public class PaketArama
{
 public  Int32  ?  paketKimlik {get;set;}
 public  string  ?  paketAdi {get;set;}
 public  Int64  ?  i_fotoKimlik {get;set;}
 public  string  ?  tanitim {get;set;}
 public  bool  ?  varmi {get;set;}
 public  Int32  ?  azamiIlanSayisi {get;set;}
 public PaketArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<Paket> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< Paket>(P => P.varmi == true);
 if (paketKimlik  != null)
 predicate = predicate.And(x => x.paketKimlik == paketKimlik ); 
 if (paketAdi  != null)
               predicate = predicate.And( x => x.paketAdi != null &&    x.paketAdi .Contains(paketAdi));
 if (i_fotoKimlik  != null)
 predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik ); 
 if (tanitim  != null)
               predicate = predicate.And( x => x.tanitim != null &&    x.tanitim .Contains(tanitim));
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
 if (azamiIlanSayisi  != null)
 predicate = predicate.And(x => x.azamiIlanSayisi == azamiIlanSayisi ); 
return  predicate;
 
}
      public async Task<List<   Paket      >> cek(veri.Varlik vari)
   {
     List <Paket> sonuc = await vari.Pakets
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<Paket?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    Paket ? sonuc = await vari.Pakets
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class PaketCizelgesi
{




/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<Paket>> ara(params Expression<Func<Paket, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<Paket>> ara(veri.Varlik vari, params Expression<Func<Paket, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.Pakets 
                  .Where(kosul).OrderByDescending(p => p.paketKimlik) 
         .ToListAsync(); 
} 
      public static async Task< Paket ?> bul(veri.Varlik vari, params Expression<Func<Paket, bool>>[] kosullar)
    {
      var kosul = Vt.Birlestir(kosullar);
       return await vari.Pakets.FirstOrDefaultAsync(kosul);
   }



public static async Task<Paket?> tekliCekKos(Int32 kimlik, Varlik kime)
{
Paket? kayit = await kime.Pakets.FirstOrDefaultAsync(p => p.paketKimlik == kimlik && p.varmi == true);
 return kayit;
}


public static async Task kaydetKos(Paket yeni, Varlik vari, params bool[] yedekAlinsinmi) 
{ 
    if (yeni.paketKimlik <= 0) 
   { 
      Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");  
     if (yeni.varmi == null)  
         yeni.varmi = true;  
      await vari.Pakets.AddAsync(yeni);  
     await vari.SaveChangesAsync(); 
     await kay.kaydetKos(vari, yedekAlinsinmi); 
 } 
 else 
 { 
    Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi "); 
    Paket? bulunan = await vari.Pakets.FirstOrDefaultAsync(p => p.paketKimlik == yeni.paketKimlik); 
    if (bulunan == null)  
        return;  
   vari.Entry(bulunan).CurrentValues.SetValues(yeni);  
    await vari.SaveChangesAsync();  
    await kay.kaydetKos(vari, yedekAlinsinmi);  
 } 
} 


   public static async Task silKos(Paket kimi, Varlik vari, params bool[] yedekAlinsinmi) 
  {
    Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
  kimi.varmi = false; 
  var bulunan = await vari.Pakets.FirstOrDefaultAsync(p => p.paketKimlik == kimi.paketKimlik); 
  if (bulunan == null) 
      return; 
  kimi.varmi = false;
  await vari.SaveChangesAsync();
   await kay.kaydetKos(vari, yedekAlinsinmi);
 }


public static Paket? tekliCek(Int32 kimlik, Varlik kime)
{
Paket ? kayit = kime.Pakets.FirstOrDefault(p => p.paketKimlik == kimlik); 
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
 public static void kaydet(Paket yeni, Varlik kime, params bool[] yedekAlinsinmi)
{
  if(yeni.paketKimlik <= 0  )
{
 if(yeni.varmi == null)  
   yeni.varmi = true ;       
      kime.Pakets.Add(yeni);
kime.SaveChanges();
Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
kay.kaydet(yedekAlinsinmi);
}
else
{

     var bulunan = kime.Pakets.FirstOrDefault(p => p.paketKimlik == yeni.paketKimlik);
     if (bulunan == null)
        return;
     kime.Entry(bulunan).CurrentValues.SetValues(yeni); 
kime.SaveChanges();

Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
kay.kaydet(yedekAlinsinmi);
}
}


  public static void sil(Paket kimi, Varlik kime )
{
    kimi.varmi = false; 
 var bulunan = kime.Pakets.FirstOrDefault(p => p.paketKimlik == kimi.paketKimlik);  
 if (bulunan == null) 
     return; 
  kime.Entry(bulunan).CurrentValues.SetValues(kimi);  
  kime.SaveChanges(); 
  Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi"); 
  kay.kaydet(); 

}
}
}


