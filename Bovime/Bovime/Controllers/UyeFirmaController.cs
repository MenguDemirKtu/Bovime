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
            UyeFirmaModel model = new UyeFirmaModel();
            await model.veriCek(url);
            return View(model);
        }
    }
}
