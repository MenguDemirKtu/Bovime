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

public class SikSorulanSorularAYRINTIArama
{
 public  Int32  ?  sikSorulanSorularkimlik {get;set;}
 public  string  ?  soru {get;set;}
 public  string  ?  yanit {get;set;}
 public  Int32  ?  sirasi {get;set;}
 public  bool  ?  varmi {get;set;}
 public SikSorulanSorularAYRINTIArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<SikSorulanSorularAYRINTI> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< SikSorulanSorularAYRINTI>(P => P.varmi == true);
 if (sikSorulanSorularkimlik  != null)
 predicate = predicate.And(x => x.sikSorulanSorularkimlik == sikSorulanSorularkimlik ); 
 if (soru  != null)
               predicate = predicate.And( x => x.soru != null &&    x.soru .Contains(soru));
 if (yanit  != null)
               predicate = predicate.And( x => x.yanit != null &&    x.yanit .Contains(yanit));
 if (sirasi  != null)
 predicate = predicate.And(x => x.sirasi == sirasi ); 
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
return  predicate;
 
}
      public async Task<List<   SikSorulanSorularAYRINTI      >> cek(veri.Varlik vari)
   {
     List <SikSorulanSorularAYRINTI> sonuc = await vari.SikSorulanSorularAYRINTIs
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<SikSorulanSorularAYRINTI?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    SikSorulanSorularAYRINTI ? sonuc = await vari.SikSorulanSorularAYRINTIs
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class SikSorulanSorularAYRINTICizelgesi
{





/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<SikSorulanSorularAYRINTI>> ara(params Expression<Func<SikSorulanSorularAYRINTI, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<SikSorulanSorularAYRINTI>> ara(veri.Varlik vari, params Expression<Func<SikSorulanSorularAYRINTI, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.SikSorulanSorularAYRINTIs 
                  .Where(kosul).OrderByDescending(p => p.sikSorulanSorularkimlik) 
         .ToListAsync(); 
} 
      public static async Task< SikSorulanSorularAYRINTI ?> bul(veri.Varlik vari, params Expression<Func<SikSorulanSorularAYRINTI, bool>>[] kosullar)
    {
      var kosul = Vt.Birlestir(kosullar);
       return await vari.SikSorulanSorularAYRINTIs.FirstOrDefaultAsync(kosul);
   }



public static async Task<SikSorulanSorularAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
{
SikSorulanSorularAYRINTI? kayit = await kime.SikSorulanSorularAYRINTIs.FirstOrDefaultAsync(p => p.sikSorulanSorularkimlik == kimlik && p.varmi == true);
 return kayit;
}




public static SikSorulanSorularAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
{
SikSorulanSorularAYRINTI ? kayit = kime.SikSorulanSorularAYRINTIs.FirstOrDefault(p => p.sikSorulanSorularkimlik == kimlik); 
 if (kayit != null)   
    if (kayit.varmi != true) 
      return null; 
return kayit;
}
}
}

