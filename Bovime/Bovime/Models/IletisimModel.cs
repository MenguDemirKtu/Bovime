using Bovime.Controllers;
namespace Bovime.Models
{
    public class IletisimModel : SiteModeli
    {

        public string? ad { get; set; }
        public string? ePosta { get; set; }
        public string? telefon { get; set; }
        public string? konu { get; set; }
        public string? ileti { get; set; }

        public string sayfaTitle { get; set; }

        public string? website { get; set; }   // honeypot
        public long formTime { get; set; }     // ticks

        public IletisimModel()
        {
            sayfaTitle = "...";
        }
        public async Task veriCek(SiteSayfasi sayfa)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                sayfaTitle = await aramaMotoruIslemleri(vari, sayfa);
            }
        }
    }
}
