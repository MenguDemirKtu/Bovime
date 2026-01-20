using Bovime.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bovime.Controllers
{
    [Route("Kampanya")]
    public class KampanyaController : SiteSayfasi
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("KampanyaGoster")]
        public async Task<IActionResult> KampanyaGoster(string kim)
        {
            string nedir = kim;
            UyeFirmaModel model = new UyeFirmaModel();
            if (oturumAcildimi())
            {
                await model.bilgiCek(oturumAcan(), kim);
            }
            else
            {
                await model.bilgiCek(null, kim);
            }
            return PartialView("_KampanyaGoster", model);
        }
    }
}
