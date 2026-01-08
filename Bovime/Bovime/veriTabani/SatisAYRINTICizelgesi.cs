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

public class SatisAYRINTIArama
{
 public  Int64  ?  satisKimlik {get;set;}
 public  Int32  ?  i_firmaKimlik {get;set;}
 public  string  ?  firmaAdi {get;set;}
 public  string  ?  kodu {get;set;}
 public  bool  ?  varmi {get;set;}
 public  Int64  ?  i_uyeKimlik {get;set;}
 public  string  ?  adi {get;set;}
 public  string  ?  soyadi {get;set;}
 public  Int64  ?  y_firmaKampanyasiKimlik {get;set;}
 public  Int32  ?  i_satisDurumuKimlik {get;set;}
 public  string  ?  SatisDurumuAdi {get;set;}
 public  DateTime  ?  tarih {get;set;}
 public  string  ?  kampanyaBasligi {get;set;}
 public SatisAYRINTIArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<SatisAYRINTI> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< SatisAYRINTI>(P => P.varmi == true);
 if (satisKimlik  != null)
 predicate = predicate.And(x => x.satisKimlik == satisKimlik ); 
 if (i_firmaKimlik  != null)
 predicate = predicate.And(x => x.i_firmaKimlik == i_firmaKimlik ); 
 if (firmaAdi  != null)
               predicate = predicate.And( x => x.firmaAdi != null &&    x.firmaAdi .Contains(firmaAdi));
 if (kodu  != null)
               predicate = predicate.And( x => x.kodu != null &&    x.kodu .Contains(kodu));
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
 if (i_uyeKimlik  != null)
 predicate = predicate.And(x => x.i_uyeKimlik == i_uyeKimlik ); 
 if (adi  != null)
               predicate = predicate.And( x => x.adi != null &&    x.adi .Contains(adi));
 if (soyadi  != null)
               predicate = predicate.And( x => x.soyadi != null &&    x.soyadi .Contains(soyadi));
 if (y_firmaKampanyasiKimlik  != null)
 predicate = predicate.And(x => x.y_firmaKampanyasiKimlik == y_firmaKampanyasiKimlik ); 
 if (i_satisDurumuKimlik  != null)
 predicate = predicate.And(x => x.i_satisDurumuKimlik == i_satisDurumuKimlik ); 
 if (SatisDurumuAdi  != null)
               predicate = predicate.And( x => x.SatisDurumuAdi != null &&    x.SatisDurumuAdi .Contains(SatisDurumuAdi));
 if (tarih  != null)
 predicate = predicate.And(x => x.tarih == tarih ); 
 if (kampanyaBasligi  != null)
               predicate = predicate.And( x => x.kampanyaBasligi != null &&    x.kampanyaBasligi .Contains(kampanyaBasligi));
return  predicate;
 
}
      public async Task<List<   SatisAYRINTI      >> cek(veri.Varlik vari)
   {
     List <SatisAYRINTI> sonuc = await vari.SatisAYRINTIs
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<SatisAYRINTI?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    SatisAYRINTI ? sonuc = await vari.SatisAYRINTIs
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class SatisAYRINTICizelgesi
{





/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<SatisAYRINTI>> ara(params Expression<Func<SatisAYRINTI, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<SatisAYRINTI>> ara(veri.Varlik vari, params Expression<Func<SatisAYRINTI, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.SatisAYRINTIs 
                  .Where(kosul).OrderByDescending(p => p.satisKimlik) 
         .ToListAsync(); 
} 
      public static async Task< SatisAYRINTI ?> bul(veri.Varlik vari, params Expression<Func<SatisAYRINTI, bool>>[] kosullar)
    {
      var kosul = Vt.Birlestir(kosullar);
       return await vari.SatisAYRINTIs.FirstOrDefaultAsync(kosul);
   }



public static async Task<SatisAYRINTI?> tekliCekKos(Int64 kimlik, Varlik kime)
{
SatisAYRINTI? kayit = await kime.SatisAYRINTIs.FirstOrDefaultAsync(p => p.satisKimlik == kimlik && p.varmi == true);
 return kayit;
}




public static SatisAYRINTI? tekliCek(Int64 kimlik, Varlik kime)
{
SatisAYRINTI ? kayit = kime.SatisAYRINTIs.FirstOrDefault(p => p.satisKimlik == kimlik); 
 if (kayit != null)   
    if (kayit.varmi != true) 
      return null; 
return kayit;
}
}
}

