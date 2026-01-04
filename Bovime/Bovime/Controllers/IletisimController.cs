using Bovime.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bovime.Controllers
{
    public class IletisimController : SiteSayfasi
    {

        public async Task<IActionResult> Index()
        {
            IletisimModel model = new IletisimModel();
            await model.veriCek(this);
            return View();
        }
    }
}
