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
 public partial class IletisimTalebiAYRINTI : Bilesen 
   { 

public IletisimTalebiAYRINTI()
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
 return bossaDoldur( ad) ;
 } 



  public async static Task<IletisimTalebiAYRINTI?> olusturKos(Varlik vari, object deger)
  {
    Int64 kimlik = Convert.ToInt64(deger);
   if (kimlik <= 0)
   {
     IletisimTalebiAYRINTI sonuc = new IletisimTalebiAYRINTI(); 
     sonuc._varSayilan();
      return sonuc;
   }
  else
  {
      return await vari.IletisimTalebiAYRINTIs.FirstOrDefaultAsync(p => p.iletisimTalebikimlik == kimlik   && p.varmi == true  );
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

        public static async Task<List<IletisimTalebiAYRINTI>> ara(params Expression<Func<IletisimTalebiAYRINTI, bool>>[] kosullar)
       {
   return await veriTabani.IletisimTalebiAYRINTICizelgesi.ara(kosullar);
  }
        public static async Task<List<IletisimTalebiAYRINTI>> ara(veri.Varlik vari, params Expression<Func<IletisimTalebiAYRINTI, bool>>[] kosullar)
       {
   return await veriTabani.IletisimTalebiAYRINTICizelgesi.ara(vari,kosullar);
  }
 public static async Task<IletisimTalebiAYRINTI ?> bul(veri.Varlik vari, params Expression<Func<IletisimTalebiAYRINTI, bool>>[] kosullar)
  {
     return await veriTabani.IletisimTalebiAYRINTICizelgesi.bul(vari, kosullar);
 }


    #region ozluk


public override string _cizelgeAdi()
 {
return "IletisimTalebiAYRINTI";   
 }


   public override string _turkceAdi() 
  {
    return "IletisimTalebi"; 
  } 
public override string _birincilAnahtarAdi()
 {
  return "iletisimTalebikimlik"; 
}


    public override long _birincilAnahtar()
 {
     return this.iletisimTalebikimlik;
}


    #endregion


  }
  }

