using Bovime.veri;
using Microsoft.EntityFrameworkCore;

namespace Bovime.Models
{
    public class SiteAnaSayfaModel
    {
        public List<MansetAYRINTI> mansetler { get; set; }
        public List<ReklamKusagi1AYRINTI> reklamKusagi1 { get; set; }

        public List<ReklamKusagi2AYRINTI> reklamKusagi0 { get; set; }
        public List<ReklamKusagi3AYRINTI> reklamKusagi2 { get; set; }

        public List<SektorAYRINTI> anaSektorler { get; set; }

        public List<FirmaGrubuAYRINTI> firmaGruplari { get; set; }
        public SiteAnaSayfaModel()
        {
            mansetler = new List<MansetAYRINTI>();
            reklamKusagi1 = new List<ReklamKusagi1AYRINTI>();
            anaSektorler = new List<SektorAYRINTI>();
            reklamKusagi0 = new List<ReklamKusagi2AYRINTI>();
            firmaGruplari = new List<FirmaGrubuAYRINTI>();
            reklamKusagi2 = new List<ReklamKusagi3AYRINTI>();
        }
        public async Task veriCek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                mansetler = await vari.MansetAYRINTIs.Where(p => p.e_yayindami == true).OrderBy(p => p.sirasi).ToListAsync();
                reklamKusagi1 = await vari.ReklamKusagi1AYRINTIs.ToListAsync();
                anaSektorler = await vari.SektorAYRINTIs.OrderBy(p => p.sektorAdi).ToListAsync();
                reklamKusagi0 = await vari.ReklamKusagi2AYRINTIs.OrderBy(p => p.sirasi).ToListAsync();
                firmaGruplari = await FirmaGrubuAYRINTI.ara(vari);
                reklamKusagi2 = await ReklamKusagi3AYRINTI.ara(vari);
                await Genel.yenile(vari);
            }
        }
    }
}
