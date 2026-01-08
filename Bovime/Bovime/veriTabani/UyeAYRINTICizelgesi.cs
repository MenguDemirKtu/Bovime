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

public class UyeAYRINTIArama
{
 public  Int64  ?  uyekimlik {get;set;}
 public  string  ?  adi {get;set;}
 public  string  ?  soyadi {get;set;}
 public  string  ?  telefon {get;set;}
 public  string  ?  ePosta {get;set;}
 public  DateTime  ?  uyelikTarihi {get;set;}
 public  bool  ?  varmi {get;set;}
 public UyeAYRINTIArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<UyeAYRINTI> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< UyeAYRINTI>(P => P.varmi == true);
 if (uyekimlik  != null)
 predicate = predicate.And(x => x.uyekimlik == uyekimlik ); 
 if (adi  != null)
               predicate = predicate.And( x => x.adi != null &&    x.adi .Contains(adi));
 if (soyadi  != null)
               predicate = predicate.And( x => x.soyadi != null &&    x.soyadi .Contains(soyadi));
 if (telefon  != null)
               predicate = predicate.And( x => x.telefon != null &&    x.telefon .Contains(telefon));
 if (ePosta  != null)
               predicate = predicate.And( x => x.ePosta != null &&    x.ePosta .Contains(ePosta));
 if (uyelikTarihi  != null)
 predicate = predicate.And(x => x.uyelikTarihi == uyelikTarihi ); 
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
return  predicate;
 
}
      public async Task<List<   UyeAYRINTI      >> cek(veri.Varlik vari)
   {
     List <UyeAYRINTI> sonuc = await vari.UyeAYRINTIs
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<UyeAYRINTI?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    UyeAYRINTI ? sonuc = await vari.UyeAYRINTIs
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class UyeAYRINTICizelgesi
{





/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<UyeAYRINTI>> ara(params Expression<Func<UyeAYRINTI, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<UyeAYRINTI>> ara(veri.Varlik vari, params Expression<Func<UyeAYRINTI, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.UyeAYRINTIs 
                  .Where(kosul).OrderByDescending(p => p.uyekimlik) 
         .ToListAsync(); 
} 
      public static async Task< UyeAYRINTI ?> bul(veri.Varlik vari, params Expression<Func<UyeAYRINTI, bool>>[] kosullar)
    {
      var kosul = Vt.Birlestir(kosullar);
       return await vari.UyeAYRINTIs.FirstOrDefaultAsync(kosul);
   }



public static async Task<UyeAYRINTI?> tekliCekKos(Int64 kimlik, Varlik kime)
{
UyeAYRINTI? kayit = await kime.UyeAYRINTIs.FirstOrDefaultAsync(p => p.uyekimlik == kimlik && p.varmi == true);
 return kayit;
}




public static UyeAYRINTI? tekliCek(Int64 kimlik, Varlik kime)
{
UyeAYRINTI ? kayit = kime.UyeAYRINTIs.FirstOrDefault(p => p.uyekimlik == kimlik); 
 if (kayit != null)   
    if (kayit.varmi != true) 
      return null; 
return kayit;
}
}
}

