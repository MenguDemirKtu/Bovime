using Bovime.veri;
using Microsoft.EntityFrameworkCore;

namespace Bovime.Models
{
    public class SiteAnaSayfaModel
    {
        public List<MansetAYRINTI> mansetler { get; set; }
        public List<ReklamKusagi1AYRINTI> reklamKusagi1 { get; set; }

        public List<SektorAYRINTI> anaSektorler { get; set; }

        public SiteAnaSayfaModel()
        {
            mansetler = new List<MansetAYRINTI>();
            reklamKusagi1 = new List<ReklamKusagi1AYRINTI>();
            anaSektorler = new List<SektorAYRINTI>();
        }
        public async Task asdasd()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                mansetler = await vari.MansetAYRINTIs.Where(p => p.e_yayindami == true).OrderBy(p => p.sirasi).ToListAsync();
                reklamKusagi1 = await vari.ReklamKusagi1AYRINTIs.ToListAsync();
                anaSektorler = await vari.SektorAYRINTIs.OrderBy(p => p.sektorAdi).ToListAsync();


                Genel.menuHtml = await Genel.menuOlustur(vari);

            }
        }
    }
}
