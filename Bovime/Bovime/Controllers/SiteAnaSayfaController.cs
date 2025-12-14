using Microsoft.AspNetCore.Mvc;

namespace Bovime.Controllers
{
    public class SiteAnaSayfaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
