using Bovime.Models;
using Bovime.veri;
using Microsoft.AspNetCore.Mvc;

namespace Bovime.Controllers
{
    public class FirmaTalebiController : SiteSayfasi
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> FirmaBasvuruAjax([FromBody] FirmaTalebiModel model)
        {
            if (model == null)
                return Json(new { basarili = false, mesaj = "Veri alınamadı." });

            // 1️⃣ Honeypot kontrolü
            if (!string.IsNullOrEmpty(model.website))
                return Json(new { basarili = false, mesaj = "İşlem reddedildi." });

            // 2️⃣ Zaman kontrolü (en az 3 saniye)
            long nowTicks = DateTime.Now.Ticks;
            var fark = TimeSpan.FromTicks(nowTicks - model.formTime).TotalSeconds;

            if (fark < 3)
                return Json(new { basarili = false, mesaj = "Çok hızlı gönderim tespit edildi." });

            // 3️⃣ Validasyon
            if (string.IsNullOrEmpty(model.firmaAdi) ||
                string.IsNullOrEmpty(model.tel) ||
                string.IsNullOrEmpty(model.ePosta))
            {
                return Json(new { basarili = false, mesaj = "Firma adı, telefon ve e-posta zorunludur." });
            }

            if (string.IsNullOrEmpty(model.metin) || model.metin.Length < 10)
                return Json(new { basarili = false, mesaj = "Açıklama yeterince uzun olmalıdır." });

            // Telefon biçim kontrolü (senin kullandığın fonksiyon)
            model.tel = Genel.telNoBicimlendir(model.tel);
            if (model.tel == ".")
                return Json(new { basarili = false, mesaj = "Telefon 5xxxxxxxx biçiminde olmalıdır." });

            try
            {
                using (Varlik _context = new Varlik())
                {
                    FirmaBasvurusu kayit = new FirmaBasvurusu
                    {
                        firmaAdi = model.firmaAdi,
                        tel = model.tel,
                        ePosta = model.ePosta,
                        metin = model.metin,
                        tarih = DateTime.Now,
                        e_gorulduMu = false,
                        varmi = true
                    };

                    _context.FirmaBasvurusus.Add(kayit);
                    await _context.SaveChangesAsync();
                }

                return Json(new { basarili = true, mesaj = "Başvurunuz başarıyla alınmıştır." });
            }
            catch
            {
                return Json(new { basarili = false, mesaj = "Kayıt sırasında hata oluştu." });
            }
        }
    }
}
