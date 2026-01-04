using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
      using System;
    using System.Threading; 
   using Microsoft.EntityFrameworkCore; 
 using System.Threading;
 using System.Collections.Generic; 
  using System.Threading.Tasks;
using System.Linq;
using Bovime.veri;
namespace Bovime.Controllers
{
 public class FirmaKampanyasiController : Sayfa
    {
public async Task<ActionResult> Cek( Models.FirmaKampanyasiModel modeli)
                            {
                                var nedir = await modeli.ayrintiliAraKos(this);
                                return basariBildirimi("/FirmaKampanyasi?id=" + nedir.kodu);
                            }
    public async Task<ActionResult> Index(string id)
        {
try{
            string tanitim = "...";
   tanitim = await Genel.dokumKisaAciklamaKos(this, "FirmaKampanyasi"); 
 gorunumAyari("", "", "Ana Sayfa", "/", "/FirmaKampanyasi/", tanitim); 
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos())
            {
   Models.FirmaKampanyasiModel modeli = new Models.FirmaKampanyasiModel();
                if (string.IsNullOrEmpty(id))
  	await	   modeli.veriCekKos(mevcutKullanici());
	 else
    	await	 modeli.kosulaGoreCek(mevcutKullanici(), id);
                return View(modeli);
            }
            else
            {
                return YetkiYok();
            }
}
catch(Exception ex)
{
return await HataSayfasiKosut(ex);
}
        }
public async Task<ActionResult> Kart(long id)
        {
try{
            if (!oturumAcildimi())
                return OturumAcilmadi();
            string tanitim = "....";
   tanitim = await Genel.dokumKisaAciklamaKos(this, "FirmaKampanyasi"); 
 gorunumAyari("Firma Kampanyası Kartı", "Firma Kampanyası Kartı", "Ana Sayfa", "/", "/FirmaKampanyasi/", tanitim); 
            enumref_YetkiTuru yetkiTuru = yetkiTuruBelirle(id);
if (await yetkiVarmiKos("FirmaKampanyasi", yetkiTuru))
            {
  Models.FirmaKampanyasiModel modeli = new Models.FirmaKampanyasiModel(); 
                  await   modeli.veriCekKos(mevcutKullanici(), id);
                return View(modeli);
            }
            else
            {
                return YetkiYok();
            }
 }
 catch (Exception ex )
 {
     return await HataSayfasiKosut(ex);
 }
        }
        [HttpPost]
public async Task<ActionResult> Sil(string id)
        {
                 try
            {
                if (!oturumAcildimi())
         return OturumAcilmadi();
                if (id == null)
                    uyariVer(Ikazlar.hicKayitSecilmemis(dilKimlik));
                if (await yetkiVarmiKos("Ogrenci", enumref_YetkiTuru.Silme))
                {
Models.FirmaKampanyasiModel modeli = new Models.FirmaKampanyasiModel();
              await       modeli.silKos(this, id??"", mevcutKullanici());
                 await       modeli.veriCekKos(mevcutKullanici()); 
                    return basariBildirimi(Ikazlar.basariylaSilindi(dilKimlik));
                }
                else
                {
                    return yetkiYokBildirimi();
                }
            }
            catch (Exception istisna)
            {
                return hataBildirimi(istisna);
            }
        }
        [HttpPost]
   public async Task<ActionResult> Kaydet(Models.FirmaKampanyasiModel gelen)
        {
            try
            { 
               if (!oturumAcildimi())
                    return OturumAcilmadi(); 
               await gelen.yetkiKontrolu(this);
               await gelen.kaydetKos(this);
               return basariBildirimi(gelen.kartVerisi, dilKimlik);
            }
            catch (Exception istisna)
            {
                return hataBildirimi(istisna);
            }
        }
    }
}
