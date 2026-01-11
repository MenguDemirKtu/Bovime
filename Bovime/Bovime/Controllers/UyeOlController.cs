using Microsoft.AspNetCore.Mvc;

namespace Bovime.Controllers
{
    public class UyeOlController : SiteSayfasi
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
