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
 public partial class FirmaGrubuReklami : Bilesen 
   { 

public FirmaGrubuReklami()
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
 return bossaDoldur( firmaGrubuBaslik) ;
 } 



  public async static Task<FirmaGrubuReklami?> olusturKos(Varlik vari, object deger)
  {
    Int64 kimlik = Convert.ToInt64(deger);
   if (kimlik <= 0)
   {
     FirmaGrubuReklami sonuc = new FirmaGrubuReklami(); 
     sonuc._varSayilan();
      return sonuc;
   }
  else
  {
      return await vari.FirmaGrubuReklamis.FirstOrDefaultAsync(p => p.firmaGrubuReklamikimlik == kimlik   && p.varmi == true  );
  } 
  } 


public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
 {
if(varmi == null)
varmi = true;
 bicimlendir(vari);
  await veriTabani.FirmaGrubuReklamiCizelgesi.kaydetKos(this, vari, yedeklensinmi);
 }
 public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
 {
    varmi = false;
    await veriTabani.FirmaGrubuReklamiCizelgesi.silKos(this, vari, yedeklensinmi);
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

        public static async Task<List<FirmaGrubuReklami>> ara(params Expression<Func<FirmaGrubuReklami, bool>>[] kosullar)
       {
   return await veriTabani.FirmaGrubuReklamiCizelgesi.ara(kosullar);
  }
        public static async Task<List<FirmaGrubuReklami>> ara(veri.Varlik vari, params Expression<Func<FirmaGrubuReklami, bool>>[] kosullar)
       {
   return await veriTabani.FirmaGrubuReklamiCizelgesi.ara(vari,kosullar);
  }


    #region ozluk


public override string _cizelgeAdi()
 {
return "FirmaGrubuReklami";   
 }


   public override string _turkceAdi() 
  {
    return "Firma Grubu Reklamı"; 
  } 
public override string _birincilAnahtarAdi()
 {
  return "firmaGrubuReklamikimlik"; 
}


    public override long _birincilAnahtar()
 {
     return this.firmaGrubuReklamikimlik;
}


    #endregion


  }
  }

