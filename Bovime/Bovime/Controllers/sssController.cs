using Bovime.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bovime.Controllers
{
    public class sssController : Controller
    {
        public async Task<IActionResult> Index()
        {
            sssModel modeli = new sssModel();
            await modeli.veriCek();
            return View(modeli);
        }
    }
}
