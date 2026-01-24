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
 public class IletisimTalebiController : Sayfa
    {
public async Task<ActionResult> Cek( Models.IletisimTalebiModel modeli)
                            {
                                var nedir = await modeli.ayrintiliAraKos(this);
                                return basariBildirimi("/IletisimTalebi?id=" + nedir.kodu);
                            }
    public async Task<ActionResult> Index(string id)
        {
try{
            string tanitim = "...";
   tanitim = await Genel.dokumKisaAciklamaKos(this, "IletisimTalebi"); 
 gorunumAyari("", "", "Ana Sayfa", "/", "/IletisimTalebi/", tanitim); 
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos())
            {
   Models.IletisimTalebiModel modeli = new Models.IletisimTalebiModel();
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
   tanitim = await Genel.dokumKisaAciklamaKos(this, "IletisimTalebi"); 
 gorunumAyari("İletişim Talebi Kartı", "İletişim Talebi Kartı", "Ana Sayfa", "/", "/IletisimTalebi/", tanitim); 
            enumref_YetkiTuru yetkiTuru = yetkiTuruBelirle(id);
if (await yetkiVarmiKos("IletisimTalebi", yetkiTuru))
            {
  Models.IletisimTalebiModel modeli = new Models.IletisimTalebiModel(); 
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
Models.IletisimTalebiModel modeli = new Models.IletisimTalebiModel();
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
   public async Task<ActionResult> Kaydet(Models.IletisimTalebiModel gelen)
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
