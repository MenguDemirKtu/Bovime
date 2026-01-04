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

public class AramaMotoruArama
{
 public  Int32  ?  aramaMotorukimlik {get;set;}
 public  string  ?  sayfaAdresi {get;set;}
 public  string  ?  title {get;set;}
 public  string  ?  keywords {get;set;}
 public  Int32  ?  goruntulenmeSayisi {get;set;}
 public  bool  ?  varmi {get;set;}
 public AramaMotoruArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<AramaMotoru> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< AramaMotoru>(P => P.varmi == true);
 if (aramaMotorukimlik  != null)
 predicate = predicate.And(x => x.aramaMotorukimlik == aramaMotorukimlik ); 
 if (sayfaAdresi  != null)
               predicate = predicate.And( x => x.sayfaAdresi != null &&    x.sayfaAdresi .Contains(sayfaAdresi));
 if (title  != null)
               predicate = predicate.And( x => x.title != null &&    x.title .Contains(title));
 if (keywords  != null)
               predicate = predicate.And( x => x.keywords != null &&    x.keywords .Contains(keywords));
 if (goruntulenmeSayisi  != null)
 predicate = predicate.And(x => x.goruntulenmeSayisi == goruntulenmeSayisi ); 
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
return  predicate;
 
}
      public async Task<List<   AramaMotoru      >> cek(veri.Varlik vari)
   {
     List <AramaMotoru> sonuc = await vari.AramaMotorus
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<AramaMotoru?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    AramaMotoru ? sonuc = await vari.AramaMotorus
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class AramaMotoruCizelgesi
{




/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<AramaMotoru>> ara(params Expression<Func<AramaMotoru, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<AramaMotoru>> ara(veri.Varlik vari, params Expression<Func<AramaMotoru, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.AramaMotorus 
                  .Where(kosul).OrderByDescending(p => p.aramaMotorukimlik) 
         .ToListAsync(); 
} 



public static async Task<AramaMotoru?> tekliCekKos(Int32 kimlik, Varlik kime)
{
AramaMotoru? kayit = await kime.AramaMotorus.FirstOrDefaultAsync(p => p.aramaMotorukimlik == kimlik && p.varmi == true);
 return kayit;
}


public static async Task kaydetKos(AramaMotoru yeni, Varlik vari, params bool[] yedekAlinsinmi) 
{ 
    if (yeni.aramaMotorukimlik <= 0) 
   { 
      Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");  
     if (yeni.varmi == null)  
         yeni.varmi = true;  
      await vari.AramaMotorus.AddAsync(yeni);  
     await vari.SaveChangesAsync(); 
     await kay.kaydetKos(vari, yedekAlinsinmi); 
 } 
 else 
 { 
    Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi "); 
    AramaMotoru? bulunan = await vari.AramaMotorus.FirstOrDefaultAsync(p => p.aramaMotorukimlik == yeni.aramaMotorukimlik); 
    if (bulunan == null)  
        return;  
   vari.Entry(bulunan).CurrentValues.SetValues(yeni);  
    await vari.SaveChangesAsync();  
    await kay.kaydetKos(vari, yedekAlinsinmi);  
 } 
} 


   public static async Task silKos(AramaMotoru kimi, Varlik vari, params bool[] yedekAlinsinmi) 
  {
    Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
  kimi.varmi = false; 
  var bulunan = await vari.AramaMotorus.FirstOrDefaultAsync(p => p.aramaMotorukimlik == kimi.aramaMotorukimlik); 
  if (bulunan == null) 
      return; 
  kimi.varmi = false;
  await vari.SaveChangesAsync();
   await kay.kaydetKos(vari, yedekAlinsinmi);
 }


public static AramaMotoru? tekliCek(Int32 kimlik, Varlik kime)
{
AramaMotoru ? kayit = kime.AramaMotorus.FirstOrDefault(p => p.aramaMotorukimlik == kimlik); 
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
 public static void kaydet(AramaMotoru yeni, Varlik kime, params bool[] yedekAlinsinmi)
{
  if(yeni.aramaMotorukimlik <= 0  )
{
 if(yeni.varmi == null)  
   yeni.varmi = true ;       
      kime.AramaMotorus.Add(yeni);
kime.SaveChanges();
Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
kay.kaydet(yedekAlinsinmi);
}
else
{

     var bulunan = kime.AramaMotorus.FirstOrDefault(p => p.aramaMotorukimlik == yeni.aramaMotorukimlik);
     if (bulunan == null)
        return;
     kime.Entry(bulunan).CurrentValues.SetValues(yeni); 
kime.SaveChanges();

Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
kay.kaydet(yedekAlinsinmi);
}
}


  public static void sil(AramaMotoru kimi, Varlik kime )
{
    kimi.varmi = false; 
 var bulunan = kime.AramaMotorus.FirstOrDefault(p => p.aramaMotorukimlik == kimi.aramaMotorukimlik);  
 if (bulunan == null) 
     return; 
  kime.Entry(bulunan).CurrentValues.SetValues(kimi);  
  kime.SaveChanges(); 
  Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi"); 
  kay.kaydet(); 

}
}
}


