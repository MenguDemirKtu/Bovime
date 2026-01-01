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
 public partial class FirmaGrubu : Bilesen 
   { 

public FirmaGrubu()
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
 return bossaDoldur( firmaGrupAdi) ;
 } 



  public async static Task<FirmaGrubu?> olusturKos(Varlik vari, object deger)
  {
    Int64 kimlik = Convert.ToInt64(deger);
   if (kimlik <= 0)
   {
     FirmaGrubu sonuc = new FirmaGrubu(); 
     sonuc._varSayilan();
      return sonuc;
   }
  else
  {
      return await vari.FirmaGrubus.FirstOrDefaultAsync(p => p.firmaGrubukimlik == kimlik   && p.varmi == true  );
  } 
  } 


public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
 {
if(varmi == null)
varmi = true;
 bicimlendir(vari);
  await veriTabani.FirmaGrubuCizelgesi.kaydetKos(this, vari, yedeklensinmi);
 }
 public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
 {
    varmi = false;
    await veriTabani.FirmaGrubuCizelgesi.silKos(this, vari, yedeklensinmi);
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

        public static async Task<List<FirmaGrubu>> ara(params Expression<Func<FirmaGrubu, bool>>[] kosullar)
       {
   return await veriTabani.FirmaGrubuCizelgesi.ara(kosullar);
  }
        public static async Task<List<FirmaGrubu>> ara(veri.Varlik vari, params Expression<Func<FirmaGrubu, bool>>[] kosullar)
       {
   return await veriTabani.FirmaGrubuCizelgesi.ara(vari,kosullar);
  }


    #region ozluk


public override string _cizelgeAdi()
 {
return "FirmaGrubu";   
 }


   public override string _turkceAdi() 
  {
    return "Firma Grubu"; 
  } 
public override string _birincilAnahtarAdi()
 {
  return "firmaGrubukimlik"; 
}


    public override long _birincilAnahtar()
 {
     return this.firmaGrubukimlik;
}


    #endregion


  }
  }

