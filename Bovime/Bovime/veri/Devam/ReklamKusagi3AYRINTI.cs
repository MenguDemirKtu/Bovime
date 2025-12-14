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
 public partial class ReklamKusagi3AYRINTI : Bilesen 
   { 

public ReklamKusagi3AYRINTI()
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
 return bossaDoldur( baslik) ;
 } 



  public async static Task<ReklamKusagi3AYRINTI?> olusturKos(Varlik vari, object deger)
  {
    Int64 kimlik = Convert.ToInt64(deger);
   if (kimlik <= 0)
   {
     ReklamKusagi3AYRINTI sonuc = new ReklamKusagi3AYRINTI(); 
     sonuc._varSayilan();
      return sonuc;
   }
  else
  {
      return await vari.ReklamKusagi3AYRINTIs.FirstOrDefaultAsync(p => p.reklamKusagi3kimlik == kimlik   && p.varmi == true  );
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

        public static async Task<List<ReklamKusagi3AYRINTI>> ara(params Expression<Func<ReklamKusagi3AYRINTI, bool>>[] kosullar)
       {
   return await veriTabani.ReklamKusagi3AYRINTICizelgesi.ara(kosullar);
  }
        public static async Task<List<ReklamKusagi3AYRINTI>> ara(veri.Varlik vari, params Expression<Func<ReklamKusagi3AYRINTI, bool>>[] kosullar)
       {
   return await veriTabani.ReklamKusagi3AYRINTICizelgesi.ara(vari,kosullar);
  }


    #region ozluk


public override string _cizelgeAdi()
 {
return "ReklamKusagi3AYRINTI";   
 }


   public override string _turkceAdi() 
  {
    return "ReklamKusagi3"; 
  } 
public override string _birincilAnahtarAdi()
 {
  return "reklamKusagi3kimlik"; 
}


    public override long _birincilAnahtar()
 {
     return this.reklamKusagi3kimlik;
}


    #endregion


  }
  }

