using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Bovime.Controllers
{
    public class SiteSayfasi : Controller
    {
        public string ipAdresi()
        {
            return HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";
        }

        public string sayfaninAdresi()
        {
            string adres = Request.GetEncodedUrl();
            string kaynak = HttpContext.Request.Host.Value ?? "xxxx";
            adres = adres.Replace("/favicon.ico", "");
            adres = adres.Replace("\\favicon.ico", "");
            adres = adres.Replace("//favicon.ico", "");
            adres = adres.Replace(kaynak, "");
            adres = adres.Replace("https:////", "").Trim();
            adres = adres.Replace("http:////", "").Trim();
            adres = adres.Replace("https://", "").Trim();
            adres = adres.Replace("http://", "").Trim();
            adres = adres.Replace("https:/", "").Trim();
            adres = adres.Replace("http:/", "").Trim();
            adres = adres.Replace(kaynak, "");
            return adres;
        }
    }
}
