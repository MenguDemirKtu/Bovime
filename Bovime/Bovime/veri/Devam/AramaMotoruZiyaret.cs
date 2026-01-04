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
 public partial class AramaMotoruZiyaret : Bilesen 
   { 

public AramaMotoruZiyaret()
{
   _varSayilan();
}


public   void bicimlendir(veri.Varlik vari) 
{

}

public   void _icDenetim(int dilKimlik, veri.Varlik vari) 
  {   
uyariVerInt32(i_aramaMotoruKimlik,"Arama Motoru" , dilKimlik ) ; 
 } 


    public override string _tanimi()
  {   
 return bossaDoldur( i_aramaMotoruKimlik) ;
 } 



  public async static Task<AramaMotoruZiyaret?> olusturKos(Varlik vari, object deger)
  {
    Int64 kimlik = Convert.ToInt64(deger);
   if (kimlik <= 0)
   {
     AramaMotoruZiyaret sonuc = new AramaMotoruZiyaret(); 
     sonuc._varSayilan();
      return sonuc;
   }
  else
  {
      return await vari.AramaMotoruZiyarets.FirstOrDefaultAsync(p => p.aramaMotoruZiyaretkimlik == kimlik   && p.varmi == true  );
  } 
  } 


public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
 {
if(varmi == null)
varmi = true;
 bicimlendir(vari);
  await veriTabani.AramaMotoruZiyaretCizelgesi.kaydetKos(this, vari, yedeklensinmi);
 }
 public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
 {
    varmi = false;
    await veriTabani.AramaMotoruZiyaretCizelgesi.silKos(this, vari, yedeklensinmi);
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

        public static async Task<List<AramaMotoruZiyaret>> ara(params Expression<Func<AramaMotoruZiyaret, bool>>[] kosullar)
       {
   return await veriTabani.AramaMotoruZiyaretCizelgesi.ara(kosullar);
  }
        public static async Task<List<AramaMotoruZiyaret>> ara(veri.Varlik vari, params Expression<Func<AramaMotoruZiyaret, bool>>[] kosullar)
       {
   return await veriTabani.AramaMotoruZiyaretCizelgesi.ara(vari,kosullar);
  }


    #region ozluk


public override string _cizelgeAdi()
 {
return "AramaMotoruZiyaret";   
 }


   public override string _turkceAdi() 
  {
    return "Arama Motoru Ziyaret"; 
  } 
public override string _birincilAnahtarAdi()
 {
  return "aramaMotoruZiyaretkimlik"; 
}


    public override long _birincilAnahtar()
 {
     return this.aramaMotoruZiyaretkimlik;
}


    #endregion


  }
  }

