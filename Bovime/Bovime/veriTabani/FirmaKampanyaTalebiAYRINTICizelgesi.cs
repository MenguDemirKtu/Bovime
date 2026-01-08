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

public class FirmaKampanyaTalebiAYRINTIArama
{
 public  Int32  ?  firmaKampanyaTalebikimlik {get;set;}
 public  Int32  ?  i_firmaKimlik {get;set;}
 public  string  ?  firmaAdi {get;set;}
 public  Int32  ?  i_firmaDurumuKimlik {get;set;}
 public  Int64  ?  i_fotoKimlik {get;set;}
 public  string  ?  metin {get;set;}
 public  Int32  ?  sirasi {get;set;}
 public  bool  ?  e_gorulduMu {get;set;}
 public  bool  ?  varmi {get;set;}
 public  DateTime  ?  girisTarihi {get;set;}
 public  string  ?  fotosu {get;set;}
 public  string  ?  baslik {get;set;}
 public FirmaKampanyaTalebiAYRINTIArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<FirmaKampanyaTalebiAYRINTI> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< FirmaKampanyaTalebiAYRINTI>(P => P.varmi == true);
 if (firmaKampanyaTalebikimlik  != null)
 predicate = predicate.And(x => x.firmaKampanyaTalebikimlik == firmaKampanyaTalebikimlik ); 
 if (i_firmaKimlik  != null)
 predicate = predicate.And(x => x.i_firmaKimlik == i_firmaKimlik ); 
 if (firmaAdi  != null)
               predicate = predicate.And( x => x.firmaAdi != null &&    x.firmaAdi .Contains(firmaAdi));
 if (i_firmaDurumuKimlik  != null)
 predicate = predicate.And(x => x.i_firmaDurumuKimlik == i_firmaDurumuKimlik ); 
 if (i_fotoKimlik  != null)
 predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik ); 
 if (metin  != null)
               predicate = predicate.And( x => x.metin != null &&    x.metin .Contains(metin));
 if (sirasi  != null)
 predicate = predicate.And(x => x.sirasi == sirasi ); 
 if (e_gorulduMu  != null)
 predicate = predicate.And(x => x.e_gorulduMu == e_gorulduMu ); 
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
 if (girisTarihi  != null)
 predicate = predicate.And(x => x.girisTarihi == girisTarihi ); 
 if (fotosu  != null)
               predicate = predicate.And( x => x.fotosu != null &&    x.fotosu .Contains(fotosu));
 if (baslik  != null)
               predicate = predicate.And( x => x.baslik != null &&    x.baslik .Contains(baslik));
return  predicate;
 
}
      public async Task<List<   FirmaKampanyaTalebiAYRINTI      >> cek(veri.Varlik vari)
   {
     List <FirmaKampanyaTalebiAYRINTI> sonuc = await vari.FirmaKampanyaTalebiAYRINTIs
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<FirmaKampanyaTalebiAYRINTI?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    FirmaKampanyaTalebiAYRINTI ? sonuc = await vari.FirmaKampanyaTalebiAYRINTIs
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class FirmaKampanyaTalebiAYRINTICizelgesi
{





/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<FirmaKampanyaTalebiAYRINTI>> ara(params Expression<Func<FirmaKampanyaTalebiAYRINTI, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<FirmaKampanyaTalebiAYRINTI>> ara(veri.Varlik vari, params Expression<Func<FirmaKampanyaTalebiAYRINTI, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.FirmaKampanyaTalebiAYRINTIs 
                  .Where(kosul).OrderByDescending(p => p.firmaKampanyaTalebikimlik) 
         .ToListAsync(); 
} 
      public static async Task< FirmaKampanyaTalebiAYRINTI ?> bul(veri.Varlik vari, params Expression<Func<FirmaKampanyaTalebiAYRINTI, bool>>[] kosullar)
    {
      var kosul = Vt.Birlestir(kosullar);
       return await vari.FirmaKampanyaTalebiAYRINTIs.FirstOrDefaultAsync(kosul);
   }



public static async Task<FirmaKampanyaTalebiAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
{
FirmaKampanyaTalebiAYRINTI? kayit = await kime.FirmaKampanyaTalebiAYRINTIs.FirstOrDefaultAsync(p => p.firmaKampanyaTalebikimlik == kimlik && p.varmi == true);
 return kayit;
}




public static FirmaKampanyaTalebiAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
{
FirmaKampanyaTalebiAYRINTI ? kayit = kime.FirmaKampanyaTalebiAYRINTIs.FirstOrDefault(p => p.firmaKampanyaTalebikimlik == kimlik); 
 if (kayit != null)   
    if (kayit.varmi != true) 
      return null; 
return kayit;
}
}
}

