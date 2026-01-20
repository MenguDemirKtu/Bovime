using Bovime.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bovime.Controllers
{
    [Route("Bovimi")]
    public class BovimiController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Goster/{url}")]
        public async Task<IActionResult> Goster(string url)
        {
            BovimiModel sa = new BovimiModel();
            await sa.cek(url);
            return View(sa);
        }
    }
}
