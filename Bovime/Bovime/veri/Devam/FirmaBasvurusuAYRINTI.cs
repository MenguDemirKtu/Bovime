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
 public partial class FirmaBasvurusuAYRINTI : Bilesen 
   { 

public FirmaBasvurusuAYRINTI()
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
 return bossaDoldur( firmaAdi) ;
 } 



  public async static Task<FirmaBasvurusuAYRINTI?> olusturKos(Varlik vari, object deger)
  {
    Int64 kimlik = Convert.ToInt64(deger);
   if (kimlik <= 0)
   {
     FirmaBasvurusuAYRINTI sonuc = new FirmaBasvurusuAYRINTI(); 
     sonuc._varSayilan();
      return sonuc;
   }
  else
  {
      return await vari.FirmaBasvurusuAYRINTIs.FirstOrDefaultAsync(p => p.firmaBasvurusukimlik == kimlik   && p.varmi == true  );
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

        public static async Task<List<FirmaBasvurusuAYRINTI>> ara(params Expression<Func<FirmaBasvurusuAYRINTI, bool>>[] kosullar)
       {
   return await veriTabani.FirmaBasvurusuAYRINTICizelgesi.ara(kosullar);
  }
        public static async Task<List<FirmaBasvurusuAYRINTI>> ara(veri.Varlik vari, params Expression<Func<FirmaBasvurusuAYRINTI, bool>>[] kosullar)
       {
   return await veriTabani.FirmaBasvurusuAYRINTICizelgesi.ara(vari,kosullar);
  }
 public static async Task<FirmaBasvurusuAYRINTI ?> bul(veri.Varlik vari, params Expression<Func<FirmaBasvurusuAYRINTI, bool>>[] kosullar)
  {
     return await veriTabani.FirmaBasvurusuAYRINTICizelgesi.bul(vari, kosullar);
 }


    #region ozluk


public override string _cizelgeAdi()
 {
return "FirmaBasvurusuAYRINTI";   
 }


   public override string _turkceAdi() 
  {
    return "FirmaBasvurusu"; 
  } 
public override string _birincilAnahtarAdi()
 {
  return "firmaBasvurusukimlik"; 
}


    public override long _birincilAnahtar()
 {
     return this.firmaBasvurusukimlik;
}


    #endregion


  }
  }

