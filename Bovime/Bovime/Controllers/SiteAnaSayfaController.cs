using Bovime.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bovime.Controllers
{
    public class SiteAnaSayfaController : Controller
    {
        public async Task<IActionResult> Index()
        {
            SiteAnaSayfaModel model = new SiteAnaSayfaModel();
            await model.veriCek();
            return View(model);
        }
    }
}
