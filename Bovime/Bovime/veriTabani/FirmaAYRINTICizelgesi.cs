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

public class FirmaAYRINTIArama
{
 public  Int32  ?  firmakimlik {get;set;}
 public  string  ?  firmaAdi {get;set;}
 public  Int32  ?  i_firmaDurumuKimlik {get;set;}
 public  string  ?  FirmaDurumuAdi {get;set;}
 public  Int64  ?  i_fotoKimlik {get;set;}
 public  string  ?  fotosu {get;set;}
 public  string  ?  telefon {get;set;}
 public  string  ?  telefon2 {get;set;}
 public  string  ?  ePosta {get;set;}
 public  string  ?  adres {get;set;}
 public  string  ?  tanitim {get;set;}
 public  string  ?  konum {get;set;}
 public  Int32  ?  sirasi {get;set;}
 public  Int32  ?  puani {get;set;}
 public  string  ?  aciklama {get;set;}
 public  bool  ?  varmi {get;set;}
 public  string  ?  firmaUrl {get;set;}
 public FirmaAYRINTIArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<FirmaAYRINTI> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< FirmaAYRINTI>(P => P.varmi == true);
 if (firmakimlik  != null)
 predicate = predicate.And(x => x.firmakimlik == firmakimlik ); 
 if (firmaAdi  != null)
               predicate = predicate.And( x => x.firmaAdi != null &&    x.firmaAdi .Contains(firmaAdi));
 if (i_firmaDurumuKimlik  != null)
 predicate = predicate.And(x => x.i_firmaDurumuKimlik == i_firmaDurumuKimlik ); 
 if (FirmaDurumuAdi  != null)
               predicate = predicate.And( x => x.FirmaDurumuAdi != null &&    x.FirmaDurumuAdi .Contains(FirmaDurumuAdi));
 if (i_fotoKimlik  != null)
 predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik ); 
 if (fotosu  != null)
               predicate = predicate.And( x => x.fotosu != null &&    x.fotosu .Contains(fotosu));
 if (telefon  != null)
               predicate = predicate.And( x => x.telefon != null &&    x.telefon .Contains(telefon));
 if (telefon2  != null)
               predicate = predicate.And( x => x.telefon2 != null &&    x.telefon2 .Contains(telefon2));
 if (ePosta  != null)
               predicate = predicate.And( x => x.ePosta != null &&    x.ePosta .Contains(ePosta));
 if (adres  != null)
               predicate = predicate.And( x => x.adres != null &&    x.adres .Contains(adres));
 if (tanitim  != null)
               predicate = predicate.And( x => x.tanitim != null &&    x.tanitim .Contains(tanitim));
 if (konum  != null)
               predicate = predicate.And( x => x.konum != null &&    x.konum .Contains(konum));
 if (sirasi  != null)
 predicate = predicate.And(x => x.sirasi == sirasi ); 
 if (puani  != null)
 predicate = predicate.And(x => x.puani == puani ); 
 if (aciklama  != null)
               predicate = predicate.And( x => x.aciklama != null &&    x.aciklama .Contains(aciklama));
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
 if (firmaUrl  != null)
               predicate = predicate.And( x => x.firmaUrl != null &&    x.firmaUrl .Contains(firmaUrl));
return  predicate;
 
}
      public async Task<List<   FirmaAYRINTI      >> cek(veri.Varlik vari)
   {
     List <FirmaAYRINTI> sonuc = await vari.FirmaAYRINTIs
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<FirmaAYRINTI?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    FirmaAYRINTI ? sonuc = await vari.FirmaAYRINTIs
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class FirmaAYRINTICizelgesi
{





/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<FirmaAYRINTI>> ara(params Expression<Func<FirmaAYRINTI, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<FirmaAYRINTI>> ara(veri.Varlik vari, params Expression<Func<FirmaAYRINTI, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.FirmaAYRINTIs 
                  .Where(kosul).OrderByDescending(p => p.firmakimlik) 
         .ToListAsync(); 
} 
      public static async Task< FirmaAYRINTI ?> bul(veri.Varlik vari, params Expression<Func<FirmaAYRINTI, bool>>[] kosullar)
    {
      var kosul = Vt.Birlestir(kosullar);
       return await vari.FirmaAYRINTIs.FirstOrDefaultAsync(kosul);
   }



public static async Task<FirmaAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
{
FirmaAYRINTI? kayit = await kime.FirmaAYRINTIs.FirstOrDefaultAsync(p => p.firmakimlik == kimlik && p.varmi == true);
 return kayit;
}




public static FirmaAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
{
FirmaAYRINTI ? kayit = kime.FirmaAYRINTIs.FirstOrDefault(p => p.firmakimlik == kimlik); 
 if (kayit != null)   
    if (kayit.varmi != true) 
      return null; 
return kayit;
}
}
}

