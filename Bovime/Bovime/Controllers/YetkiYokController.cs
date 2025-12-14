using Microsoft.AspNetCore.Mvc;

namespace Bovime.Controllers
{
    public class YetkiYokController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
