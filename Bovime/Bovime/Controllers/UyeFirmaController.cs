using Bovime.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bovime.Controllers
{
    [Route("UyeFirma")]
    public class UyeFirmaController : SiteSayfasi
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }



        [HttpGet("Goster/{url}")]
        public async Task<IActionResult> Goster(string url)
        {
            try
            {
                UyeFirmaModel model = new UyeFirmaModel();
                if (oturumAcildimi() == true)
                    await model.veriCek(url, oturumAcan());
                else
                    await model.veriCek(url, null);
                return View(model);
            }
            catch
            {
                return RedirectToAction("index", "SiteAnaSayfa");
            }
        }
    }
}
