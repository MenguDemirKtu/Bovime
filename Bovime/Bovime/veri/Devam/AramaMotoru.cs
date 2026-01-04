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
 public partial class AramaMotoru : Bilesen 
   { 

public AramaMotoru()
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



  public async static Task<AramaMotoru?> olusturKos(Varlik vari, object deger)
  {
    Int64 kimlik = Convert.ToInt64(deger);
   if (kimlik <= 0)
   {
     AramaMotoru sonuc = new AramaMotoru(); 
     sonuc._varSayilan();
      return sonuc;
   }
  else
  {
      return await vari.AramaMotorus.FirstOrDefaultAsync(p => p.aramaMotorukimlik == kimlik   && p.varmi == true  );
  } 
  } 


public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
 {
if(varmi == null)
varmi = true;
 bicimlendir(vari);
  await veriTabani.AramaMotoruCizelgesi.kaydetKos(this, vari, yedeklensinmi);
 }
 public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
 {
    varmi = false;
    await veriTabani.AramaMotoruCizelgesi.silKos(this, vari, yedeklensinmi);
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

        public static async Task<List<AramaMotoru>> ara(params Expression<Func<AramaMotoru, bool>>[] kosullar)
       {
   return await veriTabani.AramaMotoruCizelgesi.ara(kosullar);
  }
        public static async Task<List<AramaMotoru>> ara(veri.Varlik vari, params Expression<Func<AramaMotoru, bool>>[] kosullar)
       {
   return await veriTabani.AramaMotoruCizelgesi.ara(vari,kosullar);
  }


    #region ozluk


public override string _cizelgeAdi()
 {
return "AramaMotoru";   
 }


   public override string _turkceAdi() 
  {
    return "Arama Motoru"; 
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

