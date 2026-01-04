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

public class AramaMotoruZiyaretAYRINTIArama
{
 public  Int64  ?  aramaMotoruZiyaretkimlik {get;set;}
 public  Int32  ?  i_aramaMotoruKimlik {get;set;}
 public  string  ?  sayfaAdresi {get;set;}
 public  DateTime  ?  tarih {get;set;}
 public  string  ?  ipAdresi {get;set;}
 public  bool  ?  varmi {get;set;}
 public AramaMotoruZiyaretAYRINTIArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<AramaMotoruZiyaretAYRINTI> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< AramaMotoruZiyaretAYRINTI>(P => P.varmi == true);
 if (aramaMotoruZiyaretkimlik  != null)
 predicate = predicate.And(x => x.aramaMotoruZiyaretkimlik == aramaMotoruZiyaretkimlik ); 
 if (i_aramaMotoruKimlik  != null)
 predicate = predicate.And(x => x.i_aramaMotoruKimlik == i_aramaMotoruKimlik ); 
 if (sayfaAdresi  != null)
               predicate = predicate.And( x => x.sayfaAdresi != null &&    x.sayfaAdresi .Contains(sayfaAdresi));
 if (tarih  != null)
 predicate = predicate.And(x => x.tarih == tarih ); 
 if (ipAdresi  != null)
               predicate = predicate.And( x => x.ipAdresi != null &&    x.ipAdresi .Contains(ipAdresi));
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
return  predicate;
 
}
      public async Task<List<   AramaMotoruZiyaretAYRINTI      >> cek(veri.Varlik vari)
   {
     List <AramaMotoruZiyaretAYRINTI> sonuc = await vari.AramaMotoruZiyaretAYRINTIs
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<AramaMotoruZiyaretAYRINTI?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    AramaMotoruZiyaretAYRINTI ? sonuc = await vari.AramaMotoruZiyaretAYRINTIs
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class AramaMotoruZiyaretAYRINTICizelgesi
{





/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<AramaMotoruZiyaretAYRINTI>> ara(params Expression<Func<AramaMotoruZiyaretAYRINTI, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<AramaMotoruZiyaretAYRINTI>> ara(veri.Varlik vari, params Expression<Func<AramaMotoruZiyaretAYRINTI, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.AramaMotoruZiyaretAYRINTIs 
                  .Where(kosul).OrderByDescending(p => p.aramaMotoruZiyaretkimlik) 
         .ToListAsync(); 
} 



public static async Task<AramaMotoruZiyaretAYRINTI?> tekliCekKos(Int64 kimlik, Varlik kime)
{
AramaMotoruZiyaretAYRINTI? kayit = await kime.AramaMotoruZiyaretAYRINTIs.FirstOrDefaultAsync(p => p.aramaMotoruZiyaretkimlik == kimlik && p.varmi == true);
 return kayit;
}




public static AramaMotoruZiyaretAYRINTI? tekliCek(Int64 kimlik, Varlik kime)
{
AramaMotoruZiyaretAYRINTI ? kayit = kime.AramaMotoruZiyaretAYRINTIs.FirstOrDefault(p => p.aramaMotoruZiyaretkimlik == kimlik); 
 if (kayit != null)   
    if (kayit.varmi != true) 
      return null; 
return kayit;
}
}
}

