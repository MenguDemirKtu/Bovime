using Bovime.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bovime.Controllers
{
    public class FirmalarimizController : SiteSayfasi
    {
        public async Task<IActionResult> Index()
        {
            FirmalarimizModel model = new FirmalarimizModel();
            await model.veriCek();
            return View(model);
        }
    }
}
