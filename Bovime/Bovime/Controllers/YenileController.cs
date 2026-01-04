using Microsoft.AspNetCore.Mvc;

namespace Bovime.Controllers
{
    public class YenileController : Sayfa
    {
        public async Task<IActionResult> Index()
        {
            Models.YenileModel model = new Models.YenileModel();
            Genel.yenilensinmi = true;
            await model.yenileKos();
            return RedirectToAction("Index", "AnaSayfa");
        }
    }
}
