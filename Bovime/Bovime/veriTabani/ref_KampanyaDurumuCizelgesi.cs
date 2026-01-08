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

public class ref_KampanyaDurumuArama
{
 public  Int32  ?  KampanyaDurumuKimlik {get;set;}
 public  string  ?  KampanyaDurumuAdi {get;set;}
 public ref_KampanyaDurumuArama()
{
}
 
        private ExpressionStarter<ref_KampanyaDurumu> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< ref_KampanyaDurumu>();
 if (KampanyaDurumuKimlik  != null)
 predicate = predicate.And(x => x.KampanyaDurumuKimlik == KampanyaDurumuKimlik ); 
 if (KampanyaDurumuAdi  != null)
               predicate = predicate.And( x => x.KampanyaDurumuAdi != null &&    x.KampanyaDurumuAdi .Contains(KampanyaDurumuAdi));
return  predicate;
 
}
      public async Task<List<   ref_KampanyaDurumu      >> cek(veri.Varlik vari)
   {
     List <ref_KampanyaDurumu> sonuc = await vari.ref_KampanyaDurumus
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<ref_KampanyaDurumu?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    ref_KampanyaDurumu ? sonuc = await vari.ref_KampanyaDurumus
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class ref_KampanyaDurumuCizelgesi
{




/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<ref_KampanyaDurumu>> ara(params Expression<Func<ref_KampanyaDurumu, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<ref_KampanyaDurumu>> ara(veri.Varlik vari, params Expression<Func<ref_KampanyaDurumu, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.ref_KampanyaDurumus 
                  .Where(kosul).OrderByDescending(p => p.KampanyaDurumuKimlik) 
         .ToListAsync(); 
} 
      public static async Task< ref_KampanyaDurumu ?> bul(veri.Varlik vari, params Expression<Func<ref_KampanyaDurumu, bool>>[] kosullar)
    {
      var kosul = Vt.Birlestir(kosullar);
       return await vari.ref_KampanyaDurumus.FirstOrDefaultAsync(kosul);
   }



public static async Task<ref_KampanyaDurumu?> tekliCekKos(Int32 kimlik, Varlik kime)
{
ref_KampanyaDurumu? kayit = await kime.ref_KampanyaDurumus.FirstOrDefaultAsync(p => p.KampanyaDurumuKimlik == kimlik);
 return kayit;
}




public static ref_KampanyaDurumu? tekliCek(Int32 kimlik, Varlik kime)
{
ref_KampanyaDurumu ? kayit = kime.ref_KampanyaDurumus.FirstOrDefault(p => p.KampanyaDurumuKimlik == kimlik); 
return kayit;
}


/// <summary> 
 /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır. 
  /// </summary> 
  /// <param name="yeni"></param> 
  /// <param name="kime"></param> 
  /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param> 
 public static void kaydet(ref_KampanyaDurumu yeni, Varlik kime, params bool[] yedekAlinsinmi)
{
  if(yeni.KampanyaDurumuKimlik <= 0  )
{
          kime.ref_KampanyaDurumus.Add(yeni);
kime.SaveChanges();
Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
kay.kaydet(yedekAlinsinmi);
}
else
{

     var bulunan = kime.ref_KampanyaDurumus.FirstOrDefault(p => p.KampanyaDurumuKimlik == yeni.KampanyaDurumuKimlik);
     if (bulunan == null)
        return;
     kime.Entry(bulunan).CurrentValues.SetValues(yeni); 
kime.SaveChanges();

Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
kay.kaydet(yedekAlinsinmi);
}
}


  public static void sil(ref_KampanyaDurumu kimi, Varlik kime )
{

}
}
}


