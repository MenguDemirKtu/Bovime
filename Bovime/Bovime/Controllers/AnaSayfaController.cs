using Bovime.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bovime.Controllers
{
    public class AnaSayfaController : Sayfa
    {
        public async Task<IActionResult> Index(string id)
        {

            if (!oturumAcildimi())
                return OturumAcilmadi();
            ViewBag.mevcut = Genel.mevcutKullanici(this);
            ViewBag.dil = mevcutKullanici().dilKimlik;
            sayfaTuru = enum_sayfaTuru.anaSayfa;
            AnaSayfaModel modeli = new AnaSayfaModel();
            await modeli.veriCekKosut(mevcutKullanici());
            return View(modeli);
        }
    }
}
