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

public class FirmaBasvurusuAYRINTIArama
{
 public  Int32  ?  firmaBasvurusukimlik {get;set;}
 public  string  ?  firmaAdi {get;set;}
 public  string  ?  tel {get;set;}
 public  string  ?  ePosta {get;set;}
 public  string  ?  metin {get;set;}
 public  bool  ?  e_gorulduMu {get;set;}
 public  string  ?  gorulduMu {get;set;}
 public  DateTime  ?  tarih {get;set;}
 public  bool  ?  varmi {get;set;}
 public FirmaBasvurusuAYRINTIArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<FirmaBasvurusuAYRINTI> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< FirmaBasvurusuAYRINTI>(P => P.varmi == true);
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
 if (gorulduMu  != null)
               predicate = predicate.And( x => x.gorulduMu != null &&    x.gorulduMu .Contains(gorulduMu));
 if (tarih  != null)
 predicate = predicate.And(x => x.tarih == tarih ); 
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
return  predicate;
 
}
      public async Task<List<   FirmaBasvurusuAYRINTI      >> cek(veri.Varlik vari)
   {
     List <FirmaBasvurusuAYRINTI> sonuc = await vari.FirmaBasvurusuAYRINTIs
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<FirmaBasvurusuAYRINTI?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    FirmaBasvurusuAYRINTI ? sonuc = await vari.FirmaBasvurusuAYRINTIs
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class FirmaBasvurusuAYRINTICizelgesi
{





/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<FirmaBasvurusuAYRINTI>> ara(params Expression<Func<FirmaBasvurusuAYRINTI, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<FirmaBasvurusuAYRINTI>> ara(veri.Varlik vari, params Expression<Func<FirmaBasvurusuAYRINTI, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.FirmaBasvurusuAYRINTIs 
                  .Where(kosul).OrderByDescending(p => p.firmaBasvurusukimlik) 
         .ToListAsync(); 
} 
      public static async Task< FirmaBasvurusuAYRINTI ?> bul(veri.Varlik vari, params Expression<Func<FirmaBasvurusuAYRINTI, bool>>[] kosullar)
    {
      var kosul = Vt.Birlestir(kosullar);
       return await vari.FirmaBasvurusuAYRINTIs.FirstOrDefaultAsync(kosul);
   }



public static async Task<FirmaBasvurusuAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
{
FirmaBasvurusuAYRINTI? kayit = await kime.FirmaBasvurusuAYRINTIs.FirstOrDefaultAsync(p => p.firmaBasvurusukimlik == kimlik && p.varmi == true);
 return kayit;
}




public static FirmaBasvurusuAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
{
FirmaBasvurusuAYRINTI ? kayit = kime.FirmaBasvurusuAYRINTIs.FirstOrDefault(p => p.firmaBasvurusukimlik == kimlik); 
 if (kayit != null)   
    if (kayit.varmi != true) 
      return null; 
return kayit;
}
}
}

