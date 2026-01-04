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
    }
}
