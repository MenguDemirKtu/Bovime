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
 public class ReklamKusagi2Controller : Sayfa
    {
public async Task<ActionResult> Cek( Models.ReklamKusagi2Model modeli)
                            {
                                var nedir = await modeli.ayrintiliAraKos(this);
                                return basariBildirimi("/ReklamKusagi2?id=" + nedir.kodu);
                            }
    public async Task<ActionResult> Index(string id)
        {
try{
            string tanitim = "...";
   tanitim = await Genel.dokumKisaAciklamaKos(this, "ReklamKusagi2"); 
 gorunumAyari("", "", "Ana Sayfa", "/", "/ReklamKusagi2/", tanitim); 
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos())
            {
   Models.ReklamKusagi2Model modeli = new Models.ReklamKusagi2Model();
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
   tanitim = await Genel.dokumKisaAciklamaKos(this, "ReklamKusagi2"); 
 gorunumAyari("Reklam Kuşağı 2 Kartı", "Reklam Kuşağı 2 Kartı", "Ana Sayfa", "/", "/ReklamKusagi2/", tanitim); 
            enumref_YetkiTuru yetkiTuru = yetkiTuruBelirle(id);
if (await yetkiVarmiKos("ReklamKusagi2", yetkiTuru))
            {
  Models.ReklamKusagi2Model modeli = new Models.ReklamKusagi2Model(); 
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
Models.ReklamKusagi2Model modeli = new Models.ReklamKusagi2Model();
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
   public async Task<ActionResult> Kaydet(Models.ReklamKusagi2Model gelen)
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
