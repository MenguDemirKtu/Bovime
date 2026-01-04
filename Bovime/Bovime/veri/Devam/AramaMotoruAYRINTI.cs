using Microsoft.AspNetCore.Mvc.Rendering; 
using Microsoft.EntityFrameworkCore; 
using System; 
using System.Threading; 
using Microsoft.EntityFrameworkCore; 
using System.Threading; 
using System.Linq.Expressions;
using System.Collections.Generic; 
using System.Threading.Tasks; 
using System.ComponentModel.DataAnnotations.Schema;
using Bovime.veriTabani;

namespace Bovime.veri
  { 
 public partial class AramaMotoruAYRINTI : Bilesen 
   { 

public AramaMotoruAYRINTI()
{
   _varSayilan();
}


public   void bicimlendir(veri.Varlik vari) 
{

}

public   void _icDenetim(int dilKimlik, veri.Varlik vari) 
  {   
 } 


    public override string _tanimi()
  {   
 return bossaDoldur( sayfaAdresi) ;
 } 



  public async static Task<AramaMotoruAYRINTI?> olusturKos(Varlik vari, object deger)
  {
    Int64 kimlik = Convert.ToInt64(deger);
   if (kimlik <= 0)
   {
     AramaMotoruAYRINTI sonuc = new AramaMotoruAYRINTI(); 
     sonuc._varSayilan();
      return sonuc;
   }
  else
  {
      return await vari.AramaMotoruAYRINTIs.FirstOrDefaultAsync(p => p.aramaMotorukimlik == kimlik   && p.varmi == true  );
  } 
  } 


     public override void _kontrolEt(int dilKimlik, veri.Varlik vari) 
  { 
_icDenetim(dilKimlik, vari);
 }


       public override void _varSayilan() 
  { 
this.varmi = true;
 }

        public static async Task<List<AramaMotoruAYRINTI>> ara(params Expression<Func<AramaMotoruAYRINTI, bool>>[] kosullar)
       {
   return await veriTabani.AramaMotoruAYRINTICizelgesi.ara(kosullar);
  }
        public static async Task<List<AramaMotoruAYRINTI>> ara(veri.Varlik vari, params Expression<Func<AramaMotoruAYRINTI, bool>>[] kosullar)
       {
   return await veriTabani.AramaMotoruAYRINTICizelgesi.ara(vari,kosullar);
  }


    #region ozluk


public override string _cizelgeAdi()
 {
return "AramaMotoruAYRINTI";   
 }


   public override string _turkceAdi() 
  {
    return "AramaMotoru"; 
  } 
public override string _birincilAnahtarAdi()
 {
  return "aramaMotorukimlik"; 
}


    public override long _birincilAnahtar()
 {
     return this.aramaMotorukimlik;
}


    #endregion


  }
  }

