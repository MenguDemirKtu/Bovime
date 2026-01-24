using Microsoft.AspNetCore.Mvc;

namespace Bovime.Controllers
{
    public class BizKimizController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
