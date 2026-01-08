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

public class FirmaKampanyasiAYRINTIArama
{
 public  Int64  ?  firmaKampanyasikimlik {get;set;}
 public  Int32  ?  i_firmaKimlik {get;set;}
 public  string  ?  firmaAdi {get;set;}
 public  Int32  ?  i_firmaDurumuKimlik {get;set;}
 public  Int64  ?  i_fotoKimlik {get;set;}
 public  string  ?  aciklama {get;set;}
 public  DateTime  ?  yayinBaslangic {get;set;}
 public  DateTime  ?  yayinBitis {get;set;}
 public  string  ?  hedefUrl {get;set;}
 public  string  ?  onKod {get;set;}
 public  Int32  ?  sirasi {get;set;}
 public  bool  ?  varmi {get;set;}
 public  string  ?  fotosu {get;set;}
 public  string  ?  baslik {get;set;}
 public  string  ?  firmaUrl {get;set;}
 public  Int32  ?  firmaPuani {get;set;}
 public  string  ?  metin {get;set;}
 public  Int32  ?  y_firmaKampanyaTalebiKimlik {get;set;}
 public  Int32  ?  i_kampanyaDurumuKimlik {get;set;}
 public  string  ?  KampanyaDurumuAdi {get;set;}
 public FirmaKampanyasiAYRINTIArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<FirmaKampanyasiAYRINTI> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< FirmaKampanyasiAYRINTI>(P => P.varmi == true);
 if (firmaKampanyasikimlik  != null)
 predicate = predicate.And(x => x.firmaKampanyasikimlik == firmaKampanyasikimlik ); 
 if (i_firmaKimlik  != null)
 predicate = predicate.And(x => x.i_firmaKimlik == i_firmaKimlik ); 
 if (firmaAdi  != null)
               predicate = predicate.And( x => x.firmaAdi != null &&    x.firmaAdi .Contains(firmaAdi));
 if (i_firmaDurumuKimlik  != null)
 predicate = predicate.And(x => x.i_firmaDurumuKimlik == i_firmaDurumuKimlik ); 
 if (i_fotoKimlik  != null)
 predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik ); 
 if (aciklama  != null)
               predicate = predicate.And( x => x.aciklama != null &&    x.aciklama .Contains(aciklama));
 if (yayinBaslangic  != null)
 predicate = predicate.And(x => x.yayinBaslangic == yayinBaslangic ); 
 if (yayinBitis  != null)
 predicate = predicate.And(x => x.yayinBitis == yayinBitis ); 
 if (hedefUrl  != null)
               predicate = predicate.And( x => x.hedefUrl != null &&    x.hedefUrl .Contains(hedefUrl));
 if (onKod  != null)
               predicate = predicate.And( x => x.onKod != null &&    x.onKod .Contains(onKod));
 if (sirasi  != null)
 predicate = predicate.And(x => x.sirasi == sirasi ); 
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
 if (fotosu  != null)
               predicate = predicate.And( x => x.fotosu != null &&    x.fotosu .Contains(fotosu));
 if (baslik  != null)
               predicate = predicate.And( x => x.baslik != null &&    x.baslik .Contains(baslik));
 if (firmaUrl  != null)
               predicate = predicate.And( x => x.firmaUrl != null &&    x.firmaUrl .Contains(firmaUrl));
 if (firmaPuani  != null)
 predicate = predicate.And(x => x.firmaPuani == firmaPuani ); 
 if (metin  != null)
               predicate = predicate.And( x => x.metin != null &&    x.metin .Contains(metin));
 if (y_firmaKampanyaTalebiKimlik  != null)
 predicate = predicate.And(x => x.y_firmaKampanyaTalebiKimlik == y_firmaKampanyaTalebiKimlik ); 
 if (i_kampanyaDurumuKimlik  != null)
 predicate = predicate.And(x => x.i_kampanyaDurumuKimlik == i_kampanyaDurumuKimlik ); 
 if (KampanyaDurumuAdi  != null)
               predicate = predicate.And( x => x.KampanyaDurumuAdi != null &&    x.KampanyaDurumuAdi .Contains(KampanyaDurumuAdi));
return  predicate;
 
}
      public async Task<List<   FirmaKampanyasiAYRINTI      >> cek(veri.Varlik vari)
   {
     List <FirmaKampanyasiAYRINTI> sonuc = await vari.FirmaKampanyasiAYRINTIs
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<FirmaKampanyasiAYRINTI?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    FirmaKampanyasiAYRINTI ? sonuc = await vari.FirmaKampanyasiAYRINTIs
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class FirmaKampanyasiAYRINTICizelgesi
{





/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<FirmaKampanyasiAYRINTI>> ara(params Expression<Func<FirmaKampanyasiAYRINTI, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<FirmaKampanyasiAYRINTI>> ara(veri.Varlik vari, params Expression<Func<FirmaKampanyasiAYRINTI, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.FirmaKampanyasiAYRINTIs 
                  .Where(kosul).OrderByDescending(p => p.firmaKampanyasikimlik) 
         .ToListAsync(); 
} 
      public static async Task< FirmaKampanyasiAYRINTI ?> bul(veri.Varlik vari, params Expression<Func<FirmaKampanyasiAYRINTI, bool>>[] kosullar)
    {
      var kosul = Vt.Birlestir(kosullar);
       return await vari.FirmaKampanyasiAYRINTIs.FirstOrDefaultAsync(kosul);
   }



public static async Task<FirmaKampanyasiAYRINTI?> tekliCekKos(Int64 kimlik, Varlik kime)
{
FirmaKampanyasiAYRINTI? kayit = await kime.FirmaKampanyasiAYRINTIs.FirstOrDefaultAsync(p => p.firmaKampanyasikimlik == kimlik && p.varmi == true);
 return kayit;
}




public static FirmaKampanyasiAYRINTI? tekliCek(Int64 kimlik, Varlik kime)
{
FirmaKampanyasiAYRINTI ? kayit = kime.FirmaKampanyasiAYRINTIs.FirstOrDefault(p => p.firmaKampanyasikimlik == kimlik); 
 if (kayit != null)   
    if (kayit.varmi != true) 
      return null; 
return kayit;
}
}
}

