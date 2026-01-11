using Bovime.veri;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Bovime.Controllers
{
    public class SiteSayfasi : Controller
    {
        public string ipAdresi()
        {
            return HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";
        }
        protected bool oturumAcildimi()
        {
            if (HttpContext.Session.GetString("mevcutUye") == null)
                return false;
            else
                return true;
        }
        protected UyeAYRINTI oturumAcan()
        {
            string kullanici = HttpContext.Session.GetString("mevcutUye") ?? "";
            UyeAYRINTI? sonuc = JsonConvert.DeserializeObject<UyeAYRINTI>(kullanici ?? "");
            if (sonuc == null)
                throw new Exception("Oturum açılmamış");
            else
                return sonuc;
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
