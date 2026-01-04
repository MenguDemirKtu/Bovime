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
 public partial class FirmaKampanyasi : Bilesen 
   { 

public FirmaKampanyasi()
{
   _varSayilan();
}


public   void bicimlendir(veri.Varlik vari) 
{

}

public   void _icDenetim(int dilKimlik, veri.Varlik vari) 
  {   
uyariVerInt32(i_firmaKimlik,"Firma" , dilKimlik ) ; 
 } 


    public override string _tanimi()
  {   
 return bossaDoldur( i_firmaKimlik) ;
 } 



  public async static Task<FirmaKampanyasi?> olusturKos(Varlik vari, object deger)
  {
    Int64 kimlik = Convert.ToInt64(deger);
   if (kimlik <= 0)
   {
     FirmaKampanyasi sonuc = new FirmaKampanyasi(); 
     sonuc._varSayilan();
      return sonuc;
   }
  else
  {
      return await vari.FirmaKampanyasis.FirstOrDefaultAsync(p => p.firmaKampanyasikimlik == kimlik   && p.varmi == true  );
  } 
  } 


public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
 {
if(varmi == null)
varmi = true;
 bicimlendir(vari);
  await veriTabani.FirmaKampanyasiCizelgesi.kaydetKos(this, vari, yedeklensinmi);
 }
 public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
 {
    varmi = false;
    await veriTabani.FirmaKampanyasiCizelgesi.silKos(this, vari, yedeklensinmi);
 } 


     public override void _kontrolEt(int dilKimlik, veri.Varlik vari) 
  { 
_icDenetim(dilKimlik, vari);
 }


       public override void _varSayilan() 
  { 
this.varmi = true;
this.e_onaylandiMi  =  false ; 
this.e_yayindaMi  =  true ; 
 }

        public static async Task<List<FirmaKampanyasi>> ara(params Expression<Func<FirmaKampanyasi, bool>>[] kosullar)
       {
   return await veriTabani.FirmaKampanyasiCizelgesi.ara(kosullar);
  }
        public static async Task<List<FirmaKampanyasi>> ara(veri.Varlik vari, params Expression<Func<FirmaKampanyasi, bool>>[] kosullar)
       {
   return await veriTabani.FirmaKampanyasiCizelgesi.ara(vari,kosullar);
  }
 public static async Task<FirmaKampanyasi ?> bul(veri.Varlik vari, params Expression<Func<FirmaKampanyasi, bool>>[] kosullar)
  {
     return await veriTabani.FirmaKampanyasiCizelgesi.bul(vari, kosullar);
 }


    #region ozluk


public override string _cizelgeAdi()
 {
return "FirmaKampanyasi";   
 }


   public override string _turkceAdi() 
  {
    return "Firma Kampanyası"; 
  } 
public override string _birincilAnahtarAdi()
 {
  return "firmaKampanyasikimlik"; 
}


    public override long _birincilAnahtar()
 {
     return this.firmaKampanyasikimlik;
}


    #endregion


  }
  }

