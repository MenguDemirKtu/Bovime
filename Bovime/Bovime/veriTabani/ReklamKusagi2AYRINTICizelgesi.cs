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

public class ReklamKusagi2AYRINTIArama
{
 public  Int32  ?  reklamKusagi2kimlik {get;set;}
 public  string  ?  baslik {get;set;}
 public  string  ?  metin {get;set;}
 public  string  ?  hefefUrl {get;set;}
 public  Int64  ?  i_fotoKimlik {get;set;}
 public  string  ?  fotosu {get;set;}
 public  Int32  ?  sirasi {get;set;}
 public  bool  ?  varmi {get;set;}
 public ReklamKusagi2AYRINTIArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<ReklamKusagi2AYRINTI> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< ReklamKusagi2AYRINTI>(P => P.varmi == true);
 if (reklamKusagi2kimlik  != null)
 predicate = predicate.And(x => x.reklamKusagi2kimlik == reklamKusagi2kimlik ); 
 if (baslik  != null)
               predicate = predicate.And( x => x.baslik != null &&    x.baslik .Contains(baslik));
 if (metin  != null)
               predicate = predicate.And( x => x.metin != null &&    x.metin .Contains(metin));
 if (hefefUrl  != null)
               predicate = predicate.And( x => x.hefefUrl != null &&    x.hefefUrl .Contains(hefefUrl));
 if (i_fotoKimlik  != null)
 predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik ); 
 if (fotosu  != null)
               predicate = predicate.And( x => x.fotosu != null &&    x.fotosu .Contains(fotosu));
 if (sirasi  != null)
 predicate = predicate.And(x => x.sirasi == sirasi ); 
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
return  predicate;
 
}
      public async Task<List<   ReklamKusagi2AYRINTI      >> cek(veri.Varlik vari)
   {
     List <ReklamKusagi2AYRINTI> sonuc = await vari.ReklamKusagi2AYRINTIs
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<ReklamKusagi2AYRINTI?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    ReklamKusagi2AYRINTI ? sonuc = await vari.ReklamKusagi2AYRINTIs
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class ReklamKusagi2AYRINTICizelgesi
{





/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<ReklamKusagi2AYRINTI>> ara(params Expression<Func<ReklamKusagi2AYRINTI, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<ReklamKusagi2AYRINTI>> ara(veri.Varlik vari, params Expression<Func<ReklamKusagi2AYRINTI, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.ReklamKusagi2AYRINTIs 
                  .Where(kosul).OrderByDescending(p => p.reklamKusagi2kimlik) 
         .ToListAsync(); 
} 



public static async Task<ReklamKusagi2AYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
{
ReklamKusagi2AYRINTI? kayit = await kime.ReklamKusagi2AYRINTIs.FirstOrDefaultAsync(p => p.reklamKusagi2kimlik == kimlik && p.varmi == true);
 return kayit;
}




public static ReklamKusagi2AYRINTI? tekliCek(Int32 kimlik, Varlik kime)
{
ReklamKusagi2AYRINTI ? kayit = kime.ReklamKusagi2AYRINTIs.FirstOrDefault(p => p.reklamKusagi2kimlik == kimlik); 
 if (kayit != null)   
    if (kayit.varmi != true) 
      return null; 
return kayit;
}
}
}

