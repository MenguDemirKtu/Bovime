using Bovime.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bovime.Controllers
{
    public class SiteAnaSayfaController : SiteSayfasi
    {
        public async Task<IActionResult> Index()
        {
            SiteAnaSayfaModel model = new SiteAnaSayfaModel();
            await model.veriCek(this);
            return View(model);
        }

        public async Task<ActionResult> yeni()
        {
            await GenelIslemler.SMSIslemi.smsGonder("5055152086", "denemedir");
            SiteAnaSayfaModel model = new SiteAnaSayfaModel();
            await model.veriCek(this);
            return View(model);
        }
    }
}
