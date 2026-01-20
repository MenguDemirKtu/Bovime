using Bovime.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Bovime.Controllers
{
    public class UyeGirisiController : SiteSayfasi
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GirisAjax([FromBody] GirisViewModel model)
        {
            if (string.IsNullOrEmpty(model.kullaniciAdi) || string.IsNullOrEmpty(model.sifre))
            {
                return Json(new { basarili = false, mesaj = "Kullanıcı adı ve şifre zorunludur." });
            }

            using (var _db = new veri.Varlik())
            {
                var uye = await _db.Uyes.FirstOrDefaultAsync(x =>
                    x.kullaniciAdi == model.kullaniciAdi &&
                    x.sifre == model.sifre &&
                    x.varmi == true
                );


                if (uye == null)
                {
                    return Json(new { basarili = false, mesaj = "Kullanıcı adı veya şifre hatalı." });
                }
                string jsonser = JsonConvert.SerializeObject(uye);
                HttpContext.Session.SetString("mevcutUye", jsonser);
            }
            return Json(new { basarili = true, yonlendir = "/" });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Giris(GirisViewModel model)
        {
            if (string.IsNullOrEmpty(model.kullaniciAdi) || string.IsNullOrEmpty(model.sifre))
            {
                ViewBag.Hata = "Kullanıcı adı ve şifre zorunludur.";
                return View(model);
            }

            using (veri.Varlik _db = new veri.Varlik())
            {
                var uye = await _db.Uyes.FirstOrDefaultAsync(x =>
                    x.kullaniciAdi == model.kullaniciAdi &&
                    x.sifre == model.sifre &&
                    x.varmi == true
                );

                if (uye == null)
                {
                    ViewBag.Hata = "Kullanıcı adı veya şifre hatalı.";
                    return View(model);
                }

                // Giriş başarılı → yönlendirme
                // HttpContext.Session.SetInt64("uyeKimlik", uye.uyekimlik);

                return Redirect("/AnaSayfa");
            }
        }
    }

}
