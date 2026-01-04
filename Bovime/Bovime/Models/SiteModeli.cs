using Bovime.Controllers;
using Bovime.veri;
using Microsoft.EntityFrameworkCore;

namespace Bovime.Models
{
    public class SiteModeli
    {
        public async Task<string> aramaMotoruIslemleri(veri.Varlik vari, SiteSayfasi sayfa)
        {
            string adres = sayfa.sayfaninAdresi();

            AramaMotoru? karsilik = await vari.AramaMotorus.FirstOrDefaultAsync(p => p.sayfaAdresi == adres && p.varmi == true);

            if (karsilik == null)
            {
                AramaMotoru yeni = new AramaMotoru();
                yeni.sayfaAdresi = adres;
                yeni.title = "Bovime";
                yeni.varmi = true;
                yeni.goruntulenmeSayisi = 1;
                await yeni.kaydetKos(vari, false);
                karsilik = yeni;
            }
            else
            {
                if (karsilik.goruntulenmeSayisi == null)
                {
                    karsilik.goruntulenmeSayisi = 0;
                }
                karsilik.goruntulenmeSayisi++;
                await karsilik.kaydetKos(vari, false);
            }

            AramaMotoruZiyaret ziyaret = new AramaMotoruZiyaret();
            ziyaret.i_aramaMotoruKimlik = karsilik.aramaMotorukimlik;
            ziyaret.tarih = DateTime.Now;
            ziyaret.ipAdresi = sayfa.ipAdresi();
            await ziyaret.kaydetKos(vari);

            return karsilik.title ?? "Title";
        }
    }
}
