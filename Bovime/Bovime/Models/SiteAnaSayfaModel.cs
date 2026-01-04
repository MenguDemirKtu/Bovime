using Bovime.Controllers;
using Bovime.veri;
using Microsoft.EntityFrameworkCore;

namespace Bovime.Models
{
    public class SiteAnaSayfaModel : SiteModeli
    {
        public List<MansetAYRINTI> mansetler { get; set; }
        public List<ReklamKusagi1AYRINTI> reklamKusagi1 { get; set; }

        public List<ReklamKusagi2AYRINTI> reklamKusagi0 { get; set; }
        public List<ReklamKusagi3AYRINTI> reklamKusagi2 { get; set; }

        public List<SektorAYRINTI> anaSektorler { get; set; }

        public List<FirmaKampanyasiAYRINTI> kampanyalar { get; set; }
        public List<FirmaGrubuAYRINTI> firmaGruplari { get; set; }
        public SiteAnaSayfaModel()
        {
            title = "...";
            mansetler = new List<MansetAYRINTI>();
            reklamKusagi1 = new List<ReklamKusagi1AYRINTI>();
            anaSektorler = new List<SektorAYRINTI>();
            reklamKusagi0 = new List<ReklamKusagi2AYRINTI>();
            firmaGruplari = new List<FirmaGrubuAYRINTI>();
            reklamKusagi2 = new List<ReklamKusagi3AYRINTI>();
            kampanyalar = new List<FirmaKampanyasiAYRINTI>();
        }


        public async Task veriCek(SiteSayfasi sa)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var liste = await Sektor.ara(vari);

                foreach (var siradaki in liste)
                    await siradaki.kaydetKos(vari, false);

                title = await aramaMotoruIslemleri(vari, sa);
                mansetler = await vari.MansetAYRINTIs.Where(p => p.e_yayindami == true).OrderBy(p => p.sirasi).ToListAsync();
                reklamKusagi1 = await vari.ReklamKusagi1AYRINTIs.ToListAsync();
                anaSektorler = await SektorAYRINTI.ara(vari, p => p.e_anaSayfaGorunsunmu == true);
                reklamKusagi0 = await vari.ReklamKusagi2AYRINTIs.OrderBy(p => p.sirasi).ToListAsync();
                firmaGruplari = await FirmaGrubuAYRINTI.ara(vari);
                reklamKusagi2 = await ReklamKusagi3AYRINTI.ara(vari);
                kampanyalar = await FirmaKampanyasiAYRINTI.ara(p => p.e_onaylandiMi == true && p.e_yayindaMi == true);
                await Genel.yenile(vari);
            }
        }
    }
}
