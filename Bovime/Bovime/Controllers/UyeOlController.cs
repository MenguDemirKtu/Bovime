using Bovime.Models;
using Bovime.veri;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Bovime.Controllers
{
    public class UyeOlController : SiteSayfasi
    {
        public async Task<IActionResult> Index()
        {
            UyeOlModel model = new UyeOlModel();
            await model.hazirla();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UyeOlAjax([FromBody] Uye model)
        {
            try
            {
                model.telefon = Genel.telNoBicimlendir(model.telefon);

                if (string.IsNullOrEmpty(model.adi))
                    throw new Exception("Ad girilmelidir");
                if (string.IsNullOrEmpty(model.soyadi))
                    throw new Exception("Soyad girilmelidir");
                if (string.IsNullOrEmpty(model.kullaniciAdi))
                    throw new Exception("Kullanıcı adı girilmelidir");
                if (string.IsNullOrEmpty(model.sifre))
                    throw new Exception("Şifre  girilmelidir");

                if (model.telefon == ".")
                {
                    throw new Exception("Teleon numarası 5xxxxxxx formatında girilmelidir");
                }


                if (string.IsNullOrEmpty(model.kullaniciAdi) || string.IsNullOrEmpty(model.sifre))
                {
                    return Json(new { basarili = false, mesaj = "Kullanıcı adı ve şifre zorunludur." });
                }

                using (Varlik db = new Varlik())
                {
                    bool varmi = db.Uyes.Any(x => x.kullaniciAdi == model.kullaniciAdi);
                    if (varmi)
                    {
                        return Json(new { basarili = false, mesaj = "Bu kullanıcı adı zaten kullanılıyor." });
                    }

                    var karslik = await db.UyeAYRINTIs.FirstOrDefaultAsync(p => p.telefon == model.telefon);
                    if (karslik != null)
                        throw new Exception("Bu telefonu kullanan başka bir üyemiz var");

                    model.i_uyeDurumuKimlik = (int)enumref_UyeDurumu.Aktif_Uye;
                    model.uyelikTarihi = DateTime.Now;
                    model.varmi = true;

                    await db.Uyes.AddAsync(model);
                    await db.SaveChangesAsync();
                }

                return Json(new
                {
                    basarili = true,
                    mesaj = "Üyelik başarıyla oluşturuldu. Giriş yapabilirsiniz."
                });
            }
            catch (Exception exc)
            {

                return Json(new { basarili = false, mesaj = exc.Message });
            }
        }
    }
}
