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
 public partial class IletisimTalebi : Bilesen 
   { 

public IletisimTalebi()
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



  public async static Task<IletisimTalebi?> olusturKos(Varlik vari, object deger)
  {
    Int64 kimlik = Convert.ToInt64(deger);
   if (kimlik <= 0)
   {
     IletisimTalebi sonuc = new IletisimTalebi(); 
     sonuc._varSayilan();
      return sonuc;
   }
  else
  {
      return await vari.IletisimTalebis.FirstOrDefaultAsync(p => p.iletisimTalebikimlik == kimlik   && p.varmi == true  );
  } 
  } 


public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
 {
if(varmi == null)
varmi = true;
 bicimlendir(vari);
  await veriTabani.IletisimTalebiCizelgesi.kaydetKos(this, vari, yedeklensinmi);
 }
 public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
 {
    varmi = false;
    await veriTabani.IletisimTalebiCizelgesi.silKos(this, vari, yedeklensinmi);
 } 


     public override void _kontrolEt(int dilKimlik, veri.Varlik vari) 
  { 
_icDenetim(dilKimlik, vari);
 }


       public override void _varSayilan() 
  { 
this.varmi = true;
this.e_gorulduMu  =  true ; 
this.tarihDatetimeVarmi  =  true ; 
this.varmi  =  true ; 
 }

        public static async Task<List<IletisimTalebi>> ara(params Expression<Func<IletisimTalebi, bool>>[] kosullar)
       {
   return await veriTabani.IletisimTalebiCizelgesi.ara(kosullar);
  }
        public static async Task<List<IletisimTalebi>> ara(veri.Varlik vari, params Expression<Func<IletisimTalebi, bool>>[] kosullar)
       {
   return await veriTabani.IletisimTalebiCizelgesi.ara(vari,kosullar);
  }
 public static async Task<IletisimTalebi ?> bul(veri.Varlik vari, params Expression<Func<IletisimTalebi, bool>>[] kosullar)
  {
     return await veriTabani.IletisimTalebiCizelgesi.bul(vari, kosullar);
 }


    #region ozluk


public override string _cizelgeAdi()
 {
return "IletisimTalebi";   
 }


   public override string _turkceAdi() 
  {
    return "İletişim Talebi"; 
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

