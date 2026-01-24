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
 public partial class SikSorulanSorular : Bilesen 
   { 

public SikSorulanSorular()
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
 return bossaDoldur( soru) ;
 } 



  public async static Task<SikSorulanSorular?> olusturKos(Varlik vari, object deger)
  {
    Int64 kimlik = Convert.ToInt64(deger);
   if (kimlik <= 0)
   {
     SikSorulanSorular sonuc = new SikSorulanSorular(); 
     sonuc._varSayilan();
      return sonuc;
   }
  else
  {
      return await vari.SikSorulanSorulars.FirstOrDefaultAsync(p => p.sikSorulanSorularkimlik == kimlik   && p.varmi == true  );
  } 
  } 


public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
 {
if(varmi == null)
varmi = true;
 bicimlendir(vari);
  await veriTabani.SikSorulanSorularCizelgesi.kaydetKos(this, vari, yedeklensinmi);
 }
 public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
 {
    varmi = false;
    await veriTabani.SikSorulanSorularCizelgesi.silKos(this, vari, yedeklensinmi);
 } 


     public override void _kontrolEt(int dilKimlik, veri.Varlik vari) 
  { 
_icDenetim(dilKimlik, vari);
 }


       public override void _varSayilan() 
  { 
this.varmi = true;
this.varmi  =  true ; 
 }

        public static async Task<List<SikSorulanSorular>> ara(params Expression<Func<SikSorulanSorular, bool>>[] kosullar)
       {
   return await veriTabani.SikSorulanSorularCizelgesi.ara(kosullar);
  }
        public static async Task<List<SikSorulanSorular>> ara(veri.Varlik vari, params Expression<Func<SikSorulanSorular, bool>>[] kosullar)
       {
   return await veriTabani.SikSorulanSorularCizelgesi.ara(vari,kosullar);
  }
 public static async Task<SikSorulanSorular ?> bul(veri.Varlik vari, params Expression<Func<SikSorulanSorular, bool>>[] kosullar)
  {
     return await veriTabani.SikSorulanSorularCizelgesi.bul(vari, kosullar);
 }


    #region ozluk


public override string _cizelgeAdi()
 {
return "SikSorulanSorular";   
 }


   public override string _turkceAdi() 
  {
    return "Sık Sorulan Sorular"; 
  } 
public override string _birincilAnahtarAdi()
 {
  return "sikSorulanSorularkimlik"; 
}


    public override long _birincilAnahtar()
 {
     return this.sikSorulanSorularkimlik;
}


    #endregion


  }
  }

