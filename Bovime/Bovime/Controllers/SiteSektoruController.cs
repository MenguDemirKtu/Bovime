using Bovime.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bovime.Controllers
{
    public class SiteSektoruController : SiteSayfasi
    {
        [HttpGet("SiteSektoru/{url}")]
        public async Task<IActionResult> Index(string url)
        {
            try
            {
                SiteSektoruModel modeli = new SiteSektoruModel();
                await modeli.veriCek(url);
                return View(modeli);
            }
            catch
            {
                return RedirectToAction("index", "SiteAnaSayfa");
            }
        }
    }
}
