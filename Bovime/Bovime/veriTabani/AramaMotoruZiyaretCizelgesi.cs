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

public class AramaMotoruZiyaretArama
{
 public  Int64  ?  aramaMotoruZiyaretkimlik {get;set;}
 public  Int32  ?  i_aramaMotoruKimlik {get;set;}
 public  DateTime  ?  tarih {get;set;}
 public  string  ?  ipAdresi {get;set;}
 public  bool  ?  varmi {get;set;}
 public AramaMotoruZiyaretArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<AramaMotoruZiyaret> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< AramaMotoruZiyaret>(P => P.varmi == true);
 if (aramaMotoruZiyaretkimlik  != null)
 predicate = predicate.And(x => x.aramaMotoruZiyaretkimlik == aramaMotoruZiyaretkimlik ); 
 if (i_aramaMotoruKimlik  != null)
 predicate = predicate.And(x => x.i_aramaMotoruKimlik == i_aramaMotoruKimlik ); 
 if (tarih  != null)
 predicate = predicate.And(x => x.tarih == tarih ); 
 if (ipAdresi  != null)
               predicate = predicate.And( x => x.ipAdresi != null &&    x.ipAdresi .Contains(ipAdresi));
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
return  predicate;
 
}
      public async Task<List<   AramaMotoruZiyaret      >> cek(veri.Varlik vari)
   {
     List <AramaMotoruZiyaret> sonuc = await vari.AramaMotoruZiyarets
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<AramaMotoruZiyaret?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    AramaMotoruZiyaret ? sonuc = await vari.AramaMotoruZiyarets
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class AramaMotoruZiyaretCizelgesi
{




/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<AramaMotoruZiyaret>> ara(params Expression<Func<AramaMotoruZiyaret, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<AramaMotoruZiyaret>> ara(veri.Varlik vari, params Expression<Func<AramaMotoruZiyaret, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.AramaMotoruZiyarets 
                  .Where(kosul).OrderByDescending(p => p.aramaMotoruZiyaretkimlik) 
         .ToListAsync(); 
} 



public static async Task<AramaMotoruZiyaret?> tekliCekKos(Int64 kimlik, Varlik kime)
{
AramaMotoruZiyaret? kayit = await kime.AramaMotoruZiyarets.FirstOrDefaultAsync(p => p.aramaMotoruZiyaretkimlik == kimlik && p.varmi == true);
 return kayit;
}


public static async Task kaydetKos(AramaMotoruZiyaret yeni, Varlik vari, params bool[] yedekAlinsinmi) 
{ 
    if (yeni.aramaMotoruZiyaretkimlik <= 0) 
   { 
      Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");  
     if (yeni.varmi == null)  
         yeni.varmi = true;  
      await vari.AramaMotoruZiyarets.AddAsync(yeni);  
     await vari.SaveChangesAsync(); 
     await kay.kaydetKos(vari, yedekAlinsinmi); 
 } 
 else 
 { 
    Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi "); 
    AramaMotoruZiyaret? bulunan = await vari.AramaMotoruZiyarets.FirstOrDefaultAsync(p => p.aramaMotoruZiyaretkimlik == yeni.aramaMotoruZiyaretkimlik); 
    if (bulunan == null)  
        return;  
   vari.Entry(bulunan).CurrentValues.SetValues(yeni);  
    await vari.SaveChangesAsync();  
    await kay.kaydetKos(vari, yedekAlinsinmi);  
 } 
} 


   public static async Task silKos(AramaMotoruZiyaret kimi, Varlik vari, params bool[] yedekAlinsinmi) 
  {
    Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
  kimi.varmi = false; 
  var bulunan = await vari.AramaMotoruZiyarets.FirstOrDefaultAsync(p => p.aramaMotoruZiyaretkimlik == kimi.aramaMotoruZiyaretkimlik); 
  if (bulunan == null) 
      return; 
  kimi.varmi = false;
  await vari.SaveChangesAsync();
   await kay.kaydetKos(vari, yedekAlinsinmi);
 }


public static AramaMotoruZiyaret? tekliCek(Int64 kimlik, Varlik kime)
{
AramaMotoruZiyaret ? kayit = kime.AramaMotoruZiyarets.FirstOrDefault(p => p.aramaMotoruZiyaretkimlik == kimlik); 
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
 public static void kaydet(AramaMotoruZiyaret yeni, Varlik kime, params bool[] yedekAlinsinmi)
{
  if(yeni.aramaMotoruZiyaretkimlik <= 0  )
{
 if(yeni.varmi == null)  
   yeni.varmi = true ;       
      kime.AramaMotoruZiyarets.Add(yeni);
kime.SaveChanges();
Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
kay.kaydet(yedekAlinsinmi);
}
else
{

     var bulunan = kime.AramaMotoruZiyarets.FirstOrDefault(p => p.aramaMotoruZiyaretkimlik == yeni.aramaMotoruZiyaretkimlik);
     if (bulunan == null)
        return;
     kime.Entry(bulunan).CurrentValues.SetValues(yeni); 
kime.SaveChanges();

Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
kay.kaydet(yedekAlinsinmi);
}
}


  public static void sil(AramaMotoruZiyaret kimi, Varlik kime )
{
    kimi.varmi = false; 
 var bulunan = kime.AramaMotoruZiyarets.FirstOrDefault(p => p.aramaMotoruZiyaretkimlik == kimi.aramaMotoruZiyaretkimlik);  
 if (bulunan == null) 
     return; 
  kime.Entry(bulunan).CurrentValues.SetValues(kimi);  
  kime.SaveChanges(); 
  Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi"); 
  kay.kaydet(); 

}
}
}


