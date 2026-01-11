using Bovime.veri;
using Microsoft.EntityFrameworkCore;

namespace Bovime.Models
{
    public class SiteSektoruModel : SiteModeli
    {
        public List<FirmaAYRINTI> firmalari { get; set; }
        public SektorAYRINTI sektoru { get; set; }

        public SiteSektoruModel()
        {
            sektoru = new SektorAYRINTI();
            firmalari = new List<FirmaAYRINTI>();
        }

        public async Task veriCek(string url)
        {
            using (veri.Varlik vari = new Varlik())
            {
                sektoru = await SektorAYRINTI.bul(vari, p => p.sektorUrl == url) ?? throw new Exception("Sektör bulunamadı");
                var baglantilar = await FirmaSektoruAYRINTI.ara(vari, p => p.i_sektorKimlik == sektoru.sektorkimlik);
                List<int> firmalar = baglantilar.Select(p => p.i_firmaKimlik).ToList();
                firmalari = await vari.FirmaAYRINTIs.Where(o => firmalar.Contains(o.firmakimlik)).ToListAsync();

                if (firmalari.Count == 1)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        firmalari.Add(firmalari[0]);
                    }
                }
            }

        }
    }
}
