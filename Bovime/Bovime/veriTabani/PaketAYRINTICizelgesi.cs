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

public class PaketAYRINTIArama
{
 public  Int32  ?  paketKimlik {get;set;}
 public  string  ?  paketAdi {get;set;}
 public  Int64  ?  i_fotoKimlik {get;set;}
 public  string  ?  fotosu {get;set;}
 public  string  ?  tanitim {get;set;}
 public  bool  ?  varmi {get;set;}
 public PaketAYRINTIArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<PaketAYRINTI> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< PaketAYRINTI>(P => P.varmi == true);
 if (paketKimlik  != null)
 predicate = predicate.And(x => x.paketKimlik == paketKimlik ); 
 if (paketAdi  != null)
               predicate = predicate.And( x => x.paketAdi != null &&    x.paketAdi .Contains(paketAdi));
 if (i_fotoKimlik  != null)
 predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik ); 
 if (fotosu  != null)
               predicate = predicate.And( x => x.fotosu != null &&    x.fotosu .Contains(fotosu));
 if (tanitim  != null)
               predicate = predicate.And( x => x.tanitim != null &&    x.tanitim .Contains(tanitim));
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
return  predicate;
 
}
      public async Task<List<   PaketAYRINTI      >> cek(veri.Varlik vari)
   {
     List <PaketAYRINTI> sonuc = await vari.PaketAYRINTIs
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<PaketAYRINTI?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    PaketAYRINTI ? sonuc = await vari.PaketAYRINTIs
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class PaketAYRINTICizelgesi
{





/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<PaketAYRINTI>> ara(params Expression<Func<PaketAYRINTI, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<PaketAYRINTI>> ara(veri.Varlik vari, params Expression<Func<PaketAYRINTI, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.PaketAYRINTIs 
                  .Where(kosul).OrderByDescending(p => p.paketKimlik) 
         .ToListAsync(); 
} 
      public static async Task< PaketAYRINTI ?> bul(veri.Varlik vari, params Expression<Func<PaketAYRINTI, bool>>[] kosullar)
    {
      var kosul = Vt.Birlestir(kosullar);
       return await vari.PaketAYRINTIs.FirstOrDefaultAsync(kosul);
   }



public static async Task<PaketAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
{
PaketAYRINTI? kayit = await kime.PaketAYRINTIs.FirstOrDefaultAsync(p => p.paketKimlik == kimlik && p.varmi == true);
 return kayit;
}




public static PaketAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
{
PaketAYRINTI ? kayit = kime.PaketAYRINTIs.FirstOrDefault(p => p.paketKimlik == kimlik); 
 if (kayit != null)   
    if (kayit.varmi != true) 
      return null; 
return kayit;
}
}
}

