using Bovime.Models;
using Bovime.veri;
using Microsoft.AspNetCore.Mvc;

namespace Bovime.Controllers
{
    public class IletisimController : SiteSayfasi
    {

        public async Task<IActionResult> Index()
        {
            IletisimModel model = new IletisimModel();
            await model.veriCek(this);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> IletisimAjax([FromBody] IletisimModel model)
        {
            if (model == null)
            {
                return Json(new { basarili = false, mesaj = "Veri alınamadı." });
            }

            // 1️⃣ Honeypot kontrolü
            if (!string.IsNullOrEmpty(model.website))
            {
                // bot yakalandı
                return Json(new { basarili = false, mesaj = "İşlem reddedildi." });
            }

            // 2️⃣ Zaman kontrolü (en az 3 saniye geçmiş olmalı)
            long nowTicks = DateTime.Now.Ticks;
            var farkSaniye = TimeSpan.FromTicks(nowTicks - model.formTime).TotalSeconds;

            if (farkSaniye < 3)
            {
                return Json(new { basarili = false, mesaj = "Çok hızlı gönderim tespit edildi." });
            }

            using (veri.Varlik _context = new Varlik())
            {
                if (string.IsNullOrEmpty(model.ad) || string.IsNullOrEmpty(model.ePosta) || string.IsNullOrEmpty(model.telefon))
                {
                    return Json(new { basarili = false, mesaj = "Ad, telefon ve E-Posta zorunludur." });
                }
                if (string.IsNullOrEmpty(model.ileti))
                {
                    return Json(new { basarili = false, mesaj = "İleti girilmesi zorunludur." });
                }
                model.telefon = Genel.telNoBicimlendir(model.telefon);
                if (model.telefon == ".")
                {
                    return Json(new { basarili = false, mesaj = "Telefon 5xxxxxxxx biçiminde olmalıdır" });
                }
                if (model.ileti.Length < 10)
                    return Json(new { basarili = false, mesaj = "İleti yeterince uzun değil" });

                try
                {
                    IletisimTalebi kayit = new IletisimTalebi
                    {
                        ad = model.ad,
                        ePosta = model.ePosta,
                        telefon = model.telefon,
                        konu = model.konu,
                        ileti = model.ileti,
                        tarih = DateTime.Now,
                        e_gorulduMu = false,
                        varmi = true,
                        tarihDatetimeVarmi = true,
                        ipAdresi = HttpContext.Connection.RemoteIpAddress?.ToString()
                    };

                    _context.IletisimTalebis.Add(kayit);
                    await _context.SaveChangesAsync();

                    return Json(new { basarili = true, mesaj = "İletiniz başarıyla gönderildi." });
                }
                catch (Exception ex)
                {
                    return Json(new { basarili = false, mesaj = "Kayıt sırasında hata oluştu." });
                }
            }
        }

    }
}
