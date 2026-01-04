using Bovime.Controllers;
namespace Bovime.Models
{
    public class IletisimModel : SiteModeli
    {
        public string sayfaTitle { get; set; }
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
