using Bovime.veri;

namespace Bovime.Models
{
    public class BovimiModel : SiteModeli
    {
        public BovimisayfasiAYRINTI? sayfaBilgisi { get; set; }
        public async Task cek(string url)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                sayfaBilgisi = await BovimisayfasiAYRINTI.bul(vari, p => p.url == url);

            }
        }
    }
}
